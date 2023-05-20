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


public class Wizard : Mob
{

    public Wizard(Vector2 POS, Vector2 FRAMES, int OWNERID) : base("2d\\Units\\Wizard", POS, new Vector2(40, 40), new Vector2(8, 1), OWNERID)
    {
        speed = 2.0f;
        attackRange = 400;


        //frameAnimations = true;
        //currentAnimation = 0;

        //frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), frames, new Vector2(0, 0), 5, 88, 0, "Walk"));
    }

    public override void Update(Vector2 OFFSET, Player ENEMY, SquareGrid GRID, LevelDrawManager LEVELDRAWMANAGER)
    {
        
        base.Update(OFFSET, ENEMY, GRID, LEVELDRAWMANAGER);



        if (Math.Cos(rot) >= 0 || Math.Sin(rot) <= 0)
        {
            flipped = false;
        }
        else
        {
            flipped = true;
        }
        rot = 0;
        //SetAnimationByName("Walk");
    }

    public override void AI(Player ENEMY, SquareGrid GRID)
    {

        if (ENEMY.hero != null && (Globals.GetDistance(pos, ENEMY.hero.pos) < attackRange * .9f || isAttacking))
        {
            isAttacking = true;

            attackTimer.UpdateTimer();
            if(attackTimer.Test())
            {

                GameGlobals.PassProjectile(new AcidSplash(new Vector2(pos.X, pos.Y), this, new Vector2(ENEMY.hero.pos.X, ENEMY.hero.pos.Y)));

                attackTimer.ResetToZero();
                isAttacking = false;
            }
        }
        else
        {
            base.AI(ENEMY, GRID);
        }
    }


    public override void Draw(Vector2 OFFSET)
    {
        base.Draw(OFFSET);
    }
}
