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
    WorldMap worldMap;

    PassObject ChangeGameState;
    public GamePlay(PassObject CHANGEGAMESTATE) 
    {
        playState = 1;
        ChangeGameState = CHANGEGAMESTATE;
        ResetWorld(null);


        worldMap = new WorldMap(LoadLevel);
    }

    public virtual void Update()
    {
        if(playState == 0)
        {
            world.Update();
        }

        else if (playState == 1)
        {
            worldMap.Update();
        }
    }

    public virtual void ChangPlayState(object INFO)
    {
       playState = Convert.ToInt32(INFO, Globals.culture);


    }

    public virtual void LoadLevel(object INFO)
    {
        playState = 0;

        int tempLevel = Convert.ToInt32(INFO, Globals.culture);

        Globals.msgList.Add(new Message(new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2), new Vector2(200, 60), "Level: " + tempLevel, 3500, Color.Black, false));


        world = new World(ResetWorld, Convert.ToInt32(INFO, Globals.culture), ChangeGameState, ChangPlayState);
    }

    public virtual void ResetWorld(object INFO)
    {
        var levelId = 1;
        if(world != null)
        {
            levelId = world.levelId;
        }

        world = new World(ResetWorld, levelId, ChangeGameState, ChangPlayState);
    }

    public void Draw()
    {
        if (playState == 0)
        {
            world.Draw(Vector2.Zero);
        }

        else if (playState == 1)
        {
            worldMap.Draw();
        }
    }
}
