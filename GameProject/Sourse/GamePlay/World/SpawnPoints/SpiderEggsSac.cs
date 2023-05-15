#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
#endregion

namespace GameProject
{

    public class SpiderEggsSac : SpawnPoint
    {

        int maxSpawns, totalSpawns;

        public SpiderEggsSac(Vector2 POS, Vector2 FRAMES, int OWNERID, XElement DATA) 
            : base("2d\\Misc\\eggs", POS, new Vector2(45, 45), FRAMES, OWNERID, DATA)
        {
            totalSpawns = 0;
            maxSpawns = 4;

            health = 5;
            healthMax = health;

            killValue = 2;

            spawnTimer = new GameTimer(3000);
        }

        public override void Update(Vector2 OFFSET, Player ENEMY, SquareGrid GRID)
        {



            base.Update(OFFSET, ENEMY, GRID);
        }


        public override void SpawnMob()
        {
            var tempMob = new Spiderlinq(new Vector2(pos.X, pos.Y), new Vector2(1, 1), ownerId);

            if(tempMob != null)
            {
                GameGlobals.PassMob(tempMob);

                totalSpawns++;
                if(totalSpawns >= maxSpawns)
                {
                    dead = true;
                }
            }       
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
