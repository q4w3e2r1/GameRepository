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
using static System.ComponentModel.Design.ObjectSelectorEditor;
#endregion

namespace GameProject;

public class FormPart
{
    public bool showTitle;

    public string title;

    public Vector2 pos, dims;

    public PassObject Changed;

    public FormPart(Vector2 POS, Vector2 DIMS, string TITLE, PassObject CHANGED)
    {
        title = TITLE;

        pos = POS;
        dims = DIMS;

        Changed = CHANGED;

        showTitle = true;
    }

    public virtual void Update(Vector2 OFFSET)
    {
    }

    public virtual FormOption GetCurrentOption()
    {
        return null;
    }

    public virtual void LoadData(XElement DATA)
    {
        if(DATA != null)
        {

        }
    }

    public virtual XElement ReturnXML()
    {
        var xml = new XElement("Option",
                                new XElement("name", title));

        return xml;
    }

    public virtual void Draw(Vector2 OFFSET, SpriteFont FONT)
    {

    }
}
