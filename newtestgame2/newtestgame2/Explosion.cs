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
    class Explosion : PhysicalObject //explosion som sker efter att bomben dör.
    {
        //tiden som explosionen är kvar som man kan bli träffad utav.
        double explosionTimeToDie;

        //konstruktor
        public Explosion(Texture2D texture, float X, float Y, float speedX, float speedY, double explosionTimeToDie)
            : base(texture, X, Y, 0, 0)
        {
            this.explosionTimeToDie = explosionTimeToDie;
        }

        public double ExplosionTimeToDie
        {
            get { return explosionTimeToDie; }
            set { explosionTimeToDie = value; }
        }
    }
}
