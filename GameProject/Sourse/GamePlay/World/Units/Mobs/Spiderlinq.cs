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


public class Spiderlinq : Mob
{
    public GameTimer spawnTimer;

    public Spiderlinq(Vector2 POS, int OWNERID) : base("2d\\Units\\Mobs\\spider", POS, new Vector2(45, 45), OWNERID)
    {
        speed = 2.1f;

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
