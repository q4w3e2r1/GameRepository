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


public class Mob : Unit
{

    public Mob(string path, Vector2 POS, Vector2 DIMS, int OWNERID) : base(path, POS, DIMS, OWNERID)
    {
        speed = 2.0f;
    }

    public override void Update(Vector2 OFFSET, Player ENEMY)
    {
        AI(ENEMY.hero);
        base.Update(OFFSET);
    }


    public virtual void AI(Hero HERO)
    {
        pos += Globals.RadialMovement(HERO.pos, pos, speed);
        //rot = Globals.RotateTowards(pos, HERO.pos);

        if(Globals.GetDistance(pos, HERO.pos) < 15)
        {
            HERO.GetHit(1);
            dead = true;
        }

    }


    public override void Draw(Vector2 OFFSET)
    {
        base.Draw(OFFSET);
    }
}
