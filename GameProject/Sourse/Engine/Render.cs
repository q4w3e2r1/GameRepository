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
using System.Reflection;
#endregion

namespace GameProject;

public class Render
{
    private readonly float rot;

    private readonly Vector2 pos, dims, frameSize;

    private readonly Texture2D model;

    private readonly bool flipped;

    private readonly List<FrameAnimation> frameAnimationList = new();
    private readonly Color color;
    private readonly bool frameAnimations;
    private readonly int currentAnimation = 0;
    private readonly Vector2 sheetFrame;


    public Render(float rot, Vector2 pos, Vector2 dims, Texture2D model)
    {
        this.rot = rot;
        this.pos = pos;
        this.dims = dims;
        this.model = model;
    }

    public Render(float rot, Vector2 pos, Vector2 dims, Texture2D model, Color color, Vector2 sheetFrame) : this(rot, pos, dims, model)
    {
        this.color = color;
        this.sheetFrame = sheetFrame;

    }

    public virtual void Draw(Vector2 OFFSET)
    {
        if (model != null)
        {
            Globals.spriteBatch.Draw(model, new Rectangle((int)(pos.X + OFFSET.X), (int)(pos.Y + OFFSET.Y), (int)dims.X, (int)dims.Y), null, Color.White,
            rot, new Vector2(model.Bounds.Width / 2, model.Bounds.Height / 2), SpriteEffects.None, 0);
        }
    }

    public virtual void Draw(Vector2 OFFSET, Vector2 ORIGIN, Color COLOR)
    {
        if (model != null)
        {
            Globals.spriteBatch.Draw(model, new Rectangle((int)(pos.X + OFFSET.X), (int)(pos.Y + OFFSET.Y), (int)dims.X, (int)dims.Y), null, COLOR,
                rot, new Vector2(ORIGIN.X, ORIGIN.Y), SpriteEffects.None, 0);

        }
    }

    public void Draw(Vector2 imageDims, Vector2 screenShift, SpriteEffects spriteEffect)
    {
        Globals.spriteBatch.Draw(model, new Rectangle((int)(pos.X + screenShift.X), (int)(pos.Y + screenShift.Y), (int)Math.Ceiling(dims.X), (int)Math.Ceiling(dims.Y)), new Rectangle((int)(sheetFrame.X * imageDims.X), (int)(sheetFrame.Y * imageDims.Y), (int)imageDims.X, (int)imageDims.Y), color, rot, imageDims / 2, spriteEffect, 0);
    }


}
