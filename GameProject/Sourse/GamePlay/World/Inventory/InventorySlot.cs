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
public class InventorySlot : Animated2d
{
    public InventoryItem item;

    public InventorySlot(Vector2 POS, Vector2 DIMS) : base("2d\\Misc\\solidSkills", POS, DIMS, new Vector2(1, 1), Color.White)
    {

    }

    public override void Update(Vector2 OFFSET)
    {
        base.Update(OFFSET);

        if (item != null)
        {
            item.Update(OFFSET);
        }

        if(Globals.dragAndDropPacket != null && Globals.dragAndDropPacket.type == "InventoryItem" && Globals.dragAndDropPacket.IsDropped() && Hover(OFFSET))
        {
            TransferItem((InventoryItem)Globals.dragAndDropPacket.item);
        }
    }

    public virtual void TransferItem(InventoryItem ITEM)
    {
        if(ITEM != null)
        {
            var oldSlot = ITEM.slot;

            var currentItem = item;

            ITEM.slot = this;

            item = ITEM;

            if(currentItem != null)
            {
                currentItem.slot = oldSlot;
            }

            if(oldSlot != null)
            {
                oldSlot.item = currentItem;
            }
        }
    }

    public override void Draw(Vector2 OFFSET)
    {
       

        base.Draw(OFFSET);

        if (item != null)
        {
            item.Draw(OFFSET);
        }
    }

}
