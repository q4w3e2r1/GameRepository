﻿#region Includes
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


public class Spider : Mob
{
    public GameTimer spawnTimer;

    public Spider(Vector2 POS, Vector2 FRAMES, int OWNERID) 
        : base("2d\\Units\\SpiderSheet", POS, new Vector2(50, 50), new Vector2(4, 1) ,OWNERID)
    {
        speed = 1.5f;

        health = 3;
        healthMax = health;

        killValue = 3;

        frameAnimations = true;
        currentAnimation = 0;

        frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), frames, new Vector2(0, 0), 4, 100, 0, "Walk"));

        spawnTimer = new GameTimer(8000);
        spawnTimer.AddToTimer(4000);
    }

    public override void Update(Vector2 OFFSET, Player ENEMY, SquareGrid GRID, LevelDrawManager LEVELDRAWMANAGER)
    {
        spawnTimer.UpdateTimer();
        if (spawnTimer.Test())
        {
            SpawnEggSac();
            spawnTimer.ResetToZero();
        }
        base.Update(OFFSET, ENEMY, GRID, LEVELDRAWMANAGER);
        if (Math.Cos(rot) >= 0 || Math.Sin(rot) <= 0)
        {
            flipped = true;
        }
        else
        {
            flipped = false;
        }
        SetAnimationByName("Walk");
    }

    public virtual void SpawnEggSac()
    {
        GameGlobals.PassSpawnPoint(new SpiderEggsSac(new Vector2(pos.X, pos.Y), new Vector2(1, 1), ownerId, null));
    }
}
