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

namespace GameProject;


public class Hero : Unit
{
    public Hero(string path, Vector2 POS, Vector2 DIMS, Vector2 FRAMES, int OWNERID) 
        : base(path, POS, DIMS, FRAMES, OWNERID)
    {
        speed = 2.0f;

        health = 5;
        healthMax = health;

        frameAnimations = true;
        currentAnimation = 0;
       

        frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), frames, new Vector2(0, 0), 4, 77, 0, "Walk"));
        frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), frames, new Vector2(0, 0), 1, 77, 0, "Stand"));

        skills.Add(new FlameWave());
    
    }


    public override void Update(Vector2 OFFSET, Player ENEMY, SquareGrid GRID)
    {
        var checkScroll = false;


        if (Globals.keyboard.GetPress("A"))
        {
            pos = new Vector2(pos.X - speed, pos.Y);
            checkScroll = true;
            flipped = true;
        }

        if (Globals.keyboard.GetPress("D"))
        {
            pos = new Vector2(pos.X + speed, pos.Y);
            checkScroll = true;
            flipped = false;
        }

        if (Globals.keyboard.GetPress("W"))
        {
            pos = new Vector2(pos.X, pos.Y - speed);
            checkScroll = true;
        }

        if (Globals.keyboard.GetPress("S"))
        {
            pos = new Vector2(pos.X, pos.Y + speed);
            checkScroll = true;
        }

        if (Globals.keyboard.GetSinglePress("T"))
        {
            var tempLoc = GRID.GetSlotFromPixel(new Vector2(pos.X - 10, pos.Y - 50), Vector2.Zero);
            var loc = GRID.GetSlotFromLocation(tempLoc);

            if (loc != null && !loc.filled && !loc.impassable)
            {
                loc.SetToFilled(false);
                var tempBuilding = new ArrowTower(new Vector2(0, 0), new Vector2(1, 1), ownerId);
                tempBuilding.pos = GRID.GetPosFromLoc(tempLoc) + GRID.slotDims / 2 + new Vector2(0, -tempBuilding.dims.Y * .25f);

                GameGlobals.PassBuilding(tempBuilding);
            }
        }

        if (Globals.keyboard.GetSinglePress("D1"))
        {
            currentSkill = skills[0];
            currentSkill.Active = true;
        }


        if (checkScroll)
        {
            GameGlobals.CheckScroll(pos);

            SetAnimationByName("Walk");
        }
        else
        {
            SetAnimationByName("Stand");
        }



        // rot = Globals.RotateTowards(pos, new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y) - OFFSET);
        if (currentSkill == null)
        {

            if (Globals.mouse.LeftClick())
            {
                GameGlobals.PassProjectile(new Fireball(new Vector2(pos.X, pos.Y), this,
                    new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y) - OFFSET));

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


            if (Globals.mouse.RightClick())
            {
                
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
