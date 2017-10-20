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
    class Ammunition : PhysicalObject
    {
        //hur länge ska den finnas kvar
        double timeToDie;


        public Ammunition(Texture2D texture, float X, float Y, double timeToDie, GameTime gameTime)
            : base(texture, X, Y, 0, 0)
        {
            this.timeToDie = timeToDie;

        }

        public double TimeToDie
        {
            get { return timeToDie; }
            set { timeToDie = value; }
        }



    }
}
