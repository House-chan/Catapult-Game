using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Catapult.GameObjects;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace Catapult
{
    public class GameScene : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        enum Stage
        {
            MainMenu, Stage, Pause, End
        }

        Stage Game;

        enum Turn
        {
            Player, Enemy, ChangeTurn, Dead
        }

        Turn turn;
        
        Texture2D PlayerShip, EnemyShip, guideline, meteorite, gun, EnemyGun, box;
        Texture2D[] PlanetTexture = new Texture2D[11], Bullet = new Texture2D[7];

        Ship Player;
        //EnemyShip Enemy;
        List<EnemyShip> Enemy = new List<EnemyShip>();
        List<Planet> Planet = new List<Planet>();

        SpriteFont font, pauseFont, uifont;
        int countTurn;

        MainMenu mainmenu;
        Texture2D ship, soup, menuBackground, stageSelect, prop, startButton, exitButton, backButton, title, settingButton;
        Texture2D gameBackground;
        Texture2D[] stageButton = new Texture2D[5];

        Vector2 Board;
        String WhoseTurn;
        Turn lastTurn;
        float transparent;
        MouseState mouse;
        MouseState premouse;

        SoundEffectInstance bulletMove, shoot, explosion, menuMusicInstance, stageMusicInstance, stageComplete;
        SoundEffect menuMusic, stageMusic;
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

            Game = Stage.MainMenu;
            GameReset();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            gameBackground = Content.Load<Texture2D>("starfield_alpha");
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
            guideline = Content.Load<Texture2D>("Bullet/bullet4");
            //meteorite = Content.Load<Texture2D>("");
            gun = Content.Load<Texture2D>("Ship/PlayerCanon");
            EnemyGun = Content.Load<Texture2D>("Ship/EnemyCanon");
            Bullet[0] = Content.Load<Texture2D>("Bullet/bullet4");
            Bullet[1] = Content.Load<Texture2D>("Bullet/bullet2");
            Bullet[2] = Content.Load<Texture2D>("Bullet/bullet3");
            
            box = new Texture2D(_graphics.GraphicsDevice, 100, 5);
            Color[] color = new Color[100 * 5];
            for (int i = 0; i < color.Length; i++)
            {
                color[i] = Color.White;
            }
            box.SetData(color);

            Bullet[3] = Content.Load<Texture2D>("Bullet/bullet1");
            Bullet[4] = Content.Load<Texture2D>("Bullet/Nyan-Cat-PNG");
            Bullet[5] = box;

            font = Content.Load<SpriteFont>("Font");
            pauseFont = Content.Load<SpriteFont>("pause");
            uifont = Content.Load<SpriteFont>("uifont");

            //MAINMENU
            ship = Content.Load<Texture2D>("MainMenu/pirate");
            soup = Content.Load<Texture2D>("MainMenu/soup");
            menuBackground = Content.Load<Texture2D>("MainMenu/galaxy");
            title = Content.Load<Texture2D>("MainMenu/Title");
            prop = Content.Load<Texture2D>("MainMenu/nyan_cat_rotate");
            startButton = Content.Load<Texture2D>("MainMenu/Button/start_idle");
            exitButton = Content.Load<Texture2D>("MainMenu/Button/exit_idle");
            settingButton = Content.Load<Texture2D>("MainMenu/Button/Setting_idle");
            stageSelect = Content.Load<Texture2D>("MainMenu/starry-doge");
            backButton = Content.Load<Texture2D>("MainMenu/backbutton");
            menuBackground = Content.Load<Texture2D>("MainMenu/galaxy");
            stageButton[0] = Content.Load<Texture2D>("MainMenu/Button/1_idle");
            stageButton[1] = Content.Load<Texture2D>("MainMenu/Button/2_idle");
            stageButton[2] = Content.Load<Texture2D>("MainMenu/Button/3_idle");
            stageButton[3] = Content.Load<Texture2D>("MainMenu/Button/4_idle");
            stageButton[4] = Content.Load<Texture2D>("MainMenu/Button/5_idle");

            mainmenu = new MainMenu(menuBackground, startButton, settingButton, exitButton, ship, soup, title, prop, backButton, stageSelect, stageButton, box);

            //SoundEffect
            menuMusic = Content.Load<SoundEffect>("Sound/BG/Mainmenu");
            menuMusicInstance = menuMusic.CreateInstance();
            stageMusic = Content.Load<SoundEffect>("Sound/BG/Stage");
            stageMusicInstance = stageMusic.CreateInstance();
            stageComplete = Content.Load<SoundEffect>("Sound/BG/stageComplete").CreateInstance();
            bulletMove = Content.Load<SoundEffect>("Sound/Effect/BulletMove").CreateInstance();
            shoot = Content.Load<SoundEffect>("Sound/Effect/shoot").CreateInstance();
            explosion = Content.Load<SoundEffect>("Sound/Effect/Explosion").CreateInstance();

            menuMusicInstance.Volume = 0.4f * Singleton.Instance.music;
            stageMusicInstance.Volume = 0.4f * Singleton.Instance.music;
            stageComplete.Volume = Singleton.Instance.music;
            
            bulletMove.Volume = Singleton.Instance.sound;
            shoot.Volume = Singleton.Instance.sound;
            explosion.Volume = Singleton.Instance.sound;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape) || mainmenu.exit)
                Exit();

            // TODO: Add your update logic here
            switch (Game)
            {
                case Stage.MainMenu:
                    mainmenu.Update(gameTime);
                    menuMusicInstance.Volume = 0.4f * Singleton.Instance.music;
                    stageMusicInstance.Volume = 0.4f * Singleton.Instance.music;

                    bulletMove.Volume = Singleton.Instance.sound;
                    shoot.Volume = Singleton.Instance.sound;
                    explosion.Volume = Singleton.Instance.sound;
                    menuMusicInstance.Play();
                    if (mainmenu.stage != 0)
                    {
                        changeStage(mainmenu.stage);
                        menuMusicInstance.Stop();
                        //mainmenu.stage = 0;
                        Game = Stage.Stage;
                    }
                    break;

                case Stage.Stage:
                    stageMusicInstance.Play();
                    mouse = Mouse.GetState();
                    if ((mouse.Position.Y >= 30 && mouse.Position.Y < 92.5) &&
                        (mouse.Position.X >= 45 && mouse.Position.X < 95))
                    {
                        //Pressed
                        if (premouse.LeftButton == ButtonState.Pressed && mouse.LeftButton == ButtonState.Released)
                        {
                            stageMusicInstance.Stop();
                            Game = Stage.Pause;
                        }
                    }
                    switch (turn)
                    {
                        case Turn.Player:
                            Player.Update(gameTime, Enemy, Planet);
                            if (Player.stage == GameObjects.Ship.Stage.EndTurn)
                            {
                                lastTurn = Turn.Player;
                                turn = Turn.ChangeTurn;
                                for (int i = 0; i < Enemy.Count; i++)
                                {
                                    Enemy[i].ResetAction();
                                    if (Enemy[i].Health <= 0)
                                    {
                                        turn = Turn.Dead;
                                    }
                                }
                                for (int i = 0; i < Planet.Count; i++)
                                {
                                    if(Planet[i].Health <= 0)
                                    {
                                        turn = Turn.Dead;
                                    }
                                }
                                if (Player.Health <= 0)
                                {
                                    turn = Turn.Dead;
                                }
                            }
                            break;

                        case Turn.Enemy:
                            if(countTurn < Enemy.Count)
                            {
                                Enemy[countTurn].Update(gameTime, Player, Planet, Enemy);
                                if(Enemy[countTurn].stage == GameObjects.EnemyShip.Stage.EndTurn)
                                {
                                    if (Player.Health <= 0)
                                    {
                                        turn = Turn.Dead;
                                        //Player = null;
                                    }
                                    for (int i = 0; i < Planet.Count; i++)
                                    {
                                        
                                        if (Planet[i].Health <= 0)
                                        {
                                            turn = Turn.Dead;
                                            //Planet.RemoveAt(i);
                                        }
                                    }
                                    countTurn += 1;
                                }
                            }
                            else
                            {
                                countTurn = 0;
                                lastTurn = Turn.Enemy;
                                turn = Turn.ChangeTurn;
                                Player.ResetAction();
                            }
                            break;

                        case Turn.ChangeTurn:
                            mouse = Mouse.GetState();
                            if (lastTurn == Turn.Enemy)
                            {
                                if (premouse.LeftButton == ButtonState.Pressed && mouse.LeftButton == ButtonState.Released)
                                {
                                    Board.X = 350;
                                    transparent = 1;
                                }
                                if (Board.X < 350)
                                {
                                    Board.X += 20;
                                }
                                else if(transparent > 0)
                                {
                                    transparent -= 0.015f;
                                }
                                else
                                {
                                    Board.X = 1600;
                                    transparent = 1f;
                                    turn = Turn.Player;
                                    WhoseTurn = "ENEMY TURN";
                                }
                            }
                            else if(lastTurn == Turn.Player)
                            {
                                if (premouse.LeftButton == ButtonState.Pressed && mouse.LeftButton == ButtonState.Released)
                                {
                                    Board.X = 350;
                                    transparent = 1;
                                }
                                if (Board.X > 350)
                                {
                                    Board.X -= 20;
                                }
                                else if (transparent > 0)
                                {
                                    transparent -= 0.015f;
                                }
                                else
                                {
                                    turn = Turn.Enemy;
                                    transparent = 1f;
                                    Board.X = -1400;
                                    WhoseTurn = "PLAYER TURN";
                                }
                            }
                            premouse = mouse;
                            break;

                        case Turn.Dead:
                            //Player Dead
                            if (Player.Health <= 0)
                            {
                                Player = null;
                            }
                            //Enemy Dead
                            for (int i = 0; i < Enemy.Count; i++)
                            {
                                Enemy[i].ResetAction();
                                if (Enemy[i].Health <= 0)
                                {
                                    Enemy.RemoveAt(i);
                                }
                            }
                            //Planet Ded
                            for (int i = 0; i < Planet.Count; i++)
                            {
                                if (Planet[i].Health <= 0)
                                {
                                    Planet.RemoveAt(i);
                                }
                            }
                            if (Player == null)
                            {
                                stageMusicInstance.Stop();
                                Game = Stage.End;
                            }
                            else if (Enemy.Count == 0)
                            {
                                Game = Stage.End;
                                stageMusicInstance.Stop();
                            }
                            else
                            {
                                turn = Turn.ChangeTurn;
                            }
                            break;
                    }
                    premouse = mouse;
                    break;
                    
                case Stage.Pause:
                    mouse = Mouse.GetState();
                    //Resume
                    if ((mouse.Position.Y >= 290 && mouse.Position.Y < 365) &&
                        (mouse.Position.X >= 670 && mouse.Position.X < 950))
                    {
                        //Pressed
                        if (premouse.LeftButton == ButtonState.Pressed && mouse.LeftButton == ButtonState.Released)
                        {
                            Game = Stage.Stage;
                        }
                    }
                    //Restart
                    if ((mouse.Position.Y >= 400 && mouse.Position.Y < 475) &&
                        (mouse.Position.X >= 670 && mouse.Position.X < 950))
                    {
                        //Pressed
                        if (premouse.LeftButton == ButtonState.Pressed && mouse.LeftButton == ButtonState.Released)
                        {
                            GameReset();
                            changeStage(mainmenu.stage);
                            Game = Stage.Stage;
                        }
                    }
                    //Mainmenu
                    if ((mouse.Position.Y >= 510 && mouse.Position.Y < 585) &&
                        (mouse.Position.X >= 670 && mouse.Position.X < 950))
                    {
                        //Pressed
                        if (premouse.LeftButton == ButtonState.Pressed && mouse.LeftButton == ButtonState.Released)
                        {
                            GameReset();
                            mainmenu.stage = 0;
                            mainmenu.page = MainMenu.menu.MainMenu;
                            mainmenu.resetMenu();
                            Game = Stage.MainMenu;
                        }
                    }
                    //Exit
                    if ((mouse.Position.Y >= 620 && mouse.Position.Y < 695) &&
                        (mouse.Position.X >= 695 && mouse.Position.X < 925))
                    {
                        //Pressed
                        if (premouse.LeftButton == ButtonState.Pressed && mouse.LeftButton == ButtonState.Released)
                        {
                            Exit();
                        }
                    }
                    premouse = mouse;
                    break;

                case Stage.End:
                    stageComplete.Play();
                    mouse = Mouse.GetState();
                    if(Enemy.Count == 0 && mainmenu.stage < 5)
                    {
                        //Next
                        if ((mouse.Position.Y >= 500 && mouse.Position.Y < 575) &&
                            (mouse.Position.X >= 425 && mouse.Position.X < 705))
                        {
                            //Pressed
                            if (premouse.LeftButton == ButtonState.Pressed && mouse.LeftButton == ButtonState.Released)
                            {
                                GameReset();
                                changeStage(mainmenu.stage + 1);
                                stageComplete.Stop();
                                Game = Stage.Stage;
                            }
                        }
                    }
                    else
                    {
                        //Restart
                        if ((mouse.Position.Y >= 500 && mouse.Position.Y < 575) &&
                            (mouse.Position.X >= 425 && mouse.Position.X < 705))
                        {
                            //Pressed
                            if (premouse.LeftButton == ButtonState.Pressed && mouse.LeftButton == ButtonState.Released)
                            {
                                GameReset();
                                changeStage(mainmenu.stage);
                                stageComplete.Stop();
                                Game = Stage.Stage;
                            }
                        }
                    }
                    //Mainmenu
                    if ((mouse.Position.Y >= 500 && mouse.Position.Y < 575) &&
                        (mouse.Position.X >= 850 && mouse.Position.X < 1130))
                    {
                        //Pressed
                        if (premouse.LeftButton == ButtonState.Pressed && mouse.LeftButton == ButtonState.Released)
                        {
                            
                            stageComplete.Stop();
                            GameReset();
                            mainmenu.stage = 0;
                            mainmenu.page = MainMenu.menu.MainMenu;
                            mainmenu.resetMenu();
                            Game = Stage.MainMenu;
                            
                        }
                    }
                    premouse = mouse;
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
                    mainmenu.Draw(_spriteBatch);
                    break;
                case Stage.Stage:
                    //GameBackground
                    _spriteBatch.Draw(gameBackground, Vector2.Zero, Color.White);

                    Player.Draw(_spriteBatch, Planet);
                    //Stamina
                    if (turn == Turn.Player)
                    {
                        _spriteBatch.Draw(box, new Rectangle((int)Player.Position.X - PlayerShip.Width / 2 - 25, (int)Player.Position.Y + 100, 100, 25), null, Color.DarkGray, (float)Math.PI / -2.0f, new Vector2(1, 1), SpriteEffects.None, 0f);
                        _spriteBatch.Draw(box, new Rectangle((int)Player.Position.X - PlayerShip.Width / 2 - 25, (int)Player.Position.Y + 100, (int)Player.moveRange/5, 20), null, Color.Blue, (float)Math.PI / -2.0f, new Vector2(1, 1), SpriteEffects.None, 0f);
                    }
                    //Power
                    if (Player.stage == GameObjects.Ship.Stage.Shooting)
                    {
                        _spriteBatch.Draw(box, new Rectangle((int)Player.Position.X - PlayerShip.Width / 2, (int)Player.Position.Y + 115, 200, 10), Color.DarkGray * 0.8f);
                        _spriteBatch.Draw(box, new Rectangle((int)Player.Position.X - PlayerShip.Width / 2, (int)Player.Position.Y + 115, (int)Player.ShootPower * 14, 8), Color.Orange);
                    }
                    if(Player.Health > 70)
                    {
                        _spriteBatch.Draw(box, new Rectangle((int)Player.Position.X - PlayerShip.Width/2, (int)Player.Position.Y + 100, Player.Health * 2, 15), Color.Green);
                    }
                    else if(Player.Health > 30)
                    {
                        _spriteBatch.Draw(box, new Rectangle((int)Player.Position.X - PlayerShip.Width / 2, (int)Player.Position.Y + 100, Player.Health * 2, 15), Color.YellowGreen);
                    }
                    else
                    {
                        _spriteBatch.Draw(box, new Rectangle((int)Player.Position.X - PlayerShip.Width / 2, (int)Player.Position.Y + 100, Player.Health * 2, 15), Color.Red);
                    }
                    //_spriteBatch.DrawString(font, Player.Health.ToString(), Player.Position + new Vector2(0, 90), Color.Black);
                    foreach (EnemyShip list in Enemy)
                    {
                        list.Draw(_spriteBatch);
                        //_spriteBatch.DrawString(font, list.Health.ToString(), list.Position + new Vector2(0, 90), Color.Black);
                        if (list.Health > 70)
                        {
                            _spriteBatch.Draw(box, new Rectangle((int)list.Position.X - list.width / 2, (int)list.Position.Y + 80, list.Health * 2, 15), Color.Green);
                        }
                        else if (list.Health > 30)
                        {
                            _spriteBatch.Draw(box, new Rectangle((int)list.Position.X - list.width / 2, (int)list.Position.Y + 80, list.Health * 2, 15), Color.YellowGreen);
                        }
                        else
                        {
                            _spriteBatch.Draw(box, new Rectangle((int)list.Position.X - list.width / 2, (int)list.Position.Y + 80, list.Health * 2, 15), Color.Red);
                        }
                    }
                    foreach (Planet list in Planet)
                    {
                        list.Draw(_spriteBatch);
                        if (list.Health > 70)
                        {
                            _spriteBatch.Draw(box, new Rectangle((int)list.Position.X - list.width / 2, (int)list.Position.Y + 80, list.Health, 10), Color.Green);
                        }
                        else if (list.Health > 30)
                        {
                            _spriteBatch.Draw(box, new Rectangle((int)list.Position.X - list.width / 2, (int)list.Position.Y + 80, list.Health, 10), Color.YellowGreen);
                        }
                        else
                        {
                            _spriteBatch.Draw(box, new Rectangle((int)list.Position.X - list.width / 2, (int)list.Position.Y + 80, list.Health, 10), Color.Red);
                        }
                        //_spriteBatch.DrawString(font, list.Health.ToString(), list.Position + new Vector2(0, 90), Color.Black);
                    }

                    //Turn
                    _spriteBatch.Draw(box, new Rectangle(45, 30, 19, 63), Color.Yellow);
                    _spriteBatch.Draw(box, new Rectangle(76, 30, 19, 63), Color.Yellow);
                    _spriteBatch.DrawString(font, WhoseTurn, Board, Color.Red * transparent);

                    break;
                case Stage.Pause:
                    //GameBackground
                    _spriteBatch.Draw(gameBackground, Vector2.Zero, Color.White);
                    if (Player != null) Player.Draw(_spriteBatch, Planet);
                    if (EnemyShip != null)
                        foreach (EnemyShip list in Enemy)
                        {
                            list.Draw(_spriteBatch);
                        }
                    if (Planet != null)
                        foreach (Planet list in Planet)
                        {
                            list.Draw(_spriteBatch);
                        }
                    //Black Background
                    _spriteBatch.Draw(box, new Rectangle(0, 0, Singleton.SCREENWIDTH, Singleton.SCREENHEIGHT), Color.Black * 0.3f);
                    
                    //PauseBoard
                    _spriteBatch.Draw(box, new Rectangle(568, 147, 465, 605), Color.White* 0.8f);
                    _spriteBatch.DrawString(font, "Paused", new Vector2(590, 145), Color.Orange);
                    _spriteBatch.DrawString(font, "Paused", new Vector2(590, 145), Color.Yellow * 0.6f);

                    //Resume
                    _spriteBatch.Draw(box, new Rectangle(670, 290, 280, 75), Color.Green);
                    _spriteBatch.DrawString(pauseFont, "Resume", new Vector2(684, 290), Color.White);

                    //Restart
                    _spriteBatch.Draw(box, new Rectangle(670, 400, 280, 75), Color.Green);
                    _spriteBatch.DrawString(pauseFont, "Restart", new Vector2(700, 400), Color.White);

                    //Mainmenu
                    _spriteBatch.Draw(box, new Rectangle(670, 510, 280, 75), Color.Green);
                    _spriteBatch.DrawString(pauseFont, "Menu", new Vector2(725, 510), Color.White);

                    //Exit
                    _spriteBatch.Draw(box, new Rectangle(695, 620, 230, 75), Color.Red);
                    _spriteBatch.DrawString(pauseFont, "Exit", new Vector2(750, 620), Color.White);
                    break;

                case Stage.End:
                    //GameBackground
                    _spriteBatch.Draw(gameBackground, Vector2.Zero, Color.White);
                    if (Player != null) Player.Draw(_spriteBatch, Planet);
                    if (Enemy.Count > 0)
                    foreach (EnemyShip list in Enemy)
                    {
                        list.Draw(_spriteBatch);
                    }
                    if (Planet.Count > 0)
                    foreach (Planet list in Planet)
                    {
                        list.Draw(_spriteBatch);
                        //_spriteBatch.DrawString(font, list.Health.ToString(), list.Position + new Vector2(0, 90), Color.Black);
                    }
                    _spriteBatch.Draw(box, new Rectangle(0, 0, Singleton.SCREENWIDTH, Singleton.SCREENHEIGHT), Color.Black * 0.3f);
                    if(Player == null)
                    {
                        _spriteBatch.DrawString(font, "You Are Dead", new Vector2(425, 150), Color.White);
                        _spriteBatch.DrawString(font, "GGEZ", new Vector2(625, 300), Color.Red);
                        //Restart                      
                        _spriteBatch.Draw(box, new Rectangle(425, 500, 280, 75), Color.Green);
                        _spriteBatch.DrawString(pauseFont, "Restart", new Vector2(460, 500), Color.White);
                    }
                    else if(Enemy.Count == 0)
                    {
                        _spriteBatch.DrawString(font, "You Win", new Vector2(550, 220), Color.White);
                        _spriteBatch.DrawString(font, "You Win", new Vector2(550, 220), Color.Green * 0.8f);
                        //Continue
                        _spriteBatch.Draw(box, new Rectangle(425, 500, 280, 75), Color.Green);
                        if(mainmenu.stage == 5)
                        {
                            _spriteBatch.DrawString(pauseFont, "Restart", new Vector2(460, 500), Color.White);
                        }
                        else
                        {
                            _spriteBatch.DrawString(pauseFont, "Continue", new Vector2(425, 500), Color.White);
                        }
                    }
                    //Mainmenu
                    _spriteBatch.Draw(box, new Rectangle(850, 500, 280, 75), Color.DarkGray);
                    _spriteBatch.DrawString(pauseFont, "Menu", new Vector2(900, 500), Color.White);

                    break;
            }

            _spriteBatch.End();
            _graphics.BeginDraw();
            base.Draw(gameTime);
        }

        void GameReset()
        {
            if(Player != null) Player = null;
            for(int i = Enemy.Count-1; i >= 0; i--)
            {
                Enemy.RemoveAt(i);
            }
            for (int i = Planet.Count-1; i >= 0; i--)
            {
                Planet.RemoveAt(i);
            }
            countTurn = 0;
            turn = Turn.ChangeTurn;
            lastTurn = Turn.Enemy;
            WhoseTurn = "PLAYER TURN";
            Board = new Vector2(-1400, 400);
            transparent = 1f;
        }
        void changeStage(int stage)
        {
            if(stage == 1)
            {
                Player = new Ship(PlayerShip, gun, Bullet, guideline, uifont, shoot, bulletMove, explosion);
                Player.SetPosition(new Vector2(200, 600));
                Enemy.Add(new EnemyShip(EnemyShip, EnemyGun, Bullet, shoot, bulletMove, explosion));
                Enemy[0].SetPosition(new Vector2(1400, 200));
                Planet.Add(new Planet(PlanetTexture[0]));
                Planet[0].Position = new Vector2(800, 450);
            }
            else if(stage == 2)
            {
                Player = new Ship(PlayerShip, gun, Bullet, guideline, uifont, shoot, bulletMove, explosion);
                Player.SetPosition(new Vector2(200, 500));
                Enemy.Add(new EnemyShip(EnemyShip, EnemyGun, Bullet, shoot, bulletMove, explosion));
                Enemy.Add(new EnemyShip(EnemyShip, EnemyGun, Bullet, shoot, bulletMove, explosion));
                Enemy[0].SetPosition(new Vector2(1400, 200));
                Enemy[1].SetPosition(new Vector2(1400, 600));
                Planet.Add(new Planet(PlanetTexture[2]));
                Planet.Add(new Planet(PlanetTexture[3]));
                Planet[0].Position = new Vector2(900, 500);
                Planet[1].Position = new Vector2(500, 300);
            }
            else if(stage == 3)
            {
                Player = new Ship(PlayerShip, gun, Bullet, guideline, uifont, shoot, bulletMove, explosion);
                Player.SetPosition(new Vector2(200, 200));
                Enemy.Add(new EnemyShip(EnemyShip, EnemyGun, Bullet, shoot, bulletMove, explosion));
                Enemy.Add(new EnemyShip(EnemyShip, EnemyGun, Bullet, shoot, bulletMove, explosion));
                Enemy[0].SetPosition(new Vector2(1300, 600));
                Enemy[1].SetPosition(new Vector2(1400, 750));
                Planet.Add(new Planet(PlanetTexture[4]));
                Planet.Add(new Planet(PlanetTexture[5]));
                Planet.Add(new Planet(PlanetTexture[6]));
                Planet[0].Position = new Vector2(800, 200);
                Planet[1].Position = new Vector2(350, 600);
                Planet[2].Position = new Vector2(1000, 700);
            }
            else if(stage == 4)
            {
                Player = new Ship(PlayerShip, gun, Bullet, guideline, uifont, shoot, bulletMove, explosion);
                Player.SetPosition(new Vector2(200, 200));
                Enemy.Add(new EnemyShip(EnemyShip, EnemyGun, Bullet, shoot, bulletMove, explosion));
                Enemy.Add(new EnemyShip(EnemyShip, EnemyGun, Bullet, shoot, bulletMove, explosion));
                Enemy.Add(new EnemyShip(EnemyShip, EnemyGun, Bullet, shoot, bulletMove, explosion));
                Enemy[0].SetPosition(new Vector2(1400, 200));
                Enemy[1].SetPosition(new Vector2(1100, 400));
                Enemy[2].SetPosition(new Vector2(1400, 600));
                Planet.Add(new Planet(PlanetTexture[0]));
                Planet.Add(new Planet(PlanetTexture[2]));
                Planet.Add(new Planet(PlanetTexture[9]));
                Planet[0].Position = new Vector2(800, 300);
                Planet[1].Position = new Vector2(300, 700);
                Planet[2].Position = new Vector2(850, 800);
            }
            else if (stage == 5)
            {
                Player = new Ship(PlayerShip, gun, Bullet, guideline, uifont, shoot, bulletMove, explosion);
                Player.SetPosition(new Vector2(200, 200));
                Enemy.Add(new EnemyShip(EnemyShip, EnemyGun, Bullet, shoot, bulletMove, explosion));
                Enemy.Add(new EnemyShip(EnemyShip, EnemyGun, Bullet, shoot, bulletMove, explosion));
                Enemy.Add(new EnemyShip(EnemyShip, EnemyGun, Bullet, shoot, bulletMove, explosion));
                Enemy.Add(new EnemyShip(EnemyShip, EnemyGun, Bullet, shoot, bulletMove, explosion));
                Enemy.Add(new EnemyShip(EnemyShip, EnemyGun, Bullet, shoot, bulletMove, explosion));
                Enemy[0].SetPosition(new Vector2(1400, 200));
                Enemy[1].SetPosition(new Vector2(1400, 500));
                Enemy[2].SetPosition(new Vector2(1100, 400));
                Enemy[3].SetPosition(new Vector2(1100, 600));
                Enemy[4].SetPosition(new Vector2(1400, 800));
                Planet.Add(new Planet(PlanetTexture[0]));
                Planet.Add(new Planet(PlanetTexture[2]));
                Planet[0].Position = new Vector2(800, 300);
                Planet[1].Position = new Vector2(800, 700);
            }
        }

       
    }
}
