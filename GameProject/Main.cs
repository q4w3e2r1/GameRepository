#region Includes
using System;
using System.IO;
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
using System.Reflection;
#endregion

namespace GameProject;

public class Main : Game
{
    bool lockUpdate;

    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;

    GamePlay gamePlay;
    MainMenu mainMenu;

    Basic2d cursor;

    public Main()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";

        graphics.HardwareModeSwitch = false;

        Globals.appDataFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        lockUpdate = false;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        Globals.screenWidth = 1600; //1600
        Globals.screenHeight = 900; //900

        graphics.PreferredBackBufferWidth = Globals.screenWidth;
        graphics.PreferredBackBufferHeight = Globals.screenHeight;

        graphics.ApplyChanges();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        Globals.content = this.Content;
        Globals.spriteBatch = new SpriteBatch(GraphicsDevice);

        Globals.save = new Save(1, "GameProject");
        Globals.optionsMenu = new OptionsMenu(ApplyOptions);
        // TODO: use this.Content to load your game content here

        SetFullScreen();

        cursor = new Basic2d("2d\\CursorArrow", new Vector2(0, 0), new Vector2(25, 25));


        Globals.normalEffect = Globals.content.Load<Effect>("Effects\\Normal");
        Globals.throbEffect = Globals.content.Load<Effect>("Effects\\Throb");
        Globals.highlightEffect = Globals.content.Load<Effect>("Effects\\NormalHighlight");


        Globals.keyboard = new Keyboard();
        Globals.mouse = new MouseControl();



        if (File.Exists(Globals.appDataFilePath + "\\" + Globals.save.gameName + "\\XML\\KeyBinds.xml"))
        {
            GameGlobals.keyBinds = new KeyBindList(Globals.save.GetFile("XML\\KeyBinds.xml"));
        }
        else
        {
            XDocument keybindXML = XDocument.Parse("<Root><Keys><Key n=\"Move Left\"><value>A</value></Key><Key n=\"Move Right\"><value>D</value></Key><Key n=\"Move Up\"><value>W</value></Key><Key n=\"Move Down\"><value>S</value></Key></Keys></Root>");

            Globals.save.HandleSaveFormates(keybindXML, "KeyBinds.xml");

            GameGlobals.keyBinds = new KeyBindList(Globals.save.GetFile("XML\\KeyBinds.xml"));
        }


        mainMenu = new MainMenu(ChangeGameState, ExitGame);
        gamePlay = new GamePlay(ChangeGameState);

       

        Globals.soundControl = new SoundControl("Audio\\BackGroundMusic");

        Globals.soundControl.AddSound("Shoot", "Audio\\Sounds\\Projectiles\\Shoot", .1f);
        Globals.soundControl.AddSound("Hit", "Audio\\Sounds\\Projectiles\\Hit2", .1f);

        Globals.dragAndDropPacket = new DragAndDropPacket(new Vector2(40, 40));

    }


    protected override void UnloadContent()
    {
        
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            Exit();

        Globals.gameTime = gameTime;
        Globals.keyboard.Update();
        Globals.mouse.Update();


        lockUpdate = false;
        for (int i = 0; i < Globals.msgList.Count; i++)
        {
            Globals.msgList[i].Update();

            if (!Globals.msgList[i].done)
            {
                if (Globals.msgList[i].lockScreen)
                {
                    lockUpdate = true;
                }
            }
            else
            {
                Globals.msgList.RemoveAt(i);
                i--;
            }
        }


        if (!lockUpdate)
        {

            if (Globals.gameState == 0)
            {
                mainMenu.Update();
            }
            else if (Globals.gameState == 1)
            {
                gamePlay.Update();
            }
            else if (Globals.gameState == 2)
            {
                Globals.optionsMenu.Update();
            }
        }


        if(Globals.dragAndDropPacket != null)
        {
            Globals.dragAndDropPacket.Update();
        }

        Globals.keyboard.UpdateOld();
        Globals.mouse.UpdateOld();

        base.Update(gameTime);
    }
    
    public virtual void ApplyOptions(object INFO)
    {
        FormOption musicVolume = Globals.optionsMenu.GetOptionValue("Music Volume");
        float musicVolumePercent = 1.0f;
        if (musicVolume != null)
        {
            musicVolumePercent = (float)Convert.ToDecimal(musicVolume.value, Globals.culture) / 30.0f;
        }

        Globals.soundControl.AdjustVolume(musicVolumePercent);

        SetFullScreen();
    }

    public void SetFullScreen()
    {
    //    FormOption fullScreenOption = Globals.optionsMenu.GetOptionValue("Full Screen");

    //    if (Convert.ToInt32(fullScreenOption.value, Globals.culture) == 1)
    //    {
    //        graphics.IsFullScreen = true;
    //        graphics.PreferredBackBufferWidth = 1980;
    //        graphics.PreferredBackBufferHeight = Globals.screenHeight;
    //    }
    //    else
    //    {
    //        graphics.IsFullScreen = false;
    //    }

    //    graphics.ApplyChanges();
    }

    public virtual void ChangeGameState(object INFO)
    {
        Globals.gameState = Convert.ToInt32(INFO, Globals.culture);
    }

    public virtual void ExitGame(object INFO)
    {
        System.Environment.Exit(0);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        Globals.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

        if (Globals.gameState == 0)
        {
            mainMenu.Draw();
        }
        else 
        if (Globals.gameState == 1)
        {
            gamePlay.Draw();
        }
        else if (Globals.gameState == 2)
        {
            Globals.optionsMenu.Draw();
        }


        Globals.normalEffect.Parameters["xSize"].SetValue((float)cursor.model.Bounds.Width);
        Globals.normalEffect.Parameters["ySize"].SetValue((float)cursor.model.Bounds.Height);
        Globals.normalEffect.Parameters["xDraw"].SetValue((float)((int)cursor.dims.X));
        Globals.normalEffect.Parameters["yDraw"].SetValue((float)((int)cursor.dims.Y));
        Globals.normalEffect.Parameters["filterColor"].SetValue(Color.White.ToVector4());
        Globals.normalEffect.CurrentTechnique.Passes[0].Apply();

        
        
        
        for(var i =0; i< Globals.msgList.Count; i++)
        {
            Globals.msgList[i].Draw();
        }

        if (Globals.dragAndDropPacket != null)
        {
            Globals.dragAndDropPacket.Draw();
        }

        cursor.Draw(new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y), new Vector2(0, 0), Color.White);
        Globals.spriteBatch.End();
        base.Draw(gameTime);
    }
}

public static class Programm
{
    static void Main()
    {
        using var game = new GameProject.Main();
        game.Run();
    }
}