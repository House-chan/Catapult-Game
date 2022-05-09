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

        Texture2D PlayerShip, EnemyShip, guideline, meteorite, gun;
        Texture2D[] Planet = new Texture2D[11], Bullet = new Texture2D[7];
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
            PlayerShip = Content.Load<Texture2D>("Ship/PlayerShip");
            Planet[0] = Content.Load<Texture2D>("Planet/barren");
            Planet[1] = Content.Load<Texture2D>("Planet/barren-charred");
            Planet[2] = Content.Load<Texture2D>("Planet/barren-icy");
            Planet[3] = Content.Load<Texture2D>("Planet/chlorine");
            Planet[4] = Content.Load<Texture2D>("Planet/chlorine-barren");
            Planet[5] = Content.Load<Texture2D>("Planet/desert");
            Planet[6] = Content.Load<Texture2D>("Planet/inferno");
            Planet[7] = Content.Load<Texture2D>("Planet/methane");
            Planet[8] = Content.Load<Texture2D>("Planet/methane-barren");
            Planet[9] = Content.Load<Texture2D>("Planet/methane-ice");
            Planet[10] = Content.Load<Texture2D>("Planet/tundra");
            EnemyShip = Content.Load<Texture2D>("Ship/EnemyShip");
            //guideline = Content.Load<Texture2D>("");
            //meteorite = Content.Load<Texture2D>("");
            gun = Content.Load<Texture2D>("Ship/PlayerCanon");
            Bullet[0] = Content.Load<Texture2D>("Bullet/bullet1");
            Bullet[1] = Content.Load<Texture2D>("Bullet/bullet2");
            Bullet[2] = Content.Load<Texture2D>("Bullet/bullet3");
            Bullet[3] = Content.Load<Texture2D>("Bullet/bullet4");


            Player = new Ship(PlayerShip, gun, Bullet[0]);
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
