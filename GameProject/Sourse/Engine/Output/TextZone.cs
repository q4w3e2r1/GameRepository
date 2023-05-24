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

public class TextZone
{
    public int maxWidth, lineHieght;
    string str;
    public Vector2 pos, dims;
    public Color color;

    public SpriteFont font;

    public List<string> lines = new List<string>();

    public TextZone(Vector2 POS, string STR, int MAXWIDTH, int LINEHEIGTH, string FONT, Color FONTCOLOR)
    {
        pos = POS;
        str = STR;
        maxWidth = MAXWIDTH;
        lineHieght = LINEHEIGTH;
        color = FONTCOLOR;

        font = Globals.content.Load<SpriteFont>(FONT);

        if(str !="")
        {
            ParseLines();
        }
    }

    #region Proporties
    
    public string Str
    {
        get { return str; }
        set
        {
            str = value;
            ParseLines();
        }
    }

    #endregion

    public virtual void ParseLines()
    {
        lines.Clear();

        var worldList = new List<string>();
        var tempString = "";

        var largestWidth = 0;
        var currentWidth = 0;

        if (str != "" && str != null)
        {
            worldList = str.Split(' ').ToList<string>();

            for (var i = 0; i < worldList.Count; i++)
            {
                if (tempString != "")
                {
                    tempString += " ";
                }

                currentWidth = (int)(font.MeasureString(tempString + worldList[i]).X);

                if (currentWidth > largestWidth && currentWidth <= maxWidth)
                {
                    largestWidth = currentWidth;
                }

                if (currentWidth <= maxWidth)
                {
                    tempString += worldList[i];
                }
                else
                {
                    lines.Add(tempString);
                    tempString = worldList[i];
                }
            }

            if(tempString != "")
            {
                lines.Add(tempString);
            }

            SetDims(largestWidth);
        }
    }

    public virtual void SetDims(int LARGESTWIDTH)
    {
        dims = new Vector2( LARGESTWIDTH, lineHieght * lines.Count );
    }

    public virtual void Draw(Vector2 OFFSET)
    {
        for(var i =0; i < lines.Count; i++)
        {
            Globals.spriteBatch.DrawString(font, lines[i], OFFSET + new Vector2(pos.X, pos.Y + (lineHieght * i)), color);
        }
    }
}
