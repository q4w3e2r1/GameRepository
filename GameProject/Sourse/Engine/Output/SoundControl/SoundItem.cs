#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameProject;
#if WINDOWS_PHONE
using Microsoft.Xna.Framework.Input.Touch;
#endif
using Microsoft.Xna.Framework.Media;
#endregion

namespace GameProject;

public class SoundItem
{
    public float volume;
    public string name;
    public SoundEffect sound;
    public SoundEffectInstance instance;

    public SoundItem(string NAME, string SOUNDPATH, float VOLUME)
    {
        name = NAME;
        volume = VOLUME;
        sound = Globals.content.Load<SoundEffect>(SOUNDPATH);
        CreateInstance();
    }

    public virtual void CreateInstance()
    {
        instance = sound.CreateInstance();
    }

}
