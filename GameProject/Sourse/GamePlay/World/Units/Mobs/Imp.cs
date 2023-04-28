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


public class Imp : Mob
{

    public Imp(Vector2 POS, Vector2 FRAMES, int OWNERID) : base("2d\\Units\\Mobs\\Imp", POS, new Vector2(40, 40), FRAMES, OWNERID)
    {
        speed = 2.0f;
    }

    public override void Update(Vector2 OFFSET, Player ENEMY)
    {
        
        base.Update(OFFSET, ENEMY);
    }


    public override void Draw(Vector2 OFFSET)
    {
        base.Draw(OFFSET);
    }
}
