#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
#endregion

namespace GameProject
{

    public class Effect2d : Animated2d
    {
        public bool done;
        public GameTimer timer;
        public Effect2d(string path, Vector2 POS, Vector2 DIMS, Vector2 FRAMES, int MSEC)
            : base(path, POS, DIMS,  FRAMES, Color.White)
        {
            done = false;
            timer = new GameTimer(MSEC);
        }

        public override void Update(Vector2 OFFSET)
        {
            timer.UpdateTimer();
            if(timer.Test())
            {
                done = true;
            }
            base.Update(OFFSET);
        }

       
        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
