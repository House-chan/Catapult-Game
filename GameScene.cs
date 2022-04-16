using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Catapult.GameObjects;

namespace Catapult
{
    public class GameScene : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Ship ship;
        enum Stage
        {
            MainMenu, Stage, Pause
        }

        enum Turn
        {
            Player, Enemy
        }

        Turn stage;

        Texture2D PlayerShip, Planet, EnemyShip, guideline, meteorite, gun;
        Ship Player;

        public GameScene()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = Singleton.SCREENWIDTH;
            _graphics.PreferredBackBufferHeight = Singleton.SCREENHEIGHT;
            _graphics.ApplyChanges();

            stage = Turn.Player;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            PlayerShip = Content.Load<Texture2D>("PlayerShip");
            //Planet = Content.Load<Texture2D>("");
            //EnemyShip = Content.Load<Texture2D>("");
            //guideline = Content.Load<Texture2D>("");
            //meteorite = Content.Load<Texture2D>("");
            //gun = Content.Load<Texture2D>("");

            Player = new Ship(PlayerShip);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            //switch()
            Player.Update(gameTime);


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            Player.Draw(_spriteBatch);

            _spriteBatch.End();
            _graphics.BeginDraw();
            base.Draw(gameTime);
        }
    }
}
