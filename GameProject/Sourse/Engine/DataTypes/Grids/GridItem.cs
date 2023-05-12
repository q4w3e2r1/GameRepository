#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
#endregion
namespace GameProject;

public class GridItem : Animated2d
{
    public GridItem(string PATH, Vector2 POS, Vector2 DIMS, Vector2 FRAMES) : base(PATH, POS, DIMS, FRAMES, Color.White)
    {
        dims = new Vector2(120, 120);
    }

}
