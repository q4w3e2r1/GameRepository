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
#endregion

namespace GameProject;

public class TileBkg2d : Basic2d
{
    public Vector2 bkgDims;


    public TileBkg2d(string PATH, Vector2 POS, Vector2 DIMS, Vector2 BKGDIMS) : base(PATH, POS, new Vector2((float)Math.Floor(DIMS.X), (float)Math.Floor(DIMS.Y)))
    {

        bkgDims = new Vector2((float)Math.Floor(BKGDIMS.X), (float)Math.Floor(BKGDIMS.Y));

    }

    public override void Draw(Vector2 OFFSET)
    {
        float numX =(float) Math.Ceiling(bkgDims.X / dims.X);
        float numY = (float)Math.Ceiling(bkgDims.Y / dims.Y);
        for (var i = 0; i < numX; i++)
        {
            for (var j = 0; j < numY; j++)
            {
                if(i < numX - 1 && j < numY - 1) 
                {
                    base.Draw(OFFSET + new Vector2(dims.X / 2 + dims.X * i, dims.Y / 2 + dims.Y * j));
                }
                else
                {
                    var xLeft = Math.Min(dims.X, (bkgDims.X - (i * dims.X)));
                    var yLeft = Math.Min(dims.Y, (bkgDims.Y - (j * dims.Y)));
                    var xPercentLeft = Math.Min(1, xLeft / dims.X);
                    var yPercentLeft = Math.Min(1, yLeft / dims.Y);

                    Globals.spriteBatch.Draw(model, new Rectangle((int)(pos.X + OFFSET.X + dims.X * i), (int)(pos.Y + OFFSET.Y + dims.Y * j), (int)Math.Ceiling(dims.X * xPercentLeft), (int)Math.Ceiling(dims.Y * yPercentLeft)), 
                        new Rectangle(0, 0, (int)(xPercentLeft * model.Bounds.Width), (int)(yPercentLeft * model.Bounds.Height)), Color.White, rot, new Vector2(0, 0), new SpriteEffects(), 0);
                }
            }
        }
    }

}
