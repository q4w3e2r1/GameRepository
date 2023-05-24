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

namespace GameProject
{
    public class Animated2d : Basic2d
    {
        public Vector2 frames;
        public List<FrameAnimation> frameAnimationList = new();
        public Color color;
        public bool frameAnimations;
        public int currentAnimation = 0;

        public Animated2d(string PATH, Vector2 POS, Vector2 DIMS, Vector2 FRAMES, Color COLOR) : base(PATH, POS, DIMS)
        {
            Frames = new Vector2(FRAMES.X, FRAMES.Y);

            color = COLOR;
        }

        #region Properties
        public Vector2 Frames
        {
            set
            {
                frames = value;
                if (model != null)
                {
                    frameSize = new Vector2(model.Bounds.Width / frames.X, model.Bounds.Height / frames.Y);
                }
            }
            get
            {
                return frames;
            }
        }
        #endregion


        public override void Update(Vector2 OFFSET)
        {
            if (frameAnimations && frameAnimationList != null && frameAnimationList.Count > currentAnimation)
            {
                frameAnimationList[currentAnimation].Update();
            }

            base.Update(OFFSET);
        }

        public virtual void CreatePerFrameAnimation()
        {
            for(var j = 0; j < frames.Y; j++)
            {
                for(var i =0; i < frames.X; i++)
                {
                    frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), new Vector2(1, 1), new Vector2(i, j), 1, 100, 0, ""));
                }
            }
        }

        public virtual int GetAnimationFromName(string ANIMATIONNAME)
        {
            for (int i = 0; i < frameAnimationList.Count; i++)
            {
                if (frameAnimationList[i].name == ANIMATIONNAME)
                {
                    return i;
                }
            }

            return -1;
        }

        public virtual void SetAnimationByName(string NAME)
        {
            int tempAnimation = GetAnimationFromName(NAME);

            if (tempAnimation != -1)
            {
                if (tempAnimation != currentAnimation)
                {
                    frameAnimationList[tempAnimation].Reset();
                }

                currentAnimation = tempAnimation;

            }
        }

        public override void Draw(Vector2 screenShift)
        {

            if (frameAnimations && frameAnimationList[currentAnimation].Frames > 0)
            {
               // Globals.spriteBatch.Draw(model, new Rectangle((int)(pos.X+screenShift.X), (int)(pos.Y+screenShift.Y), (int)dims.X, (int)dims.Y), new Rectangle((int)((currentFrame.X-1)*dims.X), (int)((currentFrame.Y-1)*dims.Y), (int)(currentFrame.X*dims.X), (int)(currentFrame.Y*dims.Y)), color, rot, new Vector2(myModel.Bounds.Width/2, myModel.Bounds.Height/2), new SpriteEffects(), 0);
               
               if(flipped)
                    frameAnimationList[currentAnimation].Draw(model, dims, frameSize, screenShift, pos, rot, color, SpriteEffects.FlipHorizontally);
                else
                    frameAnimationList[currentAnimation].Draw(model, dims, frameSize, screenShift, pos, rot, color, new SpriteEffects());

            }
            else
            {
                base.Draw(screenShift);
            }
        }


    }
}
