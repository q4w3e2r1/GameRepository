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
using static System.Reflection.Metadata.BlobBuilder;
#endregion

namespace GameProject;

public class SkillMenu
{
    public bool active;

    Animated2d bkg;

    Skill selectedSkill;

    Hero hero;

    public SkillMenu(Hero HERO)
    {
        bkg = new Animated2d("2d\\Misc\\solidSkills", new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2), new Vector2(300, 400), new Vector2(1, 1), Color.Gray);

        hero = HERO;
    }

    public virtual void Update()
    {
        if(active)
        {
            for(var i =0; i < hero.skills.Count; i++)
            {
                if (hero.skills[i].icon.Hover(bkg.pos - bkg.dims / 2 + new Vector2(30 + (i % 5) * 50, 30 + (i / 5) * 50)))
                {
                    if(Globals.mouse.LeftClick())
                    {
                        selectedSkill = hero.skills[i];
                        selectedSkill.icon.color = Color.Blue;
                    }
                }
                else
                {
                    hero.skills[i].icon.color = Color.White;
                }
            }

            if (Globals.keyboard.GetSinglePress("D1"))
            {
                hero.skillBar.slots[0].skillButton = new SkillButton("2d\\Misc\\solid", new Vector2(0, 0), new Vector2(40, 40), new Vector2(1, 1), hero.SetSkill, selectedSkill);
            }

            if (Globals.keyboard.GetSinglePress("D2"))
            {
                hero.skillBar.slots[1].skillButton = new SkillButton("2d\\Misc\\solid", new Vector2(0, 0), new Vector2(40, 40), new Vector2(1, 1), hero.SetSkill, selectedSkill);
            }

            if (Globals.keyboard.GetSinglePress("D3"))
            {
                hero.skillBar.slots[2].skillButton = new SkillButton("2d\\Misc\\solid", new Vector2(0, 0), new Vector2(40, 40), new Vector2(1, 1), hero.SetSkill, selectedSkill);
            }

            if (Globals.keyboard.GetSinglePress("D4"))
            {
                hero.skillBar.slots[3].skillButton = new SkillButton("2d\\Misc\\solid", new Vector2(0, 0), new Vector2(40, 40), new Vector2(1, 1), hero.SetSkill, selectedSkill);
            }

        }
    }

    public virtual void ToggleActive()
    {
        active = !active;
        selectedSkill = null;
    }


    public virtual void Draw()
    {
        if(active)
        {
            bkg.Draw(Vector2.Zero);

            for (var i = 0; i < hero.skills.Count; i++)
            {
                hero.skills[i].icon.Draw(bkg.pos - bkg.dims / 2 + new Vector2(30 +(i%5)*50, 30 + (i/5) * 50));
            }
        }
    }
}
