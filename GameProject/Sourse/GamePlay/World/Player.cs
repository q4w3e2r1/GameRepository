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
    public List<Unit> units = new();
    public List<SpawnPoint> spawnPoints = new();
    public List<Building> buildings = new();

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

        for (var i = 0; i < buildings.Count; i++)
        {
            buildings[i].Update(OFFSET, ENEMY);

            if (buildings[i].dead)
            {
                ChangeScore(1);
                buildings.RemoveAt(i);
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

    public virtual List<AttackableObject> GetAllObjects()
    {
        var tempObjects = new List<AttackableObject>();
        tempObjects.AddRange(units.ToList<AttackableObject>());
        tempObjects.AddRange(spawnPoints.ToList<SpawnPoint>());
        tempObjects.AddRange(buildings.ToList<Building>());

        return tempObjects;

    }

    public virtual void Draw(Vector2 OFFSET)
    {
        if(hero != null)
        {
            hero.Draw(OFFSET);
        }

        for (var i = 0; i < buildings.Count; i++)
        {
            buildings[i].Draw(OFFSET);
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
