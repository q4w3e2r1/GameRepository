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
#endregion

namespace GameProject;

public class Main : Game
{
    private GraphicsDeviceManager _graphics;
    // private SpriteBatch _spriteBatch;

    GamePlay gamePlay;

    private Basic2d cursor;

    public Main()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        Globals.screenWidth = 800; //1600
        Globals.screenHeight = 500; //900

        _graphics.PreferredBackBufferWidth = Globals.screenWidth;
        _graphics.PreferredBackBufferHeight = Globals.screenHeight;


        _graphics.ApplyChanges();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        Globals.content = this.Content;
        Globals.spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here

        cursor = new Basic2d("2d\\CursorArrow", new Vector2(0, 0), new Vector2(20, 20));

        Globals.keyboard = new Keyboard();
        Globals.mouse = new MouseControl();

        gamePlay = new GamePlay();
    }


    protected override void UnloadContent()
    {
        
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
           Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        Globals.gameTime = gameTime;
        Globals.keyboard.Update();
        Globals.mouse.Update();

        gamePlay.Update();


        Globals.keyboard.UpdateOld();
        Globals.mouse.UpdateOld();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        Globals.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);


        gamePlay.Draw();

        cursor.Draw(new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y), new Vector2(0,0), Color.White);
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