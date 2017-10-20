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
    class Bomb : PhysicalObject //klassen för bomb som är spelarnas skott.
    {
        //tiden som bomben är kvar som man kan bli träffad utav.
        double bombTimeToDie;

        public Bomb(Texture2D texture, float X, float Y, float speedX, float speedY, double bombTimeToDie)
            : base(texture, X, Y, 0, 0)
        {
            this.bombTimeToDie = bombTimeToDie;
        }



        public double BombTimeToDie
        {
            get { return bombTimeToDie; }
            set { bombTimeToDie = value; }
        }

        public float SpeedX
        {
            get { return speed.X; }
            set { speed.X = value; }
        }

        public float SpeedY
        {
            get { return speed.Y; }
            set { speed.Y = value; }
        }
    }
}
