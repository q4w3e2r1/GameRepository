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
using System.Diagnostics;
#endregion

namespace GameProject;
public class MainMenu
{
    public Basic2d bkg;

    public PassObject PLayClickDel, ExitClickDel;

    public List<Button2d> buttons = new();

    public MainMenu(PassObject PLAYERCLICKDEL, PassObject EXITCLICKDEL)
    {
        PLayClickDel = PLAYERCLICKDEL;
        ExitClickDel = EXITCLICKDEL;

        bkg = new Basic2d("2d\\UI\\BackGrounds\\MainMenu", new Vector2(Globals.screenWidth/2, Globals.screenHeight/2), new Vector2(Globals.screenWidth, Globals.screenHeight));

        buttons.Add(new Button2d("2d\\Misc\\SimpleBtn", new Vector2(0, 0), new Vector2(175, 130), new Vector2(1, 1), "Fonts\\Arial16", "PLay", PLayClickDel, 1));

        buttons.Add(new Button2d("2d\\Misc\\SimpleBtn", new Vector2(0, 0), new Vector2(175, 130), new Vector2(1, 1), "Fonts\\Arial16", "Options", PLayClickDel, 2));

        buttons.Add(new Button2d("2d\\Misc\\SimpleBtn", new Vector2(0, 0), new Vector2(175, 130), new Vector2(1, 1), "Fonts\\Arial16", "Exit", ExitClickDel, null));
    }


    public virtual void Update()
    {
        for (var i = 0; i < buttons.Count; i++)
        {
            buttons[i].Update(new Vector2(290, 530 + 130 * i));
        }

    }
    
    public virtual void Draw()
    {
        bkg.Draw(Vector2.Zero);

        for (var i = 0; i < buttons.Count; i++)
        {
            buttons[i].Draw(new Vector2(290, 530 + 130 * i));
        }
    }

}
