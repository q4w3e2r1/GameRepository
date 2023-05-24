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
using SharpDX.Direct3D9;
#endregion

namespace GameProject;

public class WorldMap
{

    public Basic2d bkg;

    public List<Button2d> levels = new();

    PassObject ChangeGameState;

    public WorldMap(PassObject CHANGEGAMESTATE)
    {
        ChangeGameState = CHANGEGAMESTATE;

        bkg = new Basic2d("2d\\UI\\BackGrounds\\WorldMap", new Vector2(Globals.screenWidth/2, Globals.screenHeight/2), new Vector2(Globals.screenWidth, Globals.screenHeight));

        LoadData();    
    
    }

    public virtual void Update()
    {


        for(var i = 0; i< levels.Count;i++)
        {
            levels[i].Update(Vector2.Zero);
        }
    }

    public virtual void LevelClicked(object INFO)
    {
        ChangeGameState(INFO);
   }

    public virtual void LoadData()
    {
        var xml = XDocument.Load("XML\\Levels.xml");

        var levelList = (from t in xml.Descendants("Level")
                              select t).ToList<XElement>();

        for(var i = 0; i < levelList.Count;i++)
        {
            levels.Add(new Button2d("2d\\Misc\\solidLevels", new Vector2(Convert.ToInt32(levelList[i].Element("Pos").Element("x").Value, Globals.culture), Convert.ToInt32(levelList[i].Element("Pos").Element("y").Value, Globals.culture)), new Vector2(80, 80), new Vector2(1, 1), "Fonts\\Arial16", levelList[i].Attribute("id").Value, LevelClicked, levelList[i].Attribute("id").Value));
        }
    }

    public virtual void Draw()
    {
        bkg.Draw(Vector2.Zero);

        for (var i = 0; i < levels.Count; i++)
        {
            levels[i].Draw(Vector2.Zero);
        }
    }
}
