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

    public class Button2d : Animated2d
    {
        public bool isPressed, isHovered;
        public string text;

        public Color hoverColor;

        public SpriteFont font;

        public object info;
        public PassObject ButtonClicked;

        public Button2d(string path, Vector2 POS, Vector2 DIMS, Vector2 FRAMES, string FONTPATH, string TEXT, PassObject BUTTONCLICKED, object INFO) 
            : base(path, POS, DIMS, FRAMES, Color.White)
        {
            info = INFO;
            text = TEXT;
            ButtonClicked = BUTTONCLICKED;

            if(FONTPATH != "")
            {
                font = Globals.content.Load<SpriteFont>(FONTPATH);
            }

            isPressed = false;
            hoverColor = new Color(200, 230, 255);

            CreatePerFrameAnimation();
            frameAnimations = true;
        }

        public override void Update(Vector2 OFFSET)
        {
            if(Hover(OFFSET))
            {
                isHovered = true;
                if (Globals.mouse.LeftClick())
                {
                    isHovered  =false;
                    isPressed = true;
                }
                else if (Globals.mouse.LeftClickRelease()) 
                {
                    RunBtnClick();
                }
               
            }
            else
            {
                isHovered = false;
            }

            if(!Globals.mouse.LeftClick() && !Globals.mouse.LeftClickHold())
            {
                isPressed = false;
            }

            base.Update(OFFSET);
        }

        public virtual void Reset()
        {
            isPressed = false;
            isHovered = false;
        }

        public virtual void RunBtnClick()
        {
            if(ButtonClicked != null)
            {
                ButtonClicked(info);
            }

            Reset();
        }

        public override void Draw(Vector2 OFFSET)
        {
            var tempColor = Color.White;
            if(isPressed)
            {
                tempColor = Color.Gray;
            }
            else if(isHovered)
            {
                tempColor = hoverColor;
            }

            Globals.normalEffect.Parameters["xSize"].SetValue((float)model.Bounds.Width);
            Globals.normalEffect.Parameters["ySize"].SetValue((float)model.Bounds.Height);
            Globals.normalEffect.Parameters["xDraw"].SetValue((float)((int)dims.X));
            Globals.normalEffect.Parameters["yDraw"].SetValue((float)((int)dims.Y));
            Globals.normalEffect.Parameters["filterColor"].SetValue(tempColor.ToVector4());
            Globals.normalEffect.CurrentTechnique.Passes[0].Apply();


            base.Draw(OFFSET);

            if (font != null) {
                var strDims = font.MeasureString(text);
                Globals.spriteBatch.DrawString(font, text,
                   pos + OFFSET + new Vector2(-strDims.X / 2, -strDims.Y / 2), Color.Blue);
            }
        }
    }
}
