#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

public class KeyBindList
{
    public List<KeyBind> keyBinds = new();

    public KeyBindList(XDocument XML)
    {
        var bindsXML = (from t in XML.Descendants("Key")
                        select t).ToList<XElement>();

        for (var i = 0; i < bindsXML.Count; i++)
        {
            keyBinds.Add(new KeyBind(bindsXML[i].Attribute("n").Value, bindsXML[i].Element("value").Value));


        }
    }

    public virtual string GetKeyName(string NAME)
    {
        for(var i=0; i<keyBinds.Count;i++)
        {
            if (keyBinds[i].name == NAME)
            {
                return keyBinds[i].key;
            }
        }

        return "";
    }

    public virtual XElement ReturnXML()
    {
        var xml = new XElement("Keys", "");

        for(var i=0;i < keyBinds.Count;i++)
        {
            xml.Add(keyBinds[i].ReturnXML());
        }

        return xml;
    }

}
