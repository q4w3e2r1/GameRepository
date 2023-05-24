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
#endregion

namespace GameProject
{
    public class Key
    {
        public int state;
        public string key, print, display;

        
        public Key(string key, int state)
        {
            this.key = key;
            this.state = state;
            MakePrint(this.key);
        }

        public virtual void Update()
        {
            state = 2;
        }


        public void MakePrint(string key)
        {
            display = key;

            string tempStr = "";

            if(key == "A" || key == "B" || key == "C" || key == "D" || key == "E" || key == "F" || key == "G" || key == "H" || key == "I" || key == "J" || key == "K" || key == "L" || key == "M" || key == "N" || key == "O" || key == "P" || key == "Q" || key == "R" || key == "S" || key == "T" || key == "U" || key == "V" || key == "W" || key == "X" || key == "Y" || key == "Z")
            {
                tempStr = key;
            }
            if(key == "Space")
            {
                tempStr = " ";
            }
            if(key == "OemCloseBrackets")
            {
                tempStr = "]";
                display = tempStr;
            }
            if(key == "OemOpenBrackets")
            {
                tempStr = "[";
                display = tempStr;
            }
            if(key == "OemMinus")
            {
                tempStr = "-";
                display = tempStr;
            }
            if(key == "OemPeriod" || key == "Decimal")
            {
                tempStr = ".";
            }
            if(key == "D1" || key == "D2" || key == "D3" || key == "D4" || key == "D5" || key == "D6" || key == "D7" || key == "D8" || key == "D9" || key == "D0")
            {
                tempStr = key.Substring(1);
            }
            else if(key == "NumPad1" || key == "NumPad2"  || key == "NumPad3" || key == "NumPad4" || key == "NumPad5" || key == "NumPad6" || key == "NumPad7" || key == "NumPad8" || key == "NumPad9" || key == "NumPad0")
            {
                tempStr = key.Substring(6);
            }


            print = tempStr;
        }
    }
}
