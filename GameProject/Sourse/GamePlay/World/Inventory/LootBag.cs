#region Includes
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using NUnit.Framework;
#endregion

namespace GameProject;

public class LootBag : Animated2d
{

    public bool done;

    public List<InventoryItem> items = new List<InventoryItem>();
    public LootBag(string PATH, Vector2 POS, List<InventoryItem> ITEMS)
        : base(PATH, POS, new Vector2(20, 20), new Vector2(1, 1), Color.White)
    {
        done = false;

        if (ITEMS != null)
        {
            items = ITEMS;
        }
    }

    public override void Update(Vector2 OFFSET)
    {


        if(Globals.mouse.LeftClick() && Hover(OFFSET))
        {
            for(var i=0; i < items.Count; i++)
            {
                GameGlobals.AddInventory(items[i]);
            }

            done = true;
        }
    }
}
