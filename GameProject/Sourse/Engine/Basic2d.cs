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

        public Vector2 pos, dims, frameSize;

        public Texture2D model;

        public bool flipped;

        public Basic2d(string path, Vector2 pos, Vector2 dims)
        {
            this.pos = new Vector2(pos.X, pos.Y);
            this.dims = new Vector2(dims.X, dims.Y);
            rot = 0.0f;

            model = Globals.content.Load<Texture2D>(path);
        }

        public Basic2d() 
        {


        }

        public virtual void Update(Vector2 OFFSET) 
        {


        }

        public virtual bool Hover(Vector2 OFFSET)
        {
            return HoverImg(OFFSET);
        }
        public virtual bool HoverImg(Vector2 OFFSET)
        {
            var mousePos = new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y);

            if(mousePos.X >= pos.X + OFFSET.X - dims.X/2 && mousePos.X <= pos.X + OFFSET.X + dims.X / 2
                && mousePos.Y >= pos.Y + OFFSET.Y - dims.Y / 2 && mousePos.Y <= pos.Y + OFFSET.Y + dims.Y / 2)
            {
                return true;
            }
            return false;
        }


        public virtual void Draw(Vector2 OFFSET)
        {

            var render = new Render(rot,  pos,  dims, model);
            render.Draw(OFFSET);

            //if (model != null)
            //{
            //    Globals.spriteBatch.Draw(model, new Rectangle((int)(pos.X + OFFSET.X), (int)(pos.Y + OFFSET.Y), (int)dims.X, (int)dims.Y), null, Color.White,
            //           rot, new Vector2(model.Bounds.Width / 2, model.Bounds.Height / 2), SpriteEffects.None, 0);
                
            //}
        }

        public virtual void Draw(Vector2 OFFSET, Vector2 ORIGIN, Color COLOR)
        {
            var render = new Render(rot, pos, dims, model);
            render.Draw(OFFSET, ORIGIN, COLOR);

            //if (model != null)
            //{
            //       Globals.spriteBatch.Draw(model, new Rectangle((int)(pos.X + OFFSET.X), (int)(pos.Y + OFFSET.Y), (int)dims.X, (int)dims.Y), null, COLOR,
            //           rot, new Vector2(ORIGIN.X, ORIGIN.Y), SpriteEffects.None, 0);
                
            //}
        }








        //public virtual void Draw(Vector2 OFFSET)
        //{
        //    if (model != null)
        //    {
        //        if (flipped)
        //        {
        //            Globals.spriteBatch.Draw(model, new Rectangle((int)(pos.X + OFFSET.X + dims.X), (int)(pos.Y + OFFSET.Y), (int)dims.X * -1, (int)dims.Y), null, Color.White,
        //                rot, new Vector2(model.Bounds.Width / 2, model.Bounds.Height / 2), SpriteEffects.FlipHorizontally, 0);
        //        }
        //        else
        //        {
        //            Globals.spriteBatch.Draw(model, new Rectangle((int)(pos.X + OFFSET.X), (int)(pos.Y + OFFSET.Y), (int)dims.X, (int)dims.Y), null, Color.White,
        //                rot, new Vector2(model.Bounds.Width / 2, model.Bounds.Height / 2), SpriteEffects.None, 0);
        //        }
        //    }
        //}

        //public virtual void Draw(Vector2 OFFSET, Vector2 ORIGIN, Color COLOR)
        //{
        //    if (model != null)
        //    {
        //        if (flipped)
        //        {
        //            Globals.spriteBatch.Draw(model, new Rectangle((int)(pos.X + OFFSET.X + dims.X), (int)(pos.Y + OFFSET.Y), (int)dims.X * -1, (int)dims.Y), null, COLOR,
        //                rot, new Vector2(ORIGIN.X, ORIGIN.Y), SpriteEffects.FlipHorizontally, 0);
        //        }
        //        else
        //        {
        //            Globals.spriteBatch.Draw(model, new Rectangle((int)(pos.X + OFFSET.X), (int)(pos.Y + OFFSET.Y), (int)dims.X, (int)dims.Y), null, COLOR,
        //                rot, new Vector2(ORIGIN.X, ORIGIN.Y), SpriteEffects.None, 0);
        //        }
        //    }
        //}
    }
}
