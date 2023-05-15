#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
#endregion

namespace GameProject;

public class UI
{
    public Basic2d pausedOvelay;

    public Button2d resetBtn;

    public SpriteFont font;

    public QuantityDisplayBar healthBar;

    public UI(PassObject RESET) 
    {
        pausedOvelay = new Basic2d("2d\\Misc\\PauseOvelay", new Vector2(Globals.screenWidth/2, Globals.screenHeight/2), new Vector2(300, 300));

        font = Globals.content.Load<SpriteFont>("Fonts\\Arial16");

        resetBtn = new Button2d("2d\\Misc\\SimpleBtn", new Vector2(0, 0), new Vector2(150, 100), "Fonts\\Arial16", "Reset", RESET, null);

        healthBar = new QuantityDisplayBar(new Vector2(150, 40), 2, Color.Red);
    }

    public void Update(World WORLD)
    {
        healthBar.Update(WORLD.user.hero.health, WORLD.user.hero.healthMax);

        if (WORLD.user.hero.dead || WORLD.user.buildings.Count <= 0)
        {
            resetBtn.Update(new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2 + 100));
        }
    }
    public void Draw(World WORLD)
    {
        Globals.normalEffect.Parameters["xSize"].SetValue(1.0f);
        Globals.normalEffect.Parameters["ySize"].SetValue(1.0f);
        Globals.normalEffect.Parameters["xDraw"].SetValue(1.0f);
        Globals.normalEffect.Parameters["yDraw"].SetValue(1.0f);
        Globals.normalEffect.Parameters["filterColor"].SetValue(Color.Black.ToVector4());
        Globals.normalEffect.CurrentTechnique.Passes[0].Apply();

        var tempStr = "Gold = " + WORLD.user.gold;
        var strDims = font.MeasureString(tempStr);
        Globals.spriteBatch.DrawString(font, tempStr,
            new Vector2(40, 40), Color.Black);


        tempStr = "Score = " + GameGlobals.score;
        strDims = font.MeasureString(tempStr);
        Globals.spriteBatch.DrawString(font, tempStr,
            new Vector2(Globals.screenWidth / 2 - strDims.X / 2, Globals.screenHeight - 40), Color.Black);

        if (WORLD.user.hero.dead || WORLD.user.buildings.Count <= 0)
        {
            tempStr = "Press Enter or click the button Restart!";
            strDims = font.MeasureString(tempStr);
            Globals.spriteBatch.DrawString(font, tempStr,
                new Vector2(Globals.screenWidth / 2 - strDims.X / 2, Globals.screenHeight / 2), Color.Black);

            resetBtn.Draw(new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2 + 100));
        }

        healthBar.Draw(new Vector2(20, Globals.screenHeight - 40));

        if(GameGlobals.paused)
        {
            pausedOvelay.Draw(Vector2.Zero);
        }

        
    }
}
