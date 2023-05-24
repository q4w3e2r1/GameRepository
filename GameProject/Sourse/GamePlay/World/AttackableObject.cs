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

    public class AttackableObject : SceneItem
    {
        public bool dead, throbing, hover;

        public int ownerId, killValue;

        public float speed, hitDist, health, healthMax;

        public GameTimer throbTimer = new GameTimer(1000);

        public AttackableObject(string path, Vector2 POS, Vector2 DIMS,Vector2 FRAMES, int OWNERID)
            : base(path, POS, DIMS, FRAMES, new Vector2(1, 1))
        {
            ownerId = OWNERID;
            dead = false;
            throbing = false;
            hover = false;
            speed = 2.0f;

            health = 1;
            healthMax = health;

            killValue = 1;

            hitDist = 35.0f;
        }

        public virtual void Update(Vector2 OFFSET, Player ENEMY, SquareGrid GRID, LevelDrawManager LEVELDRAWMANAGER)
        {
            hover = false;
            if(Hover(OFFSET))
            {
                hover = true;
            }
           if(throbing)
            {
                throbTimer.UpdateTimer();
                if(throbTimer.Test())
                {
                    throbing = false;
                    throbTimer.ResetToZero();
                }
           }

            base.Update(OFFSET, LEVELDRAWMANAGER);
        }

        public virtual void GetHit(AttackableObject ATTACKER, float DAMAGE)
        {
            if (ATTACKER.ownerId != ownerId)
            {

                health -= DAMAGE;
                throbing = true;
                //throbTimer.ResetToZero(); //?

                if (health <= 0)
                {
                    dead = true;
                    GameGlobals.PassGold(new PlayerValuePacket(ATTACKER.ownerId, killValue));
                }
            }
        }

        public override void Draw(Vector2 OFFSET)
        {

            if(hover)
            {
                Globals.highlightEffect.Parameters["xSize"].SetValue((float)model.Bounds.Width);
                Globals.highlightEffect.Parameters["ySize"].SetValue((float)model.Bounds.Height);
                Globals.highlightEffect.Parameters["xDraw"].SetValue((float)((int)dims.X));
                Globals.highlightEffect.Parameters["yDraw"].SetValue((float)((int)dims.Y));
                Globals.highlightEffect.Parameters["len1"].SetValue(1);
                Globals.highlightEffect.Parameters["len2"].SetValue(3);
                Globals.highlightEffect.Parameters["filterColor"].SetValue(Color.White.ToVector4());
                Globals.highlightEffect.CurrentTechnique.Passes[0].Apply();

                base.Draw(OFFSET);
            }

            if (throbing)
            {
                Globals.throbEffect.Parameters["SINLOC"].SetValue((float)Math.Sin(((float)throbTimer.Timer/(float)throbTimer.MSec + (float)Math.PI/2) * ((float)Math.PI * 3)));
                Globals.throbEffect.Parameters["filterColor"].SetValue(Color.Red.ToVector4());
                Globals.throbEffect.CurrentTechnique.Passes[0].Apply();
            }
            else
            {
                Globals.normalEffect.Parameters["xSize"].SetValue((float)model.Bounds.Width);
                Globals.normalEffect.Parameters["ySize"].SetValue((float)model.Bounds.Height);
                Globals.normalEffect.Parameters["xDraw"].SetValue((float)((int)dims.X));
                Globals.normalEffect.Parameters["yDraw"].SetValue((float)((int)dims.Y));
                Globals.normalEffect.Parameters["filterColor"].SetValue(Color.White.ToVector4());
                Globals.normalEffect.CurrentTechnique.Passes[0].Apply();

            }

            base.Draw(OFFSET);
        }
    }
}
