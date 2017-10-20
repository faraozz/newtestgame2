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
    abstract class PhysicalObject : MovingObject
    {
        protected static bool isAlive = true;

        public PhysicalObject(Texture2D texture, float X, float Y, float speedX, float speedY)
            : base(texture, X, Y, speedX, speedY)
        {
        }

        //kontrollerar om kollision sker (rektanglar är hitbox)
        public bool CheckCollision(PhysicalObject other)
        {
            Rectangle myRect = new Rectangle(Convert.ToInt32(X), Convert.ToInt32(Y), Convert.ToInt32(Width), Convert.ToInt32(Height));
            Rectangle otherRect = new Rectangle(Convert.ToInt32(other.X), Convert.ToInt32(other.Y), Convert.ToInt32(other.Width), Convert.ToInt32(other.Height));
            return myRect.Intersects(otherRect);
        }

        public bool IsAlive { get { return isAlive; } set { isAlive = value; } }
    }
}
