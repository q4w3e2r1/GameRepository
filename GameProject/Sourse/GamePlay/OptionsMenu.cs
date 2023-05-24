#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
#endregion
namespace GameProject;

public class OptionsMenu
{
    Button2d exitBtn;

    public List<FormPart> formParts = new();

    SpriteFont font;

    PassObject ApplyOptions;

    public OptionsMenu(PassObject APPLYOPTION)
    {
        ApplyOptions = APPLYOPTION;

        exitBtn = new Button2d("2d\\Misc\\SimpleBtn", new Vector2(Globals.screenWidth/2, Globals.screenHeight - 100), new Vector2(150, 100), new Vector2(1, 1), "Fonts\\Arial16", "Exit", ExitClick, null);

        font = Globals.content.Load<SpriteFont>("Fonts\\Arial16");

        ArrowSelector tempSelector = null;



        formParts.Add(new CheckBox(new Vector2(Globals.screenWidth / 2, 300), new Vector2(128, 32), "Full Screen", null));


        tempSelector = new ArrowSelector(new Vector2(Globals.screenWidth / 2, 400), new Vector2(128, 32), "Music Volume", null);
        for(var i = 0; i < 31; i++)
        {
            tempSelector.AddOption(new FormOption("" + i, i));
        }
        tempSelector.selected = (int)tempSelector.options.Count / 2;
        formParts.Add(tempSelector);


        tempSelector = new ArrowSelector(new Vector2(Globals.screenWidth / 2, 500), new Vector2(128, 32), "Sound Volume", null);
        for (var i = 0; i < 31; i++)
        {
            tempSelector.AddOption(new FormOption("" + i, i));
        }
        tempSelector.selected = (int)tempSelector.options.Count / 2;
        formParts.Add(tempSelector);

        XDocument xml = Globals.save.GetFile("XML\\options.xml");

        LoadData(xml);
    }

    public virtual void Update()
    {

        for(var i =0; i < formParts.Count;i++)
        {
            formParts[i].Update(Vector2.Zero);
        }

        exitBtn.Update(Vector2.Zero);
    }

    public virtual void ExitClick(object INFO)
    {
        SaveOptions();


        Globals.gameState = 0;
    }

    public virtual FormOption GetOptionValue(string NAME)
    {
        for(var i =0; i< formParts.Count; i++)
        {
            if (formParts[i].title == NAME)
            {
                return formParts[i].GetCurrentOption();
            }
        }

        return null;
    }

    public virtual void LoadData(XDocument DATA)
    {
        if(DATA != null)
        {

            var allOptions = new List<string>();

            for(var i =0; i < formParts.Count; i++)
            {
                allOptions.Add(formParts[i].title);
            }


            for (var i =0; i < allOptions.Count; i++)
            {
                var optionList = (from t in DATA.Element("Root").Element("Options").Descendants("Option")
                                  where t.Element("name").Value == allOptions[i]
                                  select t).ToList<XElement>();

                if (optionList.Count > 0)
                {
                    for (var j = 0; j < formParts.Count; j++)
                    {
                        if (formParts[j].title == allOptions[i])
                        {
                            formParts[j].LoadData(optionList[0]);
                        }
                    }
                }
            }
        }
    }

    public virtual void SaveOptions()
    {
        var xml = new XDocument(new XElement("Root",
                                              new XElement("Options", "")));

        for(var i =0; i< formParts.Count; i++)
        {
            xml.Element("Root").Element("Options").Add(formParts[i].ReturnXML());
        }

        Globals.save.HandleSaveFormates(xml, "options.xml");

        ApplyOptions(null);
    }

    public virtual void Draw()
    {
        var strDims = font.MeasureString("Options");

        Globals.spriteBatch.DrawString(font, "Options", new Vector2(Globals.screenWidth/2 - strDims.X/2, 100), Color.White);

        exitBtn.Draw(Vector2.Zero);

        for (var i = 0; i < formParts.Count; i++)
        {
            formParts[i].Draw(Vector2.Zero, font);
        }

    }
}
