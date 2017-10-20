using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace newtestgame2
{
    class Highscore //klass för highscore.
    {
        string spelare;
        int poäng;
        public Highscore(string spelare, int poäng)
        {
            this.spelare = spelare;
            this.poäng = poäng;
        }

        public string Spelare
        {
            get { return spelare; }
            set { spelare = value; }
        }
        public int Poäng
        {
            get { return poäng; }
            set { poäng = value; }
        }
    }
}
