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


public class Tower : Building
{

    public Tower( Vector2 POS, Vector2 FRAMES, int OWNERID)
        : base("2d\\Buildings\\Obelisk1", POS, new Vector2(90, 90), FRAMES , OWNERID)
    {
        health = 30;
        healthMax = health;

        hitDist = 35.0f;

    }

    public override void Update(Vector2 OFFSET, Player ENEMY, SquareGrid GRID, LevelDrawManager LEVELDRAWMANAGER)
    {

        base.Update(OFFSET, ENEMY, GRID, LEVELDRAWMANAGER);
    }
}
