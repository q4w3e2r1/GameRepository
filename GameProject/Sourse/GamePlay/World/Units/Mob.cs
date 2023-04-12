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

    public float speed;
    public Mob(string path, Vector2 POS, Vector2 DIMS) : base(path, POS, DIMS)
    {
        speed = 2.0f;
    }

    public virtual void Update(Vector2 OFFSET, Hero HERO)
    {
        AI(HERO);
        base.Update(OFFSET);
    }


    public virtual void AI(Hero HERO)
    {
        pos += Globals.RadialMovement(HERO.pos, pos, speed);
        //rot = Globals.RotateTowards(pos, HERO.pos);
    }


    public override void Draw(Vector2 OFFSET)
    {
        base.Draw(OFFSET);
    }
}
