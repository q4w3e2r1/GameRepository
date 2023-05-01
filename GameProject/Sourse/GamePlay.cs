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
using SharpDX.Direct3D11;
using SharpDX.MediaFoundation;
#endregion

namespace GameProject;

public class GamePlay
{
    int playState;
    World world;

    PassObject ChangeGameState;
    public GamePlay(PassObject CHANGEGAMESTATE) 
    {
        playState = 0;
        ChangeGameState = CHANGEGAMESTATE;
        ResetWorld(null);
       
    }

    public virtual void Update()
    {
        if(playState == 0)
        {
            world.Update();
        }
    }

    public virtual void ResetWorld(object INFO)
    {
        world = new World(ResetWorld, ChangeGameState);
    }

    public void Draw()
    {
        if (playState == 0)
        {
            world.Draw(Vector2.Zero);
        }
    }
}
