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

public class KeyBindButton : Button2d
{

    public bool selected;
    public string keyBindString, previosKey;
    public PassObject Updated;
    public KeyBindButton(string path, Vector2 POS, Vector2 DIMS, Vector2 FRAMES, string FONTPATH, string TEXT, PassObject BUTTONCLICKED, object INFO, string KEYBINDSTRING, PassObject UPDATED)
            : base(path, POS, DIMS, FRAMES, FONTPATH, TEXT, BUTTONCLICKED, INFO)
    {

        selected = false;
        keyBindString = KEYBINDSTRING;
        previosKey = "";

        Updated = UPDATED;
    }

    public override void Update(Vector2 OFFSET)
    {
        base.Update(OFFSET);

        if(selected)
        {
            if(Globals.keyboard.pressedKeys.Count > 0)
            {

                SetNew(Globals.keyboard.pressedKeys[0].key);
            }
        }
    }

    public virtual void SetNew(string TXT)
    {
        text = TXT;
        var tempKeyBind = GameGlobals.keyBinds.GetKeyBindByName(keyBindString);

        if (tempKeyBind != null)
        {
            previosKey = tempKeyBind.key;
            tempKeyBind.key = text;

            if (Updated != null)
            {
                Updated(this);
            }

            selected = false;
        }
    }

    public override void RunBtnClick()
    {

        selected = true;
        base.RunBtnClick();
    }

    public override void Draw(Vector2 OFFSET)
    {
        Globals.normalEffect.Parameters["xSize"].SetValue(1.0f);
        Globals.normalEffect.Parameters["ySize"].SetValue(1.0f);
        Globals.normalEffect.Parameters["xDraw"].SetValue(1.0f);
        Globals.normalEffect.Parameters["yDraw"].SetValue(1.0f);
        Globals.normalEffect.Parameters["filterColor"].SetValue(Color.Black.ToVector4());
        Globals.normalEffect.CurrentTechnique.Passes[0].Apply();



        var strDims = font.MeasureString(keyBindString);
        Globals.spriteBatch.DrawString(font, keyBindString, pos + OFFSET + new Vector2(-150, -strDims.Y / 2), Color.Black);

        base.Draw(OFFSET);
    }
}
