#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.ComponentModel.Design.Serialization;
#endregion

namespace GameProject;

public class World
{
    public int levelId;

    public Vector2 offset;

    public CharacterMenu charachetMenu;

    public ExitMenu exitMenu;

    public UI ui;

    public User user;
    public AIPlayer aIPlayer;

    public SquareGrid grid;

    public TileBkg2d bkg;

    public LevelDrawManager levelDrawManager;

    public List<Projectile2d> projectiles = new();
    public List<AttackableObject> allObjects = new();
    public List<Effect2d> effects = new();
    public List<SceneItem> sceneItems = new();
    public List<LootBag> lootBags = new();

    PassObject ResetWorld, ChangeGameState, ChangePlayState;


    public World(PassObject RESETWORLD, int LEVELID, PassObject CHANGEGAMESTATE, PassObject CHANGEPLAYSTATE)
    {

        levelId = LEVELID;
        ResetWorld = RESETWORLD;
        ChangeGameState = CHANGEGAMESTATE;
        ChangePlayState = CHANGEPLAYSTATE;

        levelDrawManager = new LevelDrawManager();

        GameGlobals.PassProjectile = AddProjectile;
        GameGlobals.PassMob = AddMob;
        GameGlobals.PassEffect = AddEffect;
        GameGlobals.PassBuilding = AddBuilding;
        GameGlobals.PassSpawnPoint = AddSpawnPoint;
        GameGlobals.CheckScroll = CheckScroll;
        GameGlobals.PassGold = AddGold;
        GameGlobals.PassLootBag = AddLootBag;


        GameGlobals.paused = false;

        offset = new Vector2(0, 0);

        LoadData(levelId);

        charachetMenu = new CharacterMenu(user.hero);

        exitMenu = new ExitMenu(ExiLevel);

        ui = new UI(ResetWorld, user.hero);
        if (levelId == 1)
        {
            bkg = new TileBkg2d("2d\\UI\\Backgrounds\\StandardDirt", new Vector2(-700, -500), new Vector2(120, 100), new Vector2(grid.totalPhysicalDims.X + 2000, grid.totalPhysicalDims.Y + 2000));
        }
        if(levelId == 2)
        {
            bkg = new TileBkg2d("2d\\UI\\Backgrounds\\StandardGrass", new Vector2(-700, -500), new Vector2(100, 100), new Vector2(grid.totalPhysicalDims.X + 2000, grid.totalPhysicalDims.Y + 2000));

        }
        if(levelId == 3)
        {
            bkg = new TileBkg2d("2d\\UI\\Backgrounds\\StandardSnow", new Vector2(-700, -500), new Vector2(100, 100), new Vector2(grid.totalPhysicalDims.X + 2000, grid.totalPhysicalDims.Y + 2000));

        }
        if(levelId == 4)
        {
            bkg = new TileBkg2d("2d\\UI\\Backgrounds\\magma", new Vector2(-700, -500), new Vector2(100, 100), new Vector2(grid.totalPhysicalDims.X + 2000, grid.totalPhysicalDims.Y + 2000));

        }
    }

    public virtual void Update()
    {
        ui.Update(this);

        if (!DontUpdate())
        {
            levelDrawManager.Update();

            allObjects.Clear();
            allObjects.AddRange(user.GetAllObjects());
            allObjects.AddRange(aIPlayer.GetAllObjects());

            user.Update(aIPlayer, offset, grid, levelDrawManager);
            aIPlayer.Update(user, offset, grid, levelDrawManager);




            for(var i=0; i < lootBags.Count; i++)
            {
                lootBags[i].Update(offset);

                if (lootBags[i].done)
                {
                    lootBags.RemoveAt(i);
                    i--;
                }
            }


            for (var i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Update(offset, allObjects);

                if (projectiles[i].done)
                {
                    projectiles.RemoveAt(i);
                    i--;
                }
            }

            for (var i = 0; i < effects.Count; i++)
            {
                effects[i].Update(offset);

                if (effects[i].done)
                {
                    effects.RemoveAt(i);
                    i--;
                }
            }

            for (var i = 0; i < sceneItems.Count; i++)
            {
                sceneItems[i].Update(offset);

                sceneItems[i].UpdateDraw(offset, levelDrawManager);
            }
            //ui.Update(this);

        }
        else
        {
            if (Globals.keyboard.GetPress("Enter") && (user.hero.dead || user.buildings.Count <= 0))
            {
                ResetWorld(null);
            }
        }

        charachetMenu.Update();
        exitMenu.Update();

        if(grid != null)
        {
            grid.Update(offset);
        }

        if (Globals.keyboard.GetSinglePress("Back"))
        {
            ResetWorld(null);
            ChangeGameState(0);
        }

        if (Globals.keyboard.GetSinglePress("Space"))
        {
            GameGlobals.paused = !GameGlobals.paused;
        }

        if (Globals.keyboard.GetSinglePress("Escape"))
        {
            exitMenu.Active = !exitMenu.Active;

            charachetMenu.Active = false;
        }

        if (Globals.keyboard.GetSinglePress("G"))
        {
            grid.showGrid = !grid.showGrid;
        }

        if (Globals.keyboard.GetSinglePress("C"))
        {
            charachetMenu.Active = true;

            exitMenu.Active = false;
        }


        if(aIPlayer.defeated)
        {
            Globals.msgList.Add(new DismissibleMessage(new Vector2(Globals.screenWidth/2, Globals.screenHeight/2), new Vector2(250, 110), "You\'ve completed the level!", Color.Black, true, WinConfirm));

            //WinConfirm(null);
        }

        
    }

    public virtual void AddBuilding(object INFO)
    {
        var tempBuilding = (Building)INFO;

        if (user.id == tempBuilding.ownerId)
        {
            user.AddBuilding(tempBuilding);
        }
        else if (aIPlayer.id == tempBuilding.ownerId)
        {
            aIPlayer.AddBuilding(tempBuilding);
        }


       // aIPlayer.AddUnit((Mob)INFO);
    }

    public virtual void AddEffect(object INFO)
    {
        effects.Add((Effect2d)INFO);
    }

    public virtual void AddGold(object INFO)
    {
        var packet = (PlayerValuePacket)INFO;

        if (user.id == packet.playerId)
        {
            user.gold += (int)packet.value;
        }
        else if (aIPlayer.id == packet.playerId)
        {
            aIPlayer.gold += (int)packet.value;
        }

    }

    public virtual void AddLootBag(object INFO)
    {
        var tempBag = (LootBag)INFO;
        lootBags.Add(tempBag);
    }

    public virtual void AddMob(object INFO)
    {
        var tempUnit = (Unit)INFO;

        if(user.id == tempUnit.ownerId)
        {
            user.AddUnit(tempUnit);
        }
        else if(aIPlayer.id == tempUnit.ownerId)
        {
            aIPlayer.AddUnit(tempUnit);
        }


       // aIPlayer.AddUnit((Mob)INFO);
    }


    public virtual void AddProjectile(object INFO)
    {
        projectiles.Add((Projectile2d)INFO);
    }

  

    public virtual void AddSpawnPoint(object INFO)
    {
        var tempSpawnPoint = (SpawnPoint)INFO;

        if (user.id == tempSpawnPoint.ownerId)
        {
            user.AddSpawnPoint(tempSpawnPoint);
        }
        else if (aIPlayer.id == tempSpawnPoint.ownerId)
        {
            aIPlayer.AddSpawnPoint(tempSpawnPoint);
        }

    }

    public virtual void CheckScroll(object INFO)
    {
        var tempPos = (Vector2)INFO;

        float maxMovement = user.hero.speed * 4.5f;

        float diff = 0;


        if (tempPos.X < -offset.X + (Globals.screenWidth * .4f))
        {
            diff = -offset.X + (Globals.screenWidth * .4f) - tempPos.X;

            offset = new Vector2(offset.X +Math.Min(maxMovement, diff) , offset.Y);
        }

        if (tempPos.X > -offset.X + (Globals.screenWidth * .6f))
        {
            diff = tempPos.X - (-offset.X + (Globals.screenWidth * .6f));

            offset = new Vector2(offset.X - Math.Min(maxMovement, diff), offset.Y);
        }

        if (tempPos.Y < -offset.Y + (Globals.screenHeight * .4f))
        {

            diff = -offset.Y + (Globals.screenHeight * .4f) - tempPos.Y;

            offset = new Vector2(offset.X, offset.Y + Math.Min(maxMovement, diff));
        }

        if (tempPos.Y > -offset.Y + (Globals.screenHeight * .6f))
        {
            diff = tempPos.Y - (-offset.Y + (Globals.screenHeight * .6f));

            offset = new Vector2(offset.X, offset.Y - Math.Min(maxMovement, diff));
        }


       /* if (tempPos.X < -offset.X + (Globals.screenWidth * .4f))
        {
            offset = new Vector2(offset.X + user.hero.speed * 2, offset.Y );
        }

        if (tempPos.X > -offset.X + (Globals.screenWidth * .6f))
        {
            offset = new Vector2(offset.X - user.hero.speed * 2, offset.Y);
        }

        if (tempPos.Y < -offset.Y + (Globals.screenHeight * .4f))
        {
            offset = new Vector2(offset.X, offset.Y + user.hero.speed * 2 );
        }

        if (tempPos.Y > -offset.Y + (Globals.screenHeight * .6f))
        {
            offset = new Vector2(offset.X, offset.Y - user.hero.speed * 2);
        }*/
    }

    public virtual bool DontUpdate()
    {
        if(user.hero.dead || user.buildings.Count <= 0 || GameGlobals.paused || ui.skillMenu.active || charachetMenu.Active || exitMenu.Active)
        {
            return true;
        }

        return false;
    }

    public virtual void ExiLevel(object INFO)
    {
        ChangePlayState(INFO);
    }

    public virtual void LoadData(int LEVEL)
    {
        var xml = XDocument.Load("XML\\Levels\\Level" + LEVEL + ".xml");

        XElement tempElement = null;
        if (xml.Element("Root").Element("User") != null)
        {
            tempElement = xml.Element("Root").Element("User");
        }
        user = new User(1, tempElement);

        if(user.hero != null)
        {
            GameGlobals.AddInventory = user.hero.AddToInventory;
        }

        tempElement = null;
        if (xml.Element("Root").Element("AIPlayer") != null)
        {
            tempElement = xml.Element("Root").Element("AIPlayer");
        }

        grid = new SquareGrid(new Vector2(25, 25), new Vector2(-100, -100), new Vector2(Globals.screenWidth + 200, Globals.screenHeight + 200), xml.Element("Root").Element("GridItems"));

        aIPlayer = new AIPlayer(2, tempElement);



        Type sType = null;
        var sceneItemsList = (from t in xml.Element("Root").Element("Scene").Descendants("SceneItem")
                            select t).ToList<XElement>();

        for (var i = 0; i < sceneItemsList.Count; i++)
        {
            sType = Type.GetType("GameProject." + sceneItemsList[i].Element("type").Value, true);

            sceneItems.Add((SceneItem)(Activator.CreateInstance(sType, new Vector2(Convert.ToInt32(sceneItemsList[i].Element("Pos").Element("x").Value, Globals.culture), Convert.ToInt32(sceneItemsList[i].Element("Pos").Element("y").Value, Globals.culture)), new Vector2((float)Convert.ToDouble(sceneItemsList[i].Element("scale").Value, Globals.culture)))));
        }


    }

    public virtual void WinConfirm(object INFO)
    {
        ResetWorld(null);
        ChangePlayState(1);
    }


    public virtual void Draw(Vector2 OFFSET)
    {
        bkg.Draw(offset);
        grid.DrawGrid(offset);

       
        user.Draw(offset);
        aIPlayer.Draw(offset);

        //for(var i = 0; i < sceneItems.Count;i++)
        //{
        //    sceneItems[i].Draw(offset);
        //}

        if(levelDrawManager != null)
        {
            levelDrawManager.Draw();
        }

        for (var i = 0; i < projectiles.Count; i++)
        {
            projectiles[i].Draw(offset);
        }

        for (var i = 0; i < effects.Count; i++)
        {
            effects[i].Draw(offset);
        }

        for (var i = 0; i < lootBags.Count; i++)
        {
            lootBags[i].Draw(offset);
        }

        ui.Draw(this);

        charachetMenu.Draw();
        exitMenu.Draw();
    }
}
