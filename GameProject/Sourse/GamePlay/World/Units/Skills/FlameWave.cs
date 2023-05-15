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

public class FlameWave : Skill
{

    public FlameWave(AttackableObject OWNER) : base(OWNER)
    {


    }

    public override void Targeting(Vector2 OFFSET, Player ENEMY)
    {
        if (Globals.mouse.LeftClickRelease())
        {
            targetEffect.done = true;

            GameGlobals.PassProjectile(new FlameWaveProjectile(Globals.mouse.newMousePos - OFFSET, owner, new Vector2(0, 0), 1500));

            done = true;
            active = false;

        }
        else
        {
            targetEffect.pos = Globals.mouse.newMousePos - OFFSET;
        }
    }

    //public override void Targeting(Vector2 OFFSET, Player ENEMY)
    //{
    //    if(Globals.mouse.LeftClickRelease())
    //    {
    //        targetEffect.done = true;
    //        GameGlobals.PassEffect(new FlameCircle(Globals.mouse.newMousePos - OFFSET, new Vector2(targetEffect.dims.X, targetEffect.dims.Y)));

    //        done = true;
    //        active = false;

    //        for(var i =0; i < ENEMY.units.Count; i++)
    //        {
    //            if (Globals.GetDistance(ENEMY.units[i].pos, Globals.mouse.newMousePos - OFFSET) <= targetEffect.dims.X/2)
    //            {
    //                ENEMY.units[i].GetHit(, 100.0f);
    //            }
    //        }
    //    }
    //    else
    //    {
    //        targetEffect.pos = Globals.mouse.newMousePos - OFFSET;
    //        targetEffect.timer.ResetToZero();
    //    }
    //}
}
