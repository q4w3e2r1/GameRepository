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

public class KeyBind
{
    public string name, key;
    public KeyBind(string NAME, string KEY)
    {
        name = NAME;
        key = KEY;
    }

    public string Key
    {
        set
        {
            key = value;
        }
    }

    public virtual XElement ReturnXML()
    {
        var xml = new XElement("Key",
                                new XAttribute("n", name),
                                new XElement("value", key));

        return xml;
    }
}
