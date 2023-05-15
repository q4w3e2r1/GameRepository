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
using SharpDX.Direct3D9;
#endregion

namespace GameProject
{
    public class Hero : Unit
    {

        public Hero(string PATH, Vector2 POS, Vector2 DIMS, Vector2 FRAMES, int OWNERID)
            : base(PATH, POS, DIMS, FRAMES, OWNERID)
        {
            speed = 2.0f;

            health = 5;
            healthMax = health;

            frameAnimations = true;
            currentAnimation = 0;
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), frames, new Vector2(0, 0), 4, 77, 0, "Walk"));
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), frames, new Vector2(0, 0), 1, 77, 0, "Stand"));

            skills.Add(new FlameWave(this));
        }

        public override void Update(Vector2 OFFSET, Player ENEMY, SquareGrid GRID)
        {
            bool checkScoll = false;

            if (Globals.keyboard.GetPress("A"))
            {
                pos = new Vector2(pos.X - speed, pos.Y);
                checkScoll = true;
                flipped = true;
            }

            if (Globals.keyboard.GetPress("D"))
            {
                pos = new Vector2(pos.X + speed, pos.Y);
                checkScoll = true;
                flipped = false;
            }

            if (Globals.keyboard.GetPress("W"))
            {
                pos = new Vector2(pos.X, pos.Y - speed);
                checkScoll = true;
            }

            if (Globals.keyboard.GetPress("S"))
            {
                pos = new Vector2(pos.X, pos.Y + speed);
                checkScoll = true;
            }



            if (Globals.keyboard.GetSinglePress("D1"))
            {
                currentSkill = skills[0];
                currentSkill.Active = true;
            }

            if (checkScoll)
            {
                GameGlobals.CheckScroll(pos);

                SetAnimationByName("Walk");
            }
            else
            {
                SetAnimationByName("Stand");
            }


            //rot = Globals.RotateTowards(pos, new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y) - OFFSET);


            if (currentSkill == null)
            {
                if (Globals.mouse.LeftClick())
                {
                    GameGlobals.PassProjectile(new Fireball(new Vector2(pos.X, pos.Y), this, new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y) - OFFSET));
                }
            }
            else
            {
                currentSkill.Update(OFFSET, ENEMY);

                if (currentSkill.done)
                {
                    currentSkill.Reset();
                    currentSkill = null;
                }
            }

            if (Globals.mouse.RightClick())
            {
                if (currentSkill != null)
                {
                    currentSkill.targetEffect.done = true;
                    currentSkill.Reset();
                    currentSkill = null;
                }
            }

            base.Update(OFFSET, ENEMY, GRID);
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
