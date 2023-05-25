#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using GameProject;
using static System.Reflection.Metadata.BlobBuilder;
#endregion

namespace GameProject;

public class CharacterMenu : Menu2d
{
    public Hero hero;

    public TextZone textZone;

    public CharacterMenu(Hero HERO)
        : base(new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2), new Vector2(600, 800), null)
    {
        hero = HERO;

        textZone = new TextZone(new Vector2(0, 0), "Zed was born in a small village on the border of Ionia and Noxus. His parents were simple farmers and couldn't provide him with a good education at the local school. However, when he showed incredible agility and stealth during street games, local thieves saw potential in him and started training him in the art of theft and deception.As Zed grew up, he joined the Shadow Assassins clan led by his teacher and mentor, Master Kusho. Here he diligently trained in the art of killing and became one of the most superior assassins in the clan.", (int)(dims.X * .9f), 22, "Fonts\\Arial16", Color.Gray );
    }

    public override void Update()
    {
        base.Update();

        for (var i = 0; i < hero.inventorySlots.Count; i++)
        {
            var tempVec = new Vector2(150 + 54 * (i % 6), 400 + 54 * (i / 6));
            hero.inventorySlots[i].Update(topLeft + tempVec);
        }
    }

    public override void Draw()
    {
        base.Draw();

        if(Active)
        {
            Globals.normalEffect.Parameters["xSize"].SetValue(1.0f);
            Globals.normalEffect.Parameters["ySize"].SetValue(1.0f);
            Globals.normalEffect.Parameters["xDraw"].SetValue(1.0f);
            Globals.normalEffect.Parameters["yDraw"].SetValue(1.0f);
            Globals.normalEffect.Parameters["filterColor"].SetValue(Color.Gray.ToVector4());
            Globals.normalEffect.CurrentTechnique.Passes[0].Apply();

            var tempStr = "" + hero.name;
            var strDims = font.MeasureString(tempStr);
            Globals.spriteBatch.DrawString(font, tempStr, topLeft + new Vector2(bkg.dims.X/2 - strDims.X/2, 40), Color.Black);

            textZone.Draw(topLeft + new Vector2(10, 100));

            for(var i=0; i < hero.inventorySlots.Count;i++)
            {
                var tempVec =new Vector2(150 + 54 * (i%6), 400 + 54 * (i/6));
                hero.inventorySlots[i].Draw(topLeft + tempVec);
            }
        }
    }
}
