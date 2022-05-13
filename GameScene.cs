using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Catapult.GameObjects;
using System.Collections.Generic;

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

        Texture2D PlayerShip, EnemyShip, guideline, meteorite, gun, EnemyGun, HealthBar;
        Texture2D[] PlanetTexture = new Texture2D[11], Bullet = new Texture2D[7];
        Ship Player;
        //EnemyShip Enemy;
        List<EnemyShip> Enemy = new List<EnemyShip>();
        List<Planet> Planet = new List<Planet>();

        List<Vector2> EnemyPosition = new List<Vector2>();
        List<Vector2> PlanetPosition = new List<Vector2>();

        SpriteFont font;

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

            font = Content.Load<SpriteFont>("Font");

            HealthBar = new Texture2D(_graphics.GraphicsDevice, 100, 5);
            Color[] data = new Color[100 * 5];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = Color.White;
            }
            HealthBar.SetData(data);



            Player = new Ship(PlayerShip, gun, Bullet[0]);
            Player.SetPosition(new Vector2(100, 500));
            //Enemy = new EnemyShip(EnemyShip, EnemyGun);
            Enemy.Add(new EnemyShip(EnemyShip, EnemyGun, Bullet[0]));
            Enemy[0].SetPosition(new Vector2(800, 800));
            Planet.Add(new Planet(PlanetTexture[0]));
            Planet[0].Position = new Vector2(800, 300);

            foreach (EnemyShip list in Enemy)
            {
                EnemyPosition.Add(list.Position);
            }
            foreach (Planet list in Planet)
            {
                PlanetPosition.Add(list.Position);
            }
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
                            Player.Update(gameTime, EnemyPosition, PlanetPosition);
                            if (Player.stage == GameObjects.Ship.Stage.EndTurn)
                            {
                                turn = Turn.Enemy;
                                for (int i = 0; i < Enemy.Count; i++)
                                {
                                    Enemy[i].ResetAction();
                                    if (
                                        ((Player.bullet.X + Singleton.BULLETSIZE >= Enemy[i].Position.X && Player.bullet.Y + Singleton.BULLETSIZE >= Enemy[i].Position.Y) &&
                                        (Player.bullet.X < (Enemy[i] .Position.X + Singleton.SHIPSIZE) && Player.bullet.Y < Enemy[i].Position.Y + Singleton.SHIPSIZE)) ||
                                        ((Player.bullet.X + Singleton.BULLETSIZE >= Enemy[i].Position.X && Player.bullet.Y < (Enemy[i].Position.Y + Singleton.SHIPSIZE)) &&
                                        (Player.bullet.X < (Enemy[i] .Position.X + Singleton.SHIPSIZE) && Player.bullet.Y + Singleton.BULLETSIZE >= Enemy[i].Position.Y))
                                        )
                                    {
                                        Enemy[i].Health -= 50;
                                        if (Enemy[i].Health <= 0)
                                        {
                                            Enemy.RemoveAt(i);
                                        }
                                    }
                                }
                                for (int i = 0; i < Planet.Count; i++)
                                {
                                    if (
                                        ((Player.bullet.X + Singleton.BULLETSIZE >= Planet[i].Position.X && Player.bullet.Y + Singleton.BULLETSIZE >= Planet[i].Position.Y) &&
                                        (Player.bullet.X < (Planet[i].Position.X + Singleton.SHIPSIZE) && Player.bullet.Y < Planet[i].Position.Y + Singleton.SHIPSIZE)) ||
                                        ((Player.bullet.X + Singleton.BULLETSIZE >= Planet[i].Position.X && Player.bullet.Y < (Planet[i].Position.Y + Singleton.SHIPSIZE)) &&
                                        (Player.bullet.X < (Planet[i].Position.X + Singleton.SHIPSIZE) && Player.bullet.Y + Singleton.BULLETSIZE >= Planet[i].Position.Y))
                                        )
                                    {
                                        Planet[i].Health -= 50;
                                         if(Planet[i].Health <= 0)
                                        {
                             
                                            Planet.RemoveAt(i);
                                        }
                                    }
                                }
                                
                                
                                if(Enemy.Count == 0)
                                {
                                    Game = Stage.Pause;
                                }
                            
                            }
                            break;

                        case Turn.Enemy:
                            foreach (EnemyShip list in Enemy)
                            {
                                list.Update(gameTime, Player.Position, PlanetPosition);
                                //list.Position = new Vector2(500, 100);
                                if(list.stage == GameObjects.EnemyShip.Stage.EndTurn)
                                {
                                    if (
                                            ((list.bullet.X + Singleton.BULLETSIZE >= Player.Position.X && list.bullet.Y + Singleton.BULLETSIZE >= Player.Position.Y) &&
                                            (list.bullet.X < (Player.Position.X + Singleton.SHIPSIZE) && list.bullet.Y < Player.Position.Y + Singleton.SHIPSIZE)) ||
                                            ((list.bullet.X + Singleton.BULLETSIZE >= Player.Position.X && list.bullet.Y < (Player.Position.Y + Singleton.SHIPSIZE)) &&
                                            (list.bullet.X < (Player.Position.X + Singleton.SHIPSIZE) && list.bullet.Y + Singleton.BULLETSIZE >= Player.Position.Y))
                                            )
                                    {
                                        Player.Health -= 50;
                                        if(Player.Health <= 0)
                                        {
                                            Player = null;
                                        }
                                    }
                                    for (int i = 0; i < Planet.Count; i++)
                                    {
                                            if (
                                            ((list.bullet.X + Singleton.BULLETSIZE >= Planet[i].Position.X && list.bullet.Y + Singleton.BULLETSIZE >= Planet[i].Position.Y) &&
                                            (list.bullet.X < (Planet[i].Position.X + Singleton.SHIPSIZE) && list.bullet.Y < Planet[i].Position.Y + Singleton.SHIPSIZE)) ||
                                            ((list.bullet.X + Singleton.BULLETSIZE >= Planet[i].Position.X && list.bullet.Y < (Planet[i].Position.Y + Singleton.SHIPSIZE)) &&
                                            (list.bullet.X < (Planet[i].Position.X + Singleton.SHIPSIZE) && list.bullet.Y + Singleton.BULLETSIZE >= Planet[i].Position.Y))
                                            )
                                        {
                                            Planet[i].Health -= 50;
                                            if (Planet[i].Health <= 0)
                                            {

                                                Planet.RemoveAt(i);
                                            }
                                        }
                                    }
                                }
                            }
                            if (Player != null)
                            {
                                turn = Turn.Player;
                                Player.ResetAction();
                            }
                            else
                            {
                                Game = Stage.Pause;
                            }
                            break;
                    }
                    break;

                case Stage.Pause:
                    break;
            }

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
                    _spriteBatch.DrawString(font, Player.Health.ToString(), Player.Position + new Vector2(0, 90), Color.Black);
                    foreach (EnemyShip list in Enemy)
                    {
                        list.Draw(_spriteBatch);
                        _spriteBatch.DrawString(font, list.Health.ToString(), list.Position + new Vector2(0, 90), Color.Black);
                    }
                    foreach (Planet list in Planet)
                    {
                        list.Draw(_spriteBatch);
                        _spriteBatch.DrawString(font, list.Health.ToString(), list.Position + new Vector2(0, 90), Color.Black);
                    }
                    break;
                case Stage.Pause:

                    break;
            }

            _spriteBatch.End();
            _graphics.BeginDraw();
            base.Draw(gameTime);
        }
    }
}
