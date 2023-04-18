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

namespace GameProject
{

    public class SpiderEggsSac : SpawnPoint
    {

        int maxSpawns, totalSpawns;

        public SpiderEggsSac(Vector2 POS, int OWNERID) : base("2d\\Misc\\eggs", POS, new Vector2(45, 45), OWNERID)
        {
            totalSpawns = 0;
            maxSpawns = 4;
        }

        public override void Update(Vector2 OFFSET)
        {



            base.Update(OFFSET);
        }


        public override void SpawnMob()
        {
            var tempMob = new Spiderlinq(new Vector2(pos.X, pos.Y), ownerId);

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
