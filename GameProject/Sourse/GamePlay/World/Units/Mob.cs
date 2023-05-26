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
    public bool currentlyPathing, isAttacking;

    public float attackRange;

    public GameTimer rePathTimer = new(200), attackTimer = new GameTimer(350);

    public Mob(string path, Vector2 POS, Vector2 DIMS, Vector2 FRAMES, int OWNERID) : base(path, POS, DIMS, FRAMES, OWNERID)
    {
        attackRange = 50;
        isAttacking = false;
        currentlyPathing = false;
        speed = 2.0f;
    }

    public override void Update(Vector2 OFFSET, Player ENEMY, SquareGrid GRID, LevelDrawManager LEVELDRAWMANAGER)
    {
        AI(ENEMY, GRID);
        base.Update(OFFSET, ENEMY, GRID, LEVELDRAWMANAGER);
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
                    if (pathNodes != null && pathNodes.Count > 0)
                    {
                        pathNodes.RemoveAt(0);
                    }
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
                ENEMY.hero.GetHit(this,1);
                dead = true;
            }
        }

    }

    public override void GetHit(AttackableObject ATTACKER, float DAMAGE)
    {
        if (ATTACKER.ownerId != ownerId)
        {

            health -= DAMAGE;
            throbing = true;

            if (health <= 0)
            {
                dead = true;
                GameGlobals.PassGold(new PlayerValuePacket(ATTACKER.ownerId, killValue));

                int num = Globals.random.Next(0, 2 + 1);

                if(num == 0)
                {
                    LootBag tempBag = new LootBag("2d\\Loot\\LootBag", new Vector2(pos.X, pos.Y), null);
                    tempBag.items.Add(new TestItem());

                    GameGlobals.PassLootBag(tempBag);
                }
            }
        }
    }


    public override void Draw(Vector2 OFFSET)
    {
        base.Draw(OFFSET);
    }
}
