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


public class Spider : Mob
{
    public GameTimer spawnTimer;

    public Spider(Vector2 POS, Vector2 FRAMES, int OWNERID) 
        : base("2d\\Units\\Mobs\\spider", POS, new Vector2(60, 60), FRAMES ,OWNERID)
    {
        speed = 1.5f;

        health = 3;
        healthMax = health;

        spawnTimer = new GameTimer(8000);
        spawnTimer.AddToTimer(4000);
    }

    public override void Update(Vector2 OFFSET, Player ENEMY, SquareGrid GRID)
    {
        spawnTimer.UpdateTimer();
        if (spawnTimer.Test())
        {
            SpawnEggSac();
            spawnTimer.ResetToZero();
        }
        base.Update(OFFSET, ENEMY, GRID);
    }

    public virtual void SpawnEggSac()
    {
        GameGlobals.PassSpawnPoint(new SpiderEggsSac(new Vector2(pos.X, pos.Y), new Vector2(1, 1), ownerId, null));
    }

    public override void Draw(Vector2 OFFSET)
    {
        base.Draw(OFFSET);
    }
}
