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
using SharpDX.DirectWrite;
#endregion

namespace GameProject
{

    public class Portal : SpawnPoint
    {

        public Portal(Vector2 POS, Vector2 FRAMES, int OWNERID, XElement DATA) 
            : base("2d\\Misc\\portal", POS, new Vector2(75, 75), FRAMES, OWNERID, DATA)
        {
            health = 10;
            healthMax = health;
            killValue = 5;
        }

        public override void Update(Vector2 OFFSET, Player ENEMY, SquareGrid GRID, LevelDrawManager LEVELDRAWMANAGER)
        {
           
            base.Update(OFFSET, ENEMY, GRID, LEVELDRAWMANAGER);
        }


        public override void SpawnMob()
        {
            var num = Globals.random.Next(0, 100 + 1);

            Mob tempMob = null;
            var total = 0;


            for (var i = 0; i < mobChoices.Count;i++)
            {
                total += mobChoices[i].rate;

                if (num < total)
                {

                    var sType = Type.GetType("GameProject." + mobChoices[i].mobStr, true);

                    tempMob = (Mob)(Activator.CreateInstance(sType, new Vector2(pos.X, pos.Y), new Vector2(1, 1), ownerId));



                    break;
                }
            }
           

            if(tempMob != null)
            {
                GameGlobals.PassMob(tempMob);
            }          
        }
    }
}
