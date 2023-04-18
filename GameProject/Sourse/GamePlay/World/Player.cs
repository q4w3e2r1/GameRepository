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
using SharpDX.MediaFoundation;
#endregion

namespace GameProject;

public class Player
{
    public int id;
    public Hero hero;
    public List<Unit> units = new List<Unit>();
    public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

    public Player(int ID)
    {
        id = ID;

    }

    public virtual void Update(Player ENEMY, Vector2 OFFSET)
    {
        if (hero != null)
        {
            hero.Update(OFFSET);
        }

        for (var i = 0; i < spawnPoints.Count; i++)
        {
            spawnPoints[i].Update(OFFSET);
            if (spawnPoints[i].dead)
            {
                spawnPoints.RemoveAt(i);
                i--;
            }

        }

        for (var i = 0; i < units.Count; i++)
        {
            units[i].Update(OFFSET, ENEMY);

            if (units[i].dead)
            {
                ChangeScore(1);
                units.RemoveAt(i);
                i--;
            }
        }
    }

    public virtual void AddUnit(object INFO)
    {
        var tempUnit = (Unit)INFO;
        tempUnit.ownerId = id;
        units.Add((Unit)INFO);
    }

    public virtual void AddSpawnPoint(object INFO)
    {
        var tempSpawnPoint = (SpawnPoint)INFO;
        tempSpawnPoint.ownerId = id;
        spawnPoints.Add(tempSpawnPoint);
    }

    public virtual void ChangeScore(int SCORE) 
    {
        
    }

    public virtual void Draw(Vector2 OFFSET)
    {
        if(hero != null)
        {
            hero.Draw(OFFSET);
        }

        for (var i = 0; i < spawnPoints.Count; i++)
        {
            spawnPoints[i].Draw(OFFSET);
        }

        for (var i = 0; i < units.Count; i++)
        {
            units[i].Draw(OFFSET);
        }
    
    }

}
