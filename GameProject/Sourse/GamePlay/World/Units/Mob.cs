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

    public Mob(string path, Vector2 POS, Vector2 DIMS, Vector2 FRAMES, int OWNERID) : base(path, POS, DIMS, FRAMES, OWNERID)
    {
        speed = 2.0f;
    }

    public override void Update(Vector2 OFFSET, Player ENEMY)
    {
        AI(ENEMY);
        base.Update(OFFSET);
    }


    public virtual void AI(Player ENEMY)
    {
        pos += Globals.RadialMovement(ENEMY.hero.pos, pos, speed);
        //rot = Globals.RotateTowards(pos, HERO.pos);

        if(Globals.GetDistance(pos, ENEMY.hero.pos) < 15)
        {
            ENEMY.hero.GetHit(1);
            dead = true;
        }

    }


    public override void Draw(Vector2 OFFSET)
    {
        base.Draw(OFFSET);
    }
}
