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
    class PrintText //skriver text
    {
        SpriteFont font;
        

        public PrintText(SpriteFont font)
        {
            this.font = font;
        }

        public void Print(string text, SpriteBatch spriteBatch, int X, int Y)
        {

            spriteBatch.DrawString(font, text, new Vector2(X, Y), Color.White);
            
        }

        public void Print(string text, SpriteBatch spriteBatch, float X, float Y)
        {
            
            spriteBatch.DrawString(font, text, new Vector2(X, Y), Color.White);
        }
    }
}
