#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
#endregion
namespace GameProject;

public class DragAndDropPacket
{

    public Vector2 maxDims;
    public object item;
    public string type;

    public Animated2d icon;

    public DragAndDropPacket(Vector2 MAXDIMS)
    {

        maxDims = MAXDIMS;
    }

    public virtual void Update()
    {
        if(Globals.mouse.LeftClickRelease())
        {
            item = null;
            type = "";
            icon = null;
        }

    
    }


    public virtual void SetItem(object ITEM, string TYPE, string IMGSTRING)
    {
        item = ITEM;
        type = TYPE;

        if (IMGSTRING != "")
        {
            icon = new Animated2d(IMGSTRING, new Vector2(0, 0), maxDims, new Vector2(1, 1), Color.White);
        }
        else
        {
            icon = null;
        }

    }

    public virtual void Draw()
    {
        if(icon != null)
        {
            icon.Draw(new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y), new Vector2(0, 0), Color.White);
        }
    }

}
