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
using GameProject;
using static System.Reflection.Metadata.BlobBuilder;
#endregion

namespace GameProject;

public class ExitMenu : Menu2d
{
    public int state;
    public KeyBindMenu keyBindMenu;

    public List<Button2d> buttons = new();

    public PassObject Exit;

    public ExitMenu(PassObject EXIT)
        : base(new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2), new Vector2(600, 800), null)
    {

        state = 0;
        Exit = EXIT;

        buttons.Add(new Button2d("2d\\Misc\\SimpleBtn", new Vector2(0, 0), new Vector2(200, 150), new Vector2(1, 1), "Fonts\\Arial16", "Return", ReturnCLick, 0));
        buttons.Add(new Button2d("2d\\Misc\\SimpleBtn", new Vector2(0, 0), new Vector2(200, 150), new Vector2(1, 1), "Fonts\\Arial16", "Key Binds", KeyBinCLick, 0));
        buttons.Add(new Button2d("2d\\Misc\\SimpleBtn", new Vector2(0, 0), new Vector2(200, 150), new Vector2(1, 1), "Fonts\\Arial16", "Exit Level", ExitCLick, 1));


        keyBindMenu = new KeyBindMenu(ReturnFromKeyBinds);
    }

    public override void Update()
    {
        if (state == 0)
        {

            base.Update();

            if (Active)
            {
                for (var i = 0; i < buttons.Count; i++)
                {
                    buttons[i].Update(topLeft + new Vector2(dims.X / 2, 50 + 150 * i));
                }
            }
        }
        else if(state == 1)
        {
            keyBindMenu.Update();
        }
    }

    public virtual void ExitCLick(object INFO)
    {
        Exit(INFO);
    }

    public virtual void ReturnCLick(object INFO)
    {
        Active = false;
    }

    public virtual void KeyBinCLick(object INFO)
    {
        state = 1;
        keyBindMenu.Active = true;
    }

    public virtual void ReturnFromKeyBinds(object INFO)
    {
        state = 0;

        keyBindMenu.Active = false;

    }

    public override void Draw()
    {
        if (state == 0)
        {
            base.Draw();

            if (Active)
            {
                for (var i = 0; i < buttons.Count; i++)
                {
                    buttons[i].Draw(topLeft + new Vector2(dims.X / 2, 50 + 150 * i));
                }
            }
        }
        else if(state == 1)
        {
            keyBindMenu.Draw();
        }
    }
}
