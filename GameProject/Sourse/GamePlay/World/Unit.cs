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

    public class Unit : AttackableObject
    {
        public Skill currentSkill;

        protected Vector2 moveTo;

        protected List<Vector2> pathNodes = new();
        public List<Skill> skills = new();
        public List<InventorySlot> inventorySlots = new();

        public Unit(string path, Vector2 POS, Vector2 DIMS, Vector2 FRAMES, int OWNERID)
            : base(path, POS, DIMS,  FRAMES, OWNERID)
        {
            moveTo = new Vector2(POS.X, POS.Y); 
        }

        public override void Update(Vector2 OFFSET, Player ENEMY, SquareGrid GRID, LevelDrawManager LEVELDRAWMANAGER)
        {
           
            base.Update(OFFSET, ENEMY, GRID, LEVELDRAWMANAGER);
        }

        public virtual void AddToInventory(object INFO)
        {
            // inventorySlots.Add((InventoryItem)INFO);
            var added = false;

            for(var i=0; i < inventorySlots.Count;i++)
            {
                if (inventorySlots[i].item == null)
                {
                    var tempItem = (InventoryItem)INFO;
                    tempItem.slot = inventorySlots[i];

                    inventorySlots[i].item = tempItem;
                    added = true;
                    break;
                }
            }

            if(!added)
            {

            }
                
        }

        public virtual List<Vector2> FindPath(SquareGrid GRID, Vector2 ENDSLOT)
        {
            pathNodes.Clear();

            var tempSrartSlot = GRID.GetSlotFromPixel(pos, Vector2.Zero);

            var tempPath = GRID.GetPath(tempSrartSlot, ENDSLOT, true);

            if(tempPath == null || tempPath.Count == 0)
            {

            }

            return tempPath;
        }

        public virtual void MoveUnit()
        {
           if(pos.X != moveTo.X || pos.Y != moveTo.Y)
           {
                rot = Globals.RotateTowards(pos, moveTo);

                pos += Globals.RadialMovement(moveTo, pos, speed);
           }
           else if(pathNodes.Count > 0)
            {
                moveTo = pathNodes[0];
                pathNodes.RemoveAt(0);

                pos += Globals.RadialMovement(moveTo, pos, speed);
           }
            
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
