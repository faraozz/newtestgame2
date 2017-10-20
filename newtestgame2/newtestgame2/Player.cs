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
    class Player : PhysicalObject
    {
        //tangenttryckningar.
        Keys right;
        Keys left;
        Keys down;
        Keys up;
        Keys shoot;
        //lista med bomber och bombens textur.
        List<Bomb> bombs;
        Texture2D bombTexture;
        //Tiden sedan senaste bomben skjöt.
        double timeSinceLastBomb = 0;
        //Hur många skott spelaren har. Default 0.
        int antalbomber = 0;
        //lista med explosioner och explosionernas textur.
        List<Explosion> explosions;
        Texture2D explosiontexture;

        //namn och vilken spelar som dog (loser) används för att kunna veta vem som dog i GameElements.
        string name;
        string loser;
        //hur många vinster har spelaren i rad.
        int consecutivewins;
        //jämförelsetal som används tillsammans med consectuive wins. förklaras senare i klassen.
        int jämförelsetal;

        int health;

        //konstruktor.
        public Player(Texture2D texture, float X, float Y, float speedX, float speedY, Keys right, Keys left, Keys down, Keys up, Keys shoot, Texture2D bombTexture, int antalbomber, Texture2D explosiontexture, int dödantal, string name, int health) : base(texture, X, Y, speedX, speedY)
        {

            this.right = right;
            this.left = left;
            this.down = down;
            this.up = up;
            this.shoot = shoot;
            //skapa lista med bomber
            bombs = new List<Bomb>();

            this.bombTexture = bombTexture;
            this.antalbomber = antalbomber;
            this.explosiontexture = explosiontexture;
            //skapa lista med explosioner.
            explosions = new List<Explosion>();
            this.name = name;
            this.health = health;


        }

        public Keys Right
        {
            get { return right; }
            set { right = value; }
        }

        public Keys Left
        {
            get { return left; }
            set { left = value; }
        }
        public Keys Down
        {
            get { return down; }
            set { down = value; }
        }

        public Keys Up
        {
            get { return up; }
            set { up = value; }
        }

        public Keys Shoot
        {
            get { return shoot; }
            set { shoot = value; }
        }

        public int Antalbomber
        {
            get { return antalbomber; }
            set { antalbomber = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Loser
        {
            get { return loser; }
            set { loser = value; }
        }
        public int Consecutivewins
        {
            get { return consecutivewins; }
            set { consecutivewins = value; }
        }
        public int Jämförelsetal
        {
            get { return jämförelsetal; }
            set { jämförelsetal = value; }
        }
        public int Health
        {
            get { return health; }
            set { health = value; }
        }
        //player.update med player opponent som används för kollisionshantering senare.
        public void Update(GameWindow window, GameTime gameTime, Player opponent)
        {

            KeyboardState keyboardState = Keyboard.GetState();

            // så länge som spelaren inte åker ut ur fönstret så kan spelaren förflyttas.
            if (vector.X <= window.ClientBounds.Width - texture.Width && vector.X >= 0)
            {

                if (keyboardState.IsKeyDown(right))
                {

                    vector.X += speed.X;

                }
                if (keyboardState.IsKeyDown(left))
                {
                    vector.X -= speed.X;

                }
            }

            if (vector.Y <= window.ClientBounds.Height - texture.Height && vector.Y >= 0)
            {
                if (keyboardState.IsKeyDown(down))
                { vector.Y += speed.Y; }
                if (keyboardState.IsKeyDown(up))
                { vector.Y -= speed.Y; }
            }


            //spelaren vill skjuta
            if (keyboardState.IsKeyDown(shoot))
            {
                //skjuter 5 ggr per sek max
                if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastBomb + 200 && antalbomber > 0)
                {
                    //skottet framkallas på en slumpmässig sida utav spelaren.
                    Random random = new Random();
                    int bombspawn = random.Next(1, 5);

                    if (bombspawn == 1)
                    {
                        Bomb temp = new Bomb(bombTexture, vector.X + 25 + texture.Width / 2, vector.Y, 0, 0, gameTime.TotalGameTime.TotalMilliseconds);
                        bombs.Add(temp);
                        timeSinceLastBomb = gameTime.TotalGameTime.TotalMilliseconds;
                        antalbomber--;
                    }
                    else if (bombspawn == 2)
                    {
                        Bomb temp = new Bomb(bombTexture, vector.X - 25 - texture.Width / 2, vector.Y, 0, 0, gameTime.TotalGameTime.TotalMilliseconds);
                        bombs.Add(temp);
                        timeSinceLastBomb = gameTime.TotalGameTime.TotalMilliseconds;
                        antalbomber--;
                    }
                    else if (bombspawn == 3)
                    {
                        Bomb temp = new Bomb(bombTexture, vector.X, vector.Y + 40 + texture.Height / 2, 0, 0, gameTime.TotalGameTime.TotalMilliseconds);
                        bombs.Add(temp);
                        timeSinceLastBomb = gameTime.TotalGameTime.TotalMilliseconds;
                        antalbomber--;
                    }
                    else if (bombspawn == 4)
                    {
                        Bomb temp = new Bomb(bombTexture, vector.X, vector.Y - 25 - texture.Height / 2, 0, 0, gameTime.TotalGameTime.TotalMilliseconds);
                        bombs.Add(temp);
                        timeSinceLastBomb = gameTime.TotalGameTime.TotalMilliseconds;
                        antalbomber--;
                    }



                }
            }

            //så att spriten inte åker ut ur spelet
            if (vector.X < 0)
            { vector.X = 0; }
            if (vector.X > window.ClientBounds.Width - texture.Width)
            { vector.X = window.ClientBounds.Width - texture.Width; }
            if (vector.Y < 0)
            { vector.Y = 0; }
            if (vector.Y > window.ClientBounds.Height - texture.Height)
            { vector.Y = window.ClientBounds.Height - texture.Height; }

            //bomberna finns kvar i 1.05 sekunder innan de försvinner.
            foreach (Bomb b in bombs.ToList())
            {
                if (b.IsAlive == true)
                {
                    if (b.BombTimeToDie + 1050 < gameTime.TotalGameTime.TotalMilliseconds)
                    {
                        Explosion temp = new Explosion(explosiontexture, b.X, b.Y, 0, 0, gameTime.TotalGameTime.TotalMilliseconds);
                        explosions.Add(temp);
                        bombs.Remove(b);


                    }

                    if (CheckCollision(b))
                    {
                        if (keyboardState.IsKeyDown(down))
                        {
                            b.SpeedY = speed.Y;
                        }
                        if (keyboardState.IsKeyDown(up))
                        {
                            b.SpeedY = -speed.Y;
                        }
                        if (keyboardState.IsKeyDown(right))
                        {
                            b.SpeedX = speed.X;
                        }
                        if (keyboardState.IsKeyDown(left))
                        {
                            b.SpeedX = -speed.X;
                        }

                        if (opponent.CheckCollision(b))
                        {
                            if (keyboardState.IsKeyDown(down))
                            {
                                b.SpeedY = speed.Y;
                            }
                            if (keyboardState.IsKeyDown(up))
                            {
                                b.SpeedY = -speed.Y;
                            }
                            if (keyboardState.IsKeyDown(right))
                            {
                                b.SpeedX = speed.X;
                            }
                            if (keyboardState.IsKeyDown(left))
                            {
                                b.SpeedX = -speed.X;
                            }
                        }


                    }

                    b.X += b.SpeedX;
                    b.Y += b.SpeedY;

                }

                if (b.IsAlive == false)
                {
                    bombs.Remove(b);
                }
            }


            foreach (Explosion e in explosions.ToList())
            {
                if (e.IsAlive == true)
                {
                    if (e.ExplosionTimeToDie + 200 < gameTime.TotalGameTime.TotalMilliseconds)
                    {
                        explosions.Remove(e);
                    }

                    if (CheckCollision(e))
                    {
                        if (health <= 50)
                        {
                            jämförelsetal = consecutivewins;

                            consecutivewins = 0;
                            opponent.consecutivewins++;
                            loser = name;
                            isAlive = false;
                            explosions.Remove(e);
                        }
                        else
                        {
                            health -= 50;
                            explosions.Remove(e);
                        }


                    }

                    if (opponent.CheckCollision(e))
                    {
                        if (opponent.Health <= 50)
                        {
                            opponent.Jämförelsetal = opponent.Consecutivewins;
                            opponent.Consecutivewins = 0;
                            consecutivewins++;
                            loser = opponent.Name;
                            opponent.IsAlive = false;
                            explosions.Remove(e);
                        }
                        else
                        {
                            opponent.Health -= 50;
                            explosions.Remove(e);
                        }


                    }
                }

                else
                {
                    explosions.Remove(e);
                }

            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, vector, Color.White);
            foreach (Bomb b in bombs)
            {
                b.Draw(spriteBatch);
            }

            foreach (Explosion e in explosions)
            {
                e.Draw(spriteBatch);
            }
        }

        public void Reset(float X, float Y, float speedX, float speedY)
        {
            vector.X = X;
            vector.Y = Y;
            speed.X = speedX;
            speed.Y = speedY;
            antalbomber = 3;
            bombs.Clear();
            timeSinceLastBomb = 0;
            isAlive = true;
            health = 1000;

        }





    }
}
