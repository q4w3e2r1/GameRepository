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


public class Mob : Unit
{
    public bool currentlyPathing;

    public GameTimer rePathTimer = new(200);

    public Mob(string path, Vector2 POS, Vector2 DIMS, Vector2 FRAMES, int OWNERID) : base(path, POS, DIMS, FRAMES, OWNERID)
    {
        currentlyPathing = false;
        speed = 2.0f;
    }

    public override void Update(Vector2 OFFSET, Player ENEMY, SquareGrid GRID)
    {
        AI(ENEMY, GRID);
        base.Update(OFFSET, ENEMY, GRID);
    }


    public virtual void AI(Player ENEMY, SquareGrid GRID)
    {    

       // rot = 0;   
       rePathTimer.UpdateTimer();

        if (pathNodes == null || (pathNodes.Count == 0 && pos.X == moveTo.X && pos.Y == moveTo.Y) || rePathTimer.Test())
        {
            if(!currentlyPathing)
            {
                var repathTask = new Task(() =>
                {
                    currentlyPathing = true;

                    pathNodes = FindPath(GRID, GRID.GetSlotFromPixel(ENEMY.hero.pos, Vector2.Zero));
                    //if (pathNodes.Count <= 0)

                    //try
                    //{ moveTo = pathNodes[0];
                        pathNodes.RemoveAt(0);

                        rePathTimer.ResetToZero();

                        currentlyPathing = false;
                    //}
                    //catch(ArgumentOutOfRangeException)
                    //{
                        
                    //}                
                });

                repathTask.Start();
            }
        }
        else
        {

            MoveUnit();


            if (Globals.GetDistance(pos, ENEMY.hero.pos) < GRID.slotDims.X * 1.2f)
            {
                ENEMY.hero.GetHit(this,1);
                dead = true;
            }
        }

    }

  


    public override void Draw(Vector2 OFFSET)
    {
        base.Draw(OFFSET);
    }
}
