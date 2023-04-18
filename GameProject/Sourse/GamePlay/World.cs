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
#endregion

namespace GameProject;

public class World
{

    public Vector2 offset;

    public UI ui;

    public User user;
    public AIPlayer aIPlayer;

    public List<Projectile2d> projectiles = new();

    PassObject ResetWorld;
    public World(PassObject resetWorld)
    {
        this.ResetWorld = resetWorld;
      

        GameGlobals.PassProjectile = AddProjectile;
        GameGlobals.PassMob = AddMob;
        GameGlobals.PassSpawnPoint = AddSpawnPoint;
        GameGlobals.CheckScroll = CheckScroll;

        user = new User(1);
        aIPlayer = new AIPlayer(2);


        offset = new Vector2(0, 0);


        ui = new UI();
        ResetWorld = resetWorld;
    }

    public virtual void Update()
    {
        if(!user.hero.dead)
        {

            user.Update(aIPlayer, offset);
            aIPlayer.Update(user, offset);


            for (var i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Update(offset, aIPlayer.units.ToList<Unit>());

                if (projectiles[i].done)
                {
                    projectiles.RemoveAt(i);
                    i--;
                }
            }


        

            ui.Update(this);

        }
        else
        {
            if (Globals.keyboard.GetPress("Enter"))
            {
                ResetWorld(null);
            }
        }
    }

    public virtual void AddMob(object INFO)
    {
        var tempUnit = (Unit)INFO;

        if(user.id == tempUnit.ownerId)
        {
            user.AddUnit(tempUnit);
        }
        else if(aIPlayer.id == tempUnit.ownerId)
        {
            aIPlayer.AddUnit(tempUnit);
        }


        aIPlayer.AddUnit((Mob)INFO);
    }


    public virtual void AddProjectile(object INFO)
    {
        projectiles.Add((Projectile2d)INFO);
    }

    public virtual void AddSpawnPoint(object INFO)
    {
        var tempSpawnPoint = (SpawnPoint)INFO;

        if (user.id == tempSpawnPoint.ownerId)
        {
            user.AddSpawnPoint(tempSpawnPoint);
        }
        else if (aIPlayer.id == tempSpawnPoint.ownerId)
        {
            aIPlayer.AddSpawnPoint(tempSpawnPoint);
        }

    }

    public virtual void CheckScroll(object INFO)
    {
        var tempPos = (Vector2)INFO;

        if(tempPos.X < -offset.X + (Globals.screenWidth * .4f))
        {
            offset = new Vector2(offset.X + user.hero.speed * 2, offset.Y );
        }

        if (tempPos.X > -offset.X + (Globals.screenWidth * .6f))
        {
            offset = new Vector2(offset.X - user.hero.speed * 2, offset.Y);
        }

        if (tempPos.Y < -offset.Y + (Globals.screenHeight * .4f))
        {
            offset = new Vector2(offset.X, offset.Y + user.hero.speed * 2 );
        }

        if (tempPos.Y > -offset.Y + (Globals.screenHeight * .6f))
        {
            offset = new Vector2(offset.X, offset.Y - user.hero.speed * 2);
        }
    }

    public virtual void Draw(Vector2 OFFSET)
    {
  
        user.Draw(offset);
        aIPlayer.Draw(offset);

        for (var i = 0; i < projectiles.Count; i++)
        {
            projectiles[i].Draw(offset);
        }

        ui.Draw(this);
    }
}
