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

namespace GameProject;

public class User : Player
{
 

    public User(int ID, XElement DATA) : base(ID, DATA)
    {
       // hero = new Hero("2d\\NormalHero", new Vector2(300, 300), new Vector2(64, 64), id);

        //buildings.Add(new Tower(new Vector2(Globals.screenWidth / 2, Globals.screenHeight - 40), id));

    }

    public override void Update(Player ENEMY, Vector2 OFFSET, SquareGrid GRID)
    {
        base.Update(ENEMY, OFFSET, GRID);

        if (Globals.keyboard.GetSinglePress("T"))
        {

            if (gold >= 10)
            {
                var tempLoc = GRID.GetSlotFromPixel(new Vector2(hero.pos.X - 10, hero.pos.Y - 50), Vector2.Zero);
                var loc = GRID.GetSlotFromLocation(tempLoc);

                if (loc != null && !loc.filled && !loc.impassable)
                {
                    loc.SetToFilled(false);
                    var tempBuilding = new ArrowTower(new Vector2(0, 0), new Vector2(1, 1), id);
                    tempBuilding.pos = GRID.GetPosFromLoc(tempLoc) + GRID.slotDims / 2 + new Vector2(0, -tempBuilding.dims.Y * .25f);

                    GameGlobals.PassBuilding(tempBuilding);
                    gold -= 10;
                }
            }
        }
    }
}
