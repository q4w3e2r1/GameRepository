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

namespace GameProject;

public class Projectile2d : Basic2d
{
    public bool done;

    public float speed;

    public Unit owner;

    public Vector2 direction;

    public GameTimer timer;
    public Projectile2d(string path, Vector2 POS, Vector2 DIMS, Unit owner, Vector2 TARGET) 
        : base(path, POS, DIMS)
    {
        done = false;

        speed = 5.0f;

        this.owner = owner;

        direction = TARGET - owner.pos;
        direction.Normalize();
        


        rot = Globals.RotateTowards(pos, new Vector2(TARGET.X, TARGET.Y));


        timer = new GameTimer(1200);
    }

    public virtual void Update(Vector2 OFFSET, List<Unit> UNITS)
    {
        pos += direction * speed;



        timer.UpdateTimer();

        if(timer.Test())
        {
            done = true;
        }

        if(HitSomething(UNITS))
        {
            done = true;
        }
    }

    public virtual bool HitSomething(List<Unit> UNITS) 
    {
        for(var i = 0; i < UNITS.Count;i++)
        {

            if(Globals.GetDistance(pos, UNITS[i].pos) < UNITS[i].hitDist)
            {
                UNITS[i].GetHit();

                return true;
            }

        }

        return false;
    }

    public override void Draw(Vector2 OFFSET)
    {
        base.Draw(OFFSET);
    }
}
