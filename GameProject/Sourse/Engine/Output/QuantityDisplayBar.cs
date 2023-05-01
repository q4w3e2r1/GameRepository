﻿#region Includes
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
#endregion
public class QuantityDisplayBar
{
    public int boarder;

    public Basic2d bar, barBKG;
    public Color color;
    public QuantityDisplayBar(Vector2 DIMS, int BOARDER, Color COLOR)
    {
        boarder = BOARDER;
        color = COLOR;
        bar = new Basic2d("2d\\Misc\\solid", new Vector2(0, 0),
            new Vector2(DIMS.X - boarder * 2, DIMS.Y - boarder * 2));
        barBKG = new Basic2d("2d\\Misc\\shadeHealth", new Vector2(0, 0),
            new Vector2(DIMS.X, DIMS.Y));
    }

    public virtual void Update(float CURRENT, float MAX) 
    {
        bar.dims = new Vector2(CURRENT/MAX * (barBKG.dims.X - boarder * 2), bar.dims.Y);
    }

    public virtual void Draw(Vector2 OFFSET)
    {
        Globals.normalEffect.Parameters["xSize"].SetValue(1.0f);
        Globals.normalEffect.Parameters["ySize"].SetValue(1.0f);
        Globals.normalEffect.Parameters["xDraw"].SetValue(1.0f);
        Globals.normalEffect.Parameters["yDraw"].SetValue(1.0f);
        Globals.normalEffect.Parameters["filterColor"].SetValue(Color.White.ToVector4());
        Globals.normalEffect.CurrentTechnique.Passes[0].Apply();



        barBKG.Draw(OFFSET, new Vector2(0, 0), Color.Black);

        Globals.normalEffect.Parameters["filterColor"].SetValue(color.ToVector4());
        Globals.normalEffect.CurrentTechnique.Passes[0].Apply();

        bar.Draw(OFFSET + new Vector2(boarder, boarder),new Vector2(0,0), color);
    }


}
