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

namespace GameProject
{
    public class Basic2d
    {
        public float rot;

        public Vector2 pos, dims;

        public Texture2D model;

        public Basic2d(string path, Vector2 pos, Vector2 dims)
        {
            this.pos = pos;
            this.dims = dims;

            model = Globals.content.Load<Texture2D>(path);
        }

        public Basic2d() 
        {


        }

        public virtual void Update(Vector2 OFFSET) 
        {


        }

        public virtual void Draw(Vector2 OFFSET)
        {
            if(model != null)
            {
                Globals.spriteBatch.Draw(model, new Rectangle((int)(pos.X + OFFSET.X), (int)(pos.Y + OFFSET.Y), (int)dims.X, (int)dims.Y), null, Color.White,
                    rot, new Vector2(model.Bounds.Width/2, model.Bounds.Height/2), new SpriteEffects(), 0);
            }

        }

        public virtual void Draw(Vector2 OFFSET, Vector2 ORIGIN)
        {
            if (model != null)
            {
                Globals.spriteBatch.Draw(model, new Rectangle((int)(pos.X + OFFSET.X), (int)(pos.Y + OFFSET.Y), (int)dims.X, (int)dims.Y), null, Color.White,
                    rot, new Vector2(ORIGIN.X, ORIGIN.Y), new SpriteEffects(), 0);
            }

        }
    }
}
