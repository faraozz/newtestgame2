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
    class GameObject
    {
        protected Texture2D texture;
        protected Vector2 vector;

        public GameObject(Texture2D texture, float X, float Y)
        {
            this.texture = texture;
            this.vector.X = X;
            this.vector.Y = Y;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, vector, Color.White);
        }

        public float X { get { return vector.X; } set { vector.X = value; } }
        public float Y { get { return vector.Y; } set { vector.Y = value; } }
        public float Width { get { return texture.Width; } }
        public float Height { get { return texture.Height; } }

    }
}
