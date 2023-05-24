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

namespace GameProject
{
    public class Projectile2d : Basic2d
    {
        public bool done;

        public float speed;

        public Vector2 direction;

        public AttackableObject owner;

        public GameTimer timer;

        public Projectile2d(string PATH, Vector2 POS, Vector2 DIMS, AttackableObject OWNER, Vector2 TARGET)
            : base(PATH, POS, DIMS)
        {
            done = false;

            speed = 5.0f;

            owner = OWNER;

            direction = TARGET - owner.pos;
            direction.Normalize();

            rot = Globals.RotateTowards(pos, new Vector2(TARGET.X, TARGET.Y));

            timer = new GameTimer(1500);
        }

        public virtual void Update(Vector2 OFFSET, List<AttackableObject> UNITS)
        {

            ChangePosition();


            timer.UpdateTimer();
            if (timer.Test())
            {

                done = true;
            }

            if (HitSomething(UNITS))
            {

                done = true;
            }
        }

        public virtual void ChangePosition()
        {
            pos += direction * speed;
        }

        public virtual bool HitSomething(List<AttackableObject> UNITS)
        {
            for (int i = 0; i < UNITS.Count; i++)
            {
                if (owner.ownerId != UNITS[i].ownerId && Globals.GetDistance(pos, UNITS[i].pos) < UNITS[i].hitDist)
                {
                    UNITS[i].GetHit(owner, 1);
                    Globals.soundControl.PlaySound("Hit");
                    return true;
                }
            }

            return false;
        }


        public override void Draw(Vector2 OFFSET)
        {
            Globals.normalEffect.Parameters["xSize"].SetValue((float)model.Bounds.Width);
            Globals.normalEffect.Parameters["ySize"].SetValue((float)model.Bounds.Height);
            Globals.normalEffect.Parameters["xDraw"].SetValue((float)((int)dims.X));
            Globals.normalEffect.Parameters["yDraw"].SetValue((float)((int)dims.Y));
            Globals.normalEffect.Parameters["filterColor"].SetValue(Color.White.ToVector4());
            Globals.normalEffect.CurrentTechnique.Passes[0].Apply();

            base.Draw(OFFSET);
        }
    }
}
