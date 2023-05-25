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
using System.Diagnostics.Eventing.Reader;
#endregion

namespace GameProject
{
    public class Hero : Unit
    {
        public string name;

        public SkillBar skillBar;

        public Hero(string PATH, Vector2 POS, Vector2 DIMS, Vector2 FRAMES, int OWNERID)
            : base(PATH, POS, DIMS, FRAMES, OWNERID)
        {
            speed = 2.0f;
            name = "Zed";

            health = 5;
            healthMax = health;

            frameAnimations = true;
            currentAnimation = 0;
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), frames, new Vector2(0, 0), 4, 77, 0, "Walk"));
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), frames, new Vector2(0, 0), 1, 77, 0, "Stand"));

            skills.Add(new FlameWave(this));
            skills.Add(new Blink(this));

            skillBar = new SkillBar(new Vector2(80, Globals.screenHeight - 80), 52, 10);

            for(var i = 0; i < skills.Count; i++)
            {
                if(i < skillBar.slots.Count)
                {
                    skillBar.slots[i].skillButton = new SkillButton("2d\\Misc\\solid", new Vector2(0, 0), new Vector2(40, 40), new Vector2(1, 1), SetSkill, skills[i]);
                }
                else
                {
                    break;
                }
            }

            for(var i=0; i< 24; i++)
            {
                inventorySlots.Add(new InventorySlot(new Vector2(0, 0), new Vector2(48, 48)));
            }
        }

        public override void Update(Vector2 OFFSET, Player ENEMY, SquareGrid GRID, LevelDrawManager LEVELDRAWMANAGER)
        {
            bool checkScoll = false;

            if (Globals.keyboard.GetPress(GameGlobals.keyBinds.GetKeyName("Move Left")))
            {
                var newpos = new Vector2(pos.X - speed, pos.Y);
                if (newpos.X < GRID.totalPhysicalDims.X && newpos.X > 0)
                {
                    pos = new Vector2(pos.X - speed, pos.Y);
                    checkScoll = true;
                    flipped = true;
                }
            }

            if (Globals.keyboard.GetPress(GameGlobals.keyBinds.GetKeyName("Move Right")))
            {
                var newpos = new Vector2(pos.X + speed, pos.Y);
                if (newpos.X < GRID.totalPhysicalDims.X - 50 && newpos.X > 0)
                {
                    pos = new Vector2(pos.X + speed, pos.Y);
                    checkScoll = true;
                    flipped = false;
                }
            }

            if (Globals.keyboard.GetPress(GameGlobals.keyBinds.GetKeyName("Move Up")))
            {
                var newpos = new Vector2(pos.X, pos.Y - speed);
                if (newpos.Y < GRID.totalPhysicalDims.Y - 50 && newpos.Y > 0)
                {
                    pos = new Vector2(pos.X, pos.Y - speed);
                    checkScoll = true;
                }
            }

            if (Globals.keyboard.GetPress(GameGlobals.keyBinds.GetKeyName("Move Down")))
            {
                var newpos = new Vector2(pos.X, pos.Y + speed);
                if (newpos.Y < GRID.totalPhysicalDims.Y - 100 && newpos.X > 0)
                {
                    pos = new Vector2(pos.X, pos.Y + speed);
                    checkScoll = true;
                }
            }

            GameGlobals.CheckScroll(pos);

            if (checkScoll)
            {
                //GameGlobals.CheckScroll(pos);

                SetAnimationByName("Walk");
            }
            else
            {
                SetAnimationByName("Stand");
            }




            if (currentSkill == null)
            {
                if (Globals.mouse.LeftClick())
                {
                    GameGlobals.PassProjectile(new Fireball(new Vector2(pos.X, pos.Y), this, new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y) - OFFSET));
                    Globals.soundControl.PlaySound("Shoot");
                
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

            skillBar.Update(Vector2.Zero);
            base.Update(OFFSET, ENEMY, GRID, LEVELDRAWMANAGER);
        }

        public virtual void SetSkill(object INFO)
        {
            if (INFO != null) 
            {
                var tempPacket = (SkillSelectionTypePacket)INFO;

                currentSkill = tempPacket.skill;
                currentSkill.Active = true;
                currentSkill.selectionType = tempPacket.selectionType;
            }
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
            skillBar.Draw(Vector2.Zero);
        }
    }
}
