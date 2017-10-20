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
using System.Xml;

namespace newtestgame2
{
    static class GameElements
    {


        static Texture2D menuSprite;
        static Vector2 menuPos;
        static Player player;
        static Player player2;
        static List<Ammunition> ammunitions;
        static Texture2D ammunitionsprite;
        static PrintText printText;
        static Texture2D highscoresprite;
        static Texture2D gameoverSprite;
        static Vector2 highscorePos;
        static Vector2 gameoverPos;
        static List<Highscore> highscorelista;
        static XmlDocument xmlDoc;
        static XmlElement highscores;
        static XmlElement score;
        static XmlElement spelare;
        static XmlElement poäng;
        static XmlDocument xmlRead;

        //olika gamestates
        public enum State { Menu, Standard, Wild, Highscore, GameOver, Quit };
        public static State currentState;

        public static void Initialize()
        {
            xmlDoc = new XmlDocument();
            xmlRead = new XmlDocument();
            XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            highscores = xmlDoc.CreateElement("highscores");
            xmlDoc.AppendChild(highscores);
            ammunitions = new List<Ammunition>();
            highscorelista = new List<Highscore>();

            highscorelista.Add(new Highscore("Bot1", 0));
            highscorelista.Add(new Highscore("Bot2", 0));
            highscorelista.Add(new Highscore("Bot3", 0));

            xmlRead.Load("highscores.xml");
            XmlNodeList nodeList = xmlRead.SelectNodes("highscores/score");

            foreach (XmlNode b in nodeList)
            {
                //söker igenom varje nod och sparar texten och nyckeln för att sedan användas i dekrypteringsmetoden.
                string spelare = b.SelectSingleNode("spelare").InnerText;
                int poäng = int.Parse(b.SelectSingleNode("poäng").InnerText);
                Highscore h1 = new Highscore(spelare, poäng);
                //dekrypterar och presenterar den dekrypterade stringen.
                for (int i = 0; i <= 2; i++)
                {
                    if (h1.Poäng > highscorelista.ElementAt(i).Poäng)
                    {
                        highscorelista.Insert(i, h1);
                        break;
                    }
                }




                //genomför detta tills den har sökt igenom varje nod, vilket innebär att samtliga sparade meddelanden dekrypteras och presenteras.
            }




            while (highscorelista.Count() > 3)
            {
                highscorelista.Remove(highscorelista.ElementAt(highscorelista.Count() - 1));
            }

        }

        public static void LoadContent(ContentManager Content, GameWindow window)
        {
            //alla bilder laddas in

            player = new Player(Content.Load<Texture2D>("flaminhatbrasize"), 180, 200, 10f, 10f, Keys.D, Keys.A, Keys.S, Keys.W, Keys.Space, Content.Load<Texture2D>("bombbrasize"), 3, Content.Load<Texture2D>("explosionbrasize"), 0, "micke", 1000);
            player2 = new Player(Content.Load<Texture2D>("flaminhatbrasize"), 580, 200, 10f, 10f, Keys.L, Keys.J, Keys.K, Keys.I, Keys.Enter, Content.Load<Texture2D>("bombbrasize"), 3, Content.Load<Texture2D>("explosionbrasize"), 0, "opponent", 1000);
            printText = new PrintText(Content.Load<SpriteFont>("myFont"));
            ammunitionsprite = (Content.Load<Texture2D>("lootbrasize"));
            menuSprite = Content.Load<Texture2D>("menu");
            menuPos.X = window.ClientBounds.Width / 2 - menuSprite.Width / 2;
            menuPos.Y = window.ClientBounds.Height / 2 - menuSprite.Height / 2;
            highscoresprite = Content.Load<Texture2D>("highscore");
            highscorePos.X = window.ClientBounds.Width / 2 - highscoresprite.Width / 2;
            highscorePos.Y = window.ClientBounds.Height / 2 - highscoresprite.Height / 2;
            gameoverSprite = Content.Load<Texture2D>("gameover");
            gameoverPos.X = window.ClientBounds.Width / 2 - gameoverSprite.Width / 2;
            gameoverPos.Y = window.ClientBounds.Height / 2 - gameoverSprite.Height / 2;


        }

        public static State MenuUpdate()
        {
            //om användaren trycker på ett alternativ
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.S))
            {

                return State.Standard;
            }
            if (keyboardState.IsKeyDown(Keys.W))
            {

                return State.Wild;
            }
            if (keyboardState.IsKeyDown(Keys.H))
            {
                return State.Highscore;
            }

            if (keyboardState.IsKeyDown(Keys.Q))
            {
                return State.Quit;
            }
            return State.Menu;

        }

        public static void MenuDraw(SpriteBatch spriteBatch)
        {
            //ritar ut menyn

            spriteBatch.Draw(menuSprite, menuPos, Color.White);


        }

        public static State StandardUpdate(ContentManager content, GameWindow Window, GameTime gameTime)
        {
            player.IsAlive = true;
            player2.IsAlive = true;

            //updatemetoden för standard

            player.Update(Window, gameTime, player2);
            player2.Update(Window, gameTime, player);



            Random random = new Random();
            int newAmmunition = random.Next(1, 200);
            if (newAmmunition == 1)
            {
                int rndX = random.Next(0, Window.ClientBounds.Width - ammunitionsprite.Width);
                int rndY = random.Next(0, Window.ClientBounds.Height - ammunitionsprite.Height);

                ammunitions.Add(new Ammunition(ammunitionsprite, rndX, rndY, gameTime.TotalGameTime.TotalMilliseconds, gameTime));


            }




            foreach (Ammunition am in ammunitions.ToList())
            {
                if (am.IsAlive == true)
                {
                    if (am.TimeToDie + 5000 < gameTime.TotalGameTime.TotalMilliseconds)
                    {
                        ammunitions.Remove(am);
                    }

                    if (am.CheckCollision(player))
                    {
                        ammunitions.Remove(am);
                        player.Antalbomber++;

                    }

                    if (am.CheckCollision(player2))
                    {
                        ammunitions.Remove(am);
                        player2.Antalbomber++;
                    }
                }

                else
                {
                    ammunitions.Remove(am);
                }




            }

            if (!player.IsAlive)
            {
                Reset(Window, content);
                return State.GameOver;
            }
            if (!player2.IsAlive)
            {
                Reset(Window, content);
                return State.GameOver;
            }

            return State.Standard;
        }


        public static void StandardDraw(SpriteBatch spriteBatch)
        {
            //ritar ut själva spelet
            player.Draw(spriteBatch);
            player2.Draw(spriteBatch);
            foreach (Ammunition am in ammunitions)
            {
                am.Draw(spriteBatch);
            }
            printText.Print("M ConsW: " + player.Consecutivewins + " O ConsW: " + player2.Consecutivewins, spriteBatch, 0, 0);
            printText.Print("M Bomb: " + player.Antalbomber + "M Health: " + player.Health + " O Bomb: " + player2.Antalbomber + " O Health: " + player2.Health, spriteBatch, 0, 30);
        }

        public static State WildUpdate(ContentManager content, GameWindow Window, GameTime gameTime)
        {
            player.IsAlive = true;
            player2.IsAlive = true;

            //updatemetoden för wild
            player.Update(Window, gameTime, player2);
            player2.Update(Window, gameTime, player);



            Random random = new Random();
            int newAmmunition = random.Next(1, 10);
            if (newAmmunition == 1)
            {
                int rndX = random.Next(0, Window.ClientBounds.Width - ammunitionsprite.Width);
                int rndY = random.Next(0, Window.ClientBounds.Height - ammunitionsprite.Height);

                ammunitions.Add(new Ammunition(ammunitionsprite, rndX, rndY, gameTime.TotalGameTime.TotalMilliseconds, gameTime));


            }




            foreach (Ammunition am in ammunitions.ToList())
            {
                if (am.IsAlive == true)
                {
                    if (am.TimeToDie + 5000 < gameTime.TotalGameTime.TotalMilliseconds)
                    {
                        ammunitions.Remove(am);
                    }

                    if (am.CheckCollision(player))
                    {
                        ammunitions.Remove(am);
                        player.Antalbomber++;

                    }

                    if (am.CheckCollision(player2))
                    {
                        ammunitions.Remove(am);
                        player2.Antalbomber++;
                    }
                }

                else
                {
                    ammunitions.Remove(am);
                }





            }

            if (!player.IsAlive)
            {
                Reset(Window, content);
                return State.GameOver;
            }
            if (!player2.IsAlive)
            {
                Reset(Window, content);
                return State.GameOver;
            }
            return State.Wild;
        }



        public static State HighscoreUpdate(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //updatemetod för highscore
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                return State.Menu;
            }




            return State.Highscore;
        }

        public static void HighscoreDraw(SpriteBatch spriteBatch)
        {
            //ritar ut highscore
            spriteBatch.Draw(highscoresprite, highscorePos, Color.White);
            for (int i = 0; i <= 2; i++)
            {
                printText.Print(highscorelista.ElementAt(i).Spelare + ": " + highscorelista.ElementAt(i).Poäng.ToString() + Environment.NewLine, spriteBatch, 30, 90 * (i + 1));
            }
        }

        public static State GameOverUpdate()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                if (player.Jämförelsetal > player.Consecutivewins)
                {
                    for (int i = 0; i != 3; i++)
                    {
                        if (player.Jämförelsetal > highscorelista.ElementAt(i).Poäng)
                        {
                            highscorelista.Insert(i, new Highscore("Player", player.Jämförelsetal));
                            highscorelista.Remove(highscorelista.ElementAt(highscorelista.Count() - 1));
                            break;
                        }
                    }
                }
                if (player2.Jämförelsetal > player2.Consecutivewins)
                {
                    for (int i = 0; i != 3; i++)
                    {
                        if (player2.Jämförelsetal > highscorelista.ElementAt(i).Poäng)
                        {
                            highscorelista.Insert(i, new Highscore("Player2", player2.Jämförelsetal));
                            highscorelista.Remove(highscorelista.ElementAt(highscorelista.Count() - 1));
                            break;
                        }
                    }
                }

                return State.Menu;
            }

            return State.GameOver;

        }

        public static void GameOverDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(gameoverSprite, gameoverPos, Color.White);
            printText.Print(player.Loser + " lost ", spriteBatch, 300, 200);
        }

        public static State QuitUpdate()
        {

            for (int i = 0; i <= 2; i++)
            {
                score = xmlDoc.CreateElement("score");
                highscores.AppendChild(score);
                spelare = xmlDoc.CreateElement("spelare");
                spelare.InnerText = highscorelista.ElementAt(i).Spelare;
                score.AppendChild(spelare);
                poäng = xmlDoc.CreateElement("poäng");
                poäng.InnerText = highscorelista.ElementAt(i).Poäng.ToString();
                score.AppendChild(poäng);


            }
            xmlDoc.Save("highscores.xml");



            Environment.Exit(-1);


            return State.Quit;
        }

        public static void QuitDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(gameoverSprite, gameoverPos, Color.White);
            printText.Print(player.Loser + " lost ", spriteBatch, 300, 200);
        }

        public static void Reset(GameWindow window, ContentManager content)
        {
            player.Reset(180, 200, 10f, 10f);
            player2.Reset(580, 200, 10f, 10f);
        }
    }
}
