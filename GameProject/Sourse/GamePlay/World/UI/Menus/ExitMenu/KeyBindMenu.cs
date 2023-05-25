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

public class KeyBindMenu : Menu2d
{
    public List<Button2d> buttons = new();

    public List<KeyBindButton> keyBindButtons = new();

    public PassObject Exit;

    public KeyBindMenu(PassObject EXIT)
        : base(new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2), new Vector2(600, 800), null)
    {
        Exit = EXIT;

        hasCloseBtn = false;

        for(var i=0; i < GameGlobals.keyBinds.keyBinds.Count; i++)
        {
            keyBindButtons.Add(new KeyBindButton("2d\\Misc\\SimpleBtn", new Vector2(0,0), new Vector2(60, 60), new Vector2(1, 1), "Fonts\\Arial16", GameGlobals.keyBinds.keyBinds[i].key, CheckSelected, null, GameGlobals.keyBinds.keyBinds[i].name, CheckDuplicate));

            keyBindButtons[keyBindButtons.Count - 1].info = keyBindButtons[keyBindButtons.Count - 1];
        }

        buttons.Add(new Button2d("2d\\Misc\\SimpleBtn", new Vector2(0, 0), new Vector2(200, 150), new Vector2(1, 1), "Fonts\\Arial16", "Return", ExitCLick, 1));

    }

    public override void Update()
    {
        base.Update();

        if (Active)
        {
            for (var i = 0; i < keyBindButtons.Count; i++)
            {
                keyBindButtons[i].Update(topLeft + new Vector2(dims.X - keyBindButtons[i].dims.X * 1.25f, 30 + 60 * i));
            }

            for (var i = 0; i < buttons.Count; i++)
            {
                buttons[i].Update(topLeft + new Vector2(dims.X/2, dims.Y - keyBindButtons[i].dims.X * 1.25f + 150 * i));
            }
        }
    }

    public virtual void ExitCLick(object INFO)
    {
        var keyBindsDoc = new XDocument(new XElement("Root", ""));
        keyBindsDoc.Element("Root").Add(GameGlobals.keyBinds.ReturnXML());

        Globals.save.HandleSaveFormates(keyBindsDoc, "KeyBinds.xml");

        Exit(INFO);
    }

    public virtual void CheckDuplicate(object INFO)
    {
        KeyBindButton tempButton = (KeyBindButton)INFO;

        for (var i = 0; i < keyBindButtons.Count; i++)
        {
            if (keyBindButtons[i] != tempButton && keyBindButtons[i].text == tempButton.text)
            {
                keyBindButtons[i].SetNew(tempButton.previosKey);
            }
        }
    }

    public virtual void CheckSelected(object INFO)
    {
        KeyBindButton tempButton = (KeyBindButton)INFO;

        for(var i=0; i <keyBindButtons.Count; i++)
        {
            if (keyBindButtons[i] != tempButton)
            {
                keyBindButtons[i].selected = false;
            }
        }
    }

   
    public override void Draw()
    {
        base.Draw();

        if(Active)
        {
            for (var i = 0; i < keyBindButtons.Count; i++)
            {
                keyBindButtons[i].Draw(topLeft + new Vector2(dims.X - keyBindButtons[i].dims.X * 1.25f, 30 + 60 * i));
            }

            for (var i = 0; i < buttons.Count; i++)
            {
                buttons[i].Draw(topLeft + new Vector2(dims.X / 2, dims.Y - keyBindButtons[i].dims.X * 1.25f + 150 * i));
            }
        }
    }
}
