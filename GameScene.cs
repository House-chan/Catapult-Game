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
        enum Stage
        {
            MainMenu, Stage, Pause
        }
        Stage Game;
        enum Turn
        {
            Player, Enemy
        }

        Turn turn;

        Texture2D PlayerShip, EnemyShip, guideline, meteorite, gun, EnemyGun, AimGuideTexture;
        Texture2D[] PlanetTexture = new Texture2D[11], Bullet = new Texture2D[7];
        Ship Player;
        EnemyShip Enemy;
        Planet Planet;
        GravityDemo Gravity;
        AimGuide AimGuide;
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

            turn = Turn.Player;
            Game = Stage.Stage;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            PlayerShip = Content.Load<Texture2D>("Ship/PlayerShip");
            PlanetTexture[0] = Content.Load<Texture2D>("Planet/barren");
            PlanetTexture[1] = Content.Load<Texture2D>("Planet/barren-charred");
            PlanetTexture[2] = Content.Load<Texture2D>("Planet/barren-icy");
            PlanetTexture[3] = Content.Load<Texture2D>("Planet/chlorine");
            PlanetTexture[4] = Content.Load<Texture2D>("Planet/chlorine-barren");
            PlanetTexture[5] = Content.Load<Texture2D>("Planet/desert");
            PlanetTexture[6] = Content.Load<Texture2D>("Planet/inferno");
            PlanetTexture[7] = Content.Load<Texture2D>("Planet/methane");
            PlanetTexture[8] = Content.Load<Texture2D>("Planet/methane-barren");
            PlanetTexture[9] = Content.Load<Texture2D>("Planet/methane-ice");
            PlanetTexture[10] = Content.Load<Texture2D>("Planet/tundra");
            EnemyShip = Content.Load<Texture2D>("Ship/EnemyShip");
            //guideline = Content.Load<Texture2D>("");
            //meteorite = Content.Load<Texture2D>("");
            gun = Content.Load<Texture2D>("Ship/PlayerCanon");
            EnemyGun = Content.Load<Texture2D>("Ship/EnemyCanon");
            Bullet[0] = Content.Load<Texture2D>("Bullet/bullet1");
            Bullet[1] = Content.Load<Texture2D>("Bullet/bullet2");
            Bullet[2] = Content.Load<Texture2D>("Bullet/bullet3");
            Bullet[3] = Content.Load<Texture2D>("Bullet/bullet4");
            AimGuideTexture = Content.Load<Texture2D>("Bullet/bullet4");


            Player = new Ship(PlayerShip, gun, Bullet[0]);
            Enemy = new EnemyShip(EnemyShip, EnemyGun);
            Planet = new Planet(PlanetTexture[0]);
            Gravity = new GravityDemo();
            AimGuide = new AimGuide(Player, Gravity, AimGuideTexture);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            

            // TODO: Add your update logic here
            switch (Game)
            {
                case Stage.MainMenu:
                    break;
                case Stage.Stage:
                    switch (turn)
                    {
                        case Turn.Player:
                            Player.Update(gameTime);
                            switch(Player.stage)
                            {
                                case GameObjects.Ship.Stage.EndTurn:
                                    turn = Turn.Enemy;
                                    break;
                            }
                            break;
                        case Turn.Enemy:
                            Enemy.Update(gameTime);
                            switch(Enemy.stage)
                            {
                                case GameObjects.EnemyShip.Stage.EndTurn:
                                    turn = Turn.Player;
                                    break;
                            }
                            break;
                    }
                    break;

                case Stage.Pause:
                    break;
            }
            Gravity.Update(Player.gun.bullet);
            AimGuide.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            switch (Game)
            {
                case Stage.MainMenu:
                    break;
                case Stage.Stage:
                    Player.Draw(_spriteBatch);
                    Enemy.Draw(_spriteBatch);
                    break;
                case Stage.Pause:

                    break;
            }
            AimGuide.Draw(_spriteBatch);
            _spriteBatch.End();
            _graphics.BeginDraw();
            base.Draw(gameTime);
        }
    }
}
