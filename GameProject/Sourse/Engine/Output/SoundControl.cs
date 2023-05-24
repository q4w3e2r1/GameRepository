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
#endregion

namespace GameProject;

public class SoundControl
{

    public SoundItem bkgMusic;

    public List<SoundItem> sounds = new();

    public SoundControl(string MUSICPATH)
    {
        if(MUSICPATH != "")
        {
            ChangeMusic(MUSICPATH);
        }
    }

    public virtual void AddSound(string NAME, string SOUNDPATH, float VOLUME)
    {
        sounds.Add( new SoundItem(NAME, SOUNDPATH, VOLUME));
    }

    public virtual void AdjustVolume(float PERCENT)
    {
        if(bkgMusic.instance != null)
        {
            bkgMusic.instance.Volume = PERCENT * bkgMusic.volume;
        }
    }

    public virtual void ChangeMusic(string MUSICPATH)
    {
        bkgMusic = new SoundItem("Bkg Music", MUSICPATH, .25f);
        bkgMusic.CreateInstance();

        var musicVolume = Globals.optionsMenu.GetOptionValue("Music Volume");
        var musicVolumePercent = 1.0f;
        if(musicVolume != null)
        {
            musicVolumePercent = (float)Convert.ToDecimal(musicVolume.value, Globals.culture)/30.0f;
        }

        AdjustVolume(musicVolumePercent);
        bkgMusic.instance.IsLooped = true;
        bkgMusic.instance.Play(); 
    }

    public virtual void PlaySound(string NAME)
    {
        for(var i =0; i < sounds.Count; i++)
        {
            if (sounds[i].name == NAME)
            {
                sounds[i].CreateInstance();
                RunSound(sounds[i].sound, sounds[i].instance, sounds[i].volume);
            }
        }
    }

    public virtual void RunSound(SoundEffect SOUND, SoundEffectInstance INSTANCE, float VOLUME)
    {
        var soundVolume = Globals.optionsMenu.GetOptionValue("Sound Volume");
        var soundVolumePercent = 1.0f;
        if (soundVolume != null)
        {
            soundVolumePercent = (float)Convert.ToDecimal(soundVolume.value, Globals.culture) / 30.0f;
        }

        INSTANCE.Volume = soundVolumePercent * VOLUME;
        INSTANCE.Play();
    }

}
