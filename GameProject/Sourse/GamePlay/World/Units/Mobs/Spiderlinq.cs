#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

    public Spiderlinq(Vector2 POS, Vector2 FRAMES, int OWNERID) 
        : base("2d\\Units\\SpiderSheet", POS, new Vector2(30, 30), new Vector2(4, 1), OWNERID)
    {
        speed = 2.1f;
        frameAnimations = true;
        currentAnimation = 0;

        frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), frames, new Vector2(0, 0), 4, 100, 0, "Walk"));

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
        SetAnimationByName("Walk");
    }

    public override void AI(Player ENEMY, SquareGrid GRID)
    {
        Building temp = null;
        
        for(var i = 0; i < ENEMY.buildings.Count; i++)
        {
            if (ENEMY.buildings[i].GetType().ToString() == "GameProject.Tower" ||
                ENEMY.buildings[i].GetType().ToString() == "GameProject.Player")
            {
                temp = ENEMY.buildings[i];
            }
        }

        if (temp != null)
        {
            if (pathNodes == null || (pathNodes.Count == 0 && pos.X == moveTo.X && pos.Y == moveTo.Y))
            {
                pathNodes = FindPath(GRID, GRID.GetSlotFromPixel(temp.pos, Vector2.Zero));
                moveTo = pathNodes[0];
                pathNodes.RemoveAt(0);
            }
            else
            {

                MoveUnit();


                if (Globals.GetDistance(pos, temp.pos) < GRID.slotDims.X * 1.2f)
                {
                    temp.GetHit(this, 1);
                    dead = true;
                }
            }
        }
        else
        {
            rePathTimer.UpdateTimer();

            if (pathNodes == null || (pathNodes.Count == 0 && pos.X == moveTo.X && pos.Y == moveTo.Y) || rePathTimer.Test())
            {
                if (!currentlyPathing)
                {
                    var repathTask = new Task(() =>
                    {
                        currentlyPathing = true;

                        pathNodes = FindPath(GRID, GRID.GetSlotFromPixel(ENEMY.hero.pos, Vector2.Zero));

                        pathNodes.RemoveAt(0);

                        rePathTimer.ResetToZero();

                        currentlyPathing = false;

                    });

                    repathTask.Start();
                }
            }
            else
            {

                MoveUnit();


                if (Globals.GetDistance(pos, ENEMY.hero.pos) < GRID.slotDims.X * 1.2f)
                {
                    ENEMY.hero.GetHit(this, 1);
                    dead = true;
                }
            }
        }

    }

    

    public override void Draw(Vector2 OFFSET)
    {
        base.Draw(OFFSET);
    }
}
