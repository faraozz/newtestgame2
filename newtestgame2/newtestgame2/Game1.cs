using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace newtestgame2
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            graphics.IsFullScreen = true;
            GameElements.currentState = GameElements.State.Menu;
            GameElements.Initialize();

            base.Initialize();
        }


        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            GameElements.LoadContent(Content, Window);
        }


        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            switch (GameElements.currentState)
            {
                case GameElements.State.Standard:
                    GameElements.currentState = GameElements.StandardUpdate(Content, Window, gameTime);
                    break;
                case GameElements.State.Wild:
                    GameElements.currentState = GameElements.WildUpdate(Content, Window, gameTime);
                    break;
                case GameElements.State.Highscore:
                    GameElements.currentState = GameElements.HighscoreUpdate(gameTime, spriteBatch);
                    break;
                case GameElements.State.GameOver:
                    GameElements.currentState = GameElements.GameOverUpdate();
                    break;
                case GameElements.State.Quit:
                    GameElements.currentState = GameElements.QuitUpdate();
                    break;

                default: //menyn
                    GameElements.currentState = GameElements.MenuUpdate();
                    break;
            }


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            switch (GameElements.currentState)
            {
                case GameElements.State.Standard:
                    GameElements.StandardDraw(spriteBatch);
                    break;
                case GameElements.State.Wild:
                    GameElements.StandardDraw(spriteBatch);
                    break;
                case GameElements.State.Highscore:
                    GameElements.HighscoreDraw(spriteBatch);
                    break;
                case GameElements.State.GameOver:
                    GameElements.GameOverDraw(spriteBatch);
                    break;
                case GameElements.State.Quit:
                    GameElements.QuitDraw(spriteBatch);
                    break;

                default: //menyn 
                    GameElements.MenuDraw(spriteBatch);
                    break;
            }
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
