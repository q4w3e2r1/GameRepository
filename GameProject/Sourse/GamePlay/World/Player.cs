#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using SharpDX.MediaFoundation;
#endregion

namespace GameProject;

public class Player
{
    public int id;
    public Hero hero;
    public List<Unit> units = new();
    public List<SpawnPoint> spawnPoints = new();
    public List<Building> buildings = new();

    public Player(int ID, XElement DATA)
    {
        id = ID;
        LoadData(DATA);
    }

    public virtual void Update(Player ENEMY, Vector2 OFFSET, SquareGrid GRID)
    {
        if (hero != null)
        {
            hero.Update(OFFSET, ENEMY, GRID);
        }

        for (var i = 0; i < spawnPoints.Count; i++)
        {
            spawnPoints[i].Update(OFFSET, ENEMY, GRID);
            if (spawnPoints[i].dead)
            {
                spawnPoints.RemoveAt(i);
                i--;
            }

        }

        for (var i = 0; i < units.Count; i++)
        {
            units[i].Update(OFFSET, ENEMY, GRID);

            if (units[i].dead)
            {
                ChangeScore(1);
                units.RemoveAt(i);
                i--;
            }
        }

        for (var i = 0; i < buildings.Count; i++)
        {
            buildings[i].Update(OFFSET, ENEMY, GRID);

            if (buildings[i].dead)
            {
                ChangeScore(1);
                buildings.RemoveAt(i);
                i--;
            }
        }
    }

    public virtual void AddBuilding(object INFO)
    {
        var tempBuilding = (Building)INFO;
        tempBuilding.ownerId = id;
        buildings.Add((Building)INFO);
    }

    public virtual void AddSpawnPoint(object INFO)
    {
        var tempSpawnPoint = (SpawnPoint)INFO;
        tempSpawnPoint.ownerId = id;
        spawnPoints.Add(tempSpawnPoint);
    }

    public virtual void AddUnit(object INFO)
    {
        var tempUnit = (Unit)INFO;
        tempUnit.ownerId = id;
        units.Add((Unit)INFO);
    }

    public virtual void ChangeScore(int SCORE) 
    {
        
    }

    public virtual List<AttackableObject> GetAllObjects()
    {
        var tempObjects = new List<AttackableObject>();
        tempObjects.AddRange(units.ToList<AttackableObject>());
        tempObjects.AddRange(spawnPoints.ToList<SpawnPoint>());
        tempObjects.AddRange(buildings.ToList<Building>());

        return tempObjects;

    }

    public virtual void LoadData(XElement DATA)
    {
        var spawnList = (from t in DATA.Descendants("SpawnPoint")
                         select t).ToList<XElement>();
        

        Type sType = null;

        for(var i = 0; i < spawnList.Count; i++)
        {
            sType = Type.GetType("GameProject."+spawnList[i].Element("type").Value, true);

            spawnPoints.Add((SpawnPoint)(Activator.CreateInstance(sType, new Vector2(Convert.ToInt32(spawnList[i].Element("Pos").Element("x").Value, Globals.culture), 
                Convert.ToInt32(spawnList[i].Element("Pos").Element("y").Value, Globals.culture)), new Vector2(1, 1), id, spawnList[i])));
        }

        var buildingList = (from t in DATA.Descendants("Building")
                         select t).ToList<XElement>();

        for (var i = 0; i < buildingList.Count; i++)
        {
            sType = Type.GetType("GameProject." + buildingList[i].Element("type").Value, true);

            buildings.Add((Building)(Activator.CreateInstance(sType, new Vector2(Convert.ToInt32(buildingList[i].Element("Pos").Element("x").Value, Globals.culture),
                Convert.ToInt32(buildingList[i].Element("Pos").Element("y").Value, Globals.culture)), new Vector2(1, 1), id)));
        }

        if(DATA.Element("Hero") != null)
        {
            hero = new Hero("2d\\Units\\HeroSheet1", new Vector2(Convert.ToInt32(DATA.Element("Hero").Element("Pos").Element("x").Value, Globals.culture),
                Convert.ToInt32(DATA.Element("Hero").Element("Pos").Element("y").Value, Globals.culture)), new Vector2(75, 75), new Vector2(4, 1), id);
        }
    }

    public virtual void Draw(Vector2 OFFSET)
    {
        if(hero != null)
        {
            hero.Draw(OFFSET);
        }

        for (var i = 0; i < buildings.Count; i++)
        {
            buildings[i].Draw(OFFSET);
        }

        for (var i = 0; i < spawnPoints.Count; i++)
        {
            spawnPoints[i].Draw(OFFSET);
        }

        for (var i = 0; i < units.Count; i++)
        {
            units[i].Draw(OFFSET);
        }


    
    }

}
