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
#endregion

namespace GameProject;

public class ArrowSelector : FormPart
{
    public int selected;

 

    public List<Button2d> buttons = new();

    public List<FormOption> options = new();

    public ArrowSelector(Vector2 POS, Vector2 DIMS, string TITLE, PassObject CHANGED) 
        : base(POS, DIMS, TITLE, CHANGED)
    {
        
        selected = 0;

        buttons.Add(new Button2d("2d\\Misc\\ArrowLeft", new Vector2(-dims.X/2 + dims.Y/2, 0), new Vector2(dims.Y, dims.Y), new Vector2(1, 1), "Fonts\\Arial16", "", ArrowLeftClick, null));
        buttons.Add(new Button2d("2d\\Misc\\ArrowRight", new Vector2(dims.X / 2 - dims.Y / 2, 0), new Vector2(dims.Y, dims.Y), new Vector2(1, 1), "Fonts\\Arial16", "", ArrowRightClick, null));

    }

    public override void Update(Vector2 OFFSET)
    {
        for(var i = 0; i < buttons.Count; i++)
        {
            buttons[i].Update(OFFSET + pos);
        }
    }

    public virtual void AddOption(FormOption FORMOPRION)
    {
        options.Add(FORMOPRION);
    }

    public virtual void ArrowLeftClick(object INFO)
    {
        selected--;

        if(selected < 0)
        {
            selected = 0;
        }
    }

    public virtual void ArrowRightClick(object INFO)
    {
        selected++;

        if (selected >= options.Count)
        {
            selected = options.Count - 1;
        }
    }

    public override FormOption GetCurrentOption()
    {
        return options[selected];
    }

    public override void LoadData(XElement DATA)
    {
        if (DATA != null)
        {
            if (DATA.Element("selected") != null)
            {
                selected = Convert.ToInt32(DATA.Element("selected").Value, Globals.culture);
            }
        }
    }

    public override XElement ReturnXML()
    {
        var xml = new XElement("Option",
                                new XElement("name", title),
                                new XElement("selected", selected),
                                new XElement("selectedName", options[selected].name));

        return xml;
    }

    public override void Draw(Vector2 OFFSET, SpriteFont FONT)
    {
        for (var i = 0; i < buttons.Count; i++)
        {
            buttons[i].Draw(OFFSET + pos);
        }

        Globals.normalEffect.Parameters["xSize"].SetValue(1.0f);
        Globals.normalEffect.Parameters["ySize"].SetValue(1.0f);
        Globals.normalEffect.Parameters["xDraw"].SetValue(1.0f);
        Globals.normalEffect.Parameters["yDraw"].SetValue(1.0f);
        Globals.normalEffect.Parameters["filterColor"].SetValue(Color.White.ToVector4());
        Globals.normalEffect.CurrentTechnique.Passes[0].Apply();


        if (showTitle && options.Count > selected && selected >= 0)
        {
            Vector2 strDims = FONT.MeasureString(options[selected].name);

            Globals.spriteBatch.DrawString(FONT, options[selected].name, OFFSET + pos + new Vector2(- strDims.X / 2, -strDims.Y/2), Color.White);

            strDims = FONT.MeasureString(title+": ");

            Globals.spriteBatch.DrawString(FONT, title, OFFSET + pos + new Vector2(-dims.X/2 - strDims.X - 10, -strDims.Y / 2), Color.White);

        }
    }
}
