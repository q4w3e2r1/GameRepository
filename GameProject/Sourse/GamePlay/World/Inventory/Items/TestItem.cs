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
public class TestItem : InventoryItem
{

    public TestItem() : base("2d\\Loot\\items\\OffhandIcon1", new Vector2(0, 0), new Vector2(32, 32), new Vector2(1, 1), Color.White)
    {

    }

}
