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

    public class Unit : Basic2d
    {
        public bool dead;
        public float speed, hitDist;

        public Unit(string path, Vector2 POS, Vector2 DIMS) : base(path, POS, DIMS)
        {
            dead = false;
            speed = 2.0f;

            hitDist = 35.0f;
        }

        public override void Update(Vector2 OFFSET)
        {
           
            base.Update(OFFSET);
        }

        public virtual void GetHit()
        {
            dead = true;
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
