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

namespace GameProject
{
    public class GameGlobals
    {
        public static bool paused = false;

        public static int score = 0;
        public static PassObject PassProjectile, PassMob, PassBuilding, PassSpawnPoint, CheckScroll;

    }
}

