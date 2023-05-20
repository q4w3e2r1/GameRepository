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

    public Imp(Vector2 POS, Vector2 FRAMES, int OWNERID) : base("2d\\Units\\ImpSheet", POS, new Vector2(40, 40), new Vector2(8, 1), OWNERID)
    {
        speed = 2.0f;

        frameAnimations = true;
        currentAnimation = 0;

        frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), frames, new Vector2(0, 0), 8, 66, 0, "Walk"));
    }

    public override void Update(Vector2 OFFSET, Player ENEMY, SquareGrid GRID, LevelDrawManager LEVELDRAWMANAGER)
    {
        
        base.Update(OFFSET, ENEMY, GRID, LEVELDRAWMANAGER);
        if (Math.Cos(rot) >= 0 || Math.Sin(rot) <= 0)
        {
            flipped = true;
        }
        else
        {
            flipped = false;
        }
        rot = 0;
        SetAnimationByName("Walk");
    }


    public override void Draw(Vector2 OFFSET)
    {
        base.Draw(OFFSET);
    }
}
