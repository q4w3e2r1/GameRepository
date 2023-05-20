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

namespace GameProject
{

    public class AttackableObject : SceneItem
    {
        public bool dead;

        public int ownerId, killValue;

        public float speed, hitDist, health, healthMax;

        public AttackableObject(string path, Vector2 POS, Vector2 DIMS,Vector2 FRAMES, int OWNERID)
            : base(path, POS, DIMS, FRAMES, new Vector2(1, 1))
        {
            ownerId = OWNERID;
            dead = false;
            speed = 2.0f;

            health = 1;
            healthMax = health;

            killValue = 1;

            hitDist = 35.0f;
        }

        public virtual void Update(Vector2 OFFSET, Player ENEMY, SquareGrid GRID, LevelDrawManager LEVELDRAWMANAGER)
        {
           
            base.Update(OFFSET, LEVELDRAWMANAGER);
        }

        public virtual void GetHit(AttackableObject ATTACKER, float DAMAGE)
        {
            health -= DAMAGE;

            if (health <= 0)
            {
                dead = true;
                GameGlobals.PassGold(new PlayerValuePacket(ATTACKER.ownerId, killValue));
            }
        }

        public override void Draw(Vector2 OFFSET)
        {
            Globals.normalEffect.Parameters["xSize"].SetValue((float)model.Bounds.Width);
            Globals.normalEffect.Parameters["ySize"].SetValue((float)model.Bounds.Height);
            Globals.normalEffect.Parameters["xDraw"].SetValue((float)((int)dims.X));
            Globals.normalEffect.Parameters["yDraw"].SetValue((float)((int)dims.Y));
            Globals.normalEffect.Parameters["filterColor"].SetValue(Color.White.ToVector4());
            Globals.normalEffect.CurrentTechnique.Passes[0].Apply();


            base.Draw(OFFSET);
        }
    }
}
