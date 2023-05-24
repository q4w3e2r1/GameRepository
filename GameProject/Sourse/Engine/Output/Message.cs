﻿#region Includes
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

public class Message
{
    public bool done, lockScreen;
    public Vector2 pos, dims;
    public Color color;
    public TextZone textZone;

    public GameTimer timer;

    public Message(Vector2 POS, Vector2 DIMS, string MSG, int MSEC, Color COLOR, bool LOCKSCREEN)
    {
        pos = POS;
        dims = DIMS;
        color = COLOR;
        done = false;
        lockScreen = LOCKSCREEN;


        textZone = new TextZone(new Vector2(0, 0), MSG, (int)(dims.X * 0.9f), 22, "Fonts\\Arial16", COLOR);

        timer = new GameTimer(MSEC);
    }

    public virtual void Update()
    {
        timer.UpdateTimer();
        if(timer.Test())
        {
            done = true;
        }

        textZone.color = color * (float)(.9f * (float)(timer.MSec- (float)timer.Timer) / (float)(timer.MSec));
    }

    public virtual void Draw()
    {


        textZone.Draw(new Vector2(pos.X - textZone.dims.X/2, pos.Y));
    }
}
