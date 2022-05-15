using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catapult.GameObjects
{
    class Ship : GameObject
    {
        float ShootPower;
        float Health;
        float moveRange;

        public enum Stage
        {
            Start, Shooting, Move, EndTurn
        }
        public Stage stage;


        int speed;
        public Gun gun;

        public Ship(Texture2D texture, Texture2D gunTexture, Texture2D bulletTexture) : base(texture)
        {
            speed = 5;
            moveRange = 10000;
            Health = 100;
            ShootPower = 5.0f;
            stage = Stage.Start;
            gun = new Gun(gunTexture, bulletTexture)
            {
                Position = new Vector2(this.Position.X + 90, this.Position.Y + 30)
            };
        }
        public override void Update(GameTime gameTime)
        {
            switch (stage)
            {
                case Stage.Start:
                    moving();
                    Singleton.Instance.CurrentMouse = Mouse.GetState();
                    //Aiming Gun
                    gun.aiming();

                    //Select Ammo
                    gun.changeAmmo();

                    //when click change stage save angle and Position
                    if (Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Pressed)
                    {   
                        //Start Charge Gun
                        stage = Stage.Shooting;
                        //Reload Ammo (Create Ammo)
                    }
                
                    break;

                case Stage.Shooting:
                    Singleton.Instance.PreviousMouse = Singleton.Instance.CurrentMouse;
                    Singleton.Instance.CurrentMouse = Mouse.GetState();
                    //Aim Until shoot
                    gun.aiming();

                    //Charge Power
                    shoot();
                    
                    break;

                case Stage.Move:
                    gun.Update(gameTime);
                    if (gun.bullet.hit())
                    {
                        gun.clearBullet();
                        stage = Stage.EndTurn;
                    }
                        break;

                case Stage.EndTurn:
                    //Swap Turn
                    break;

            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Draw ship and method ammo
            spriteBatch.Draw(_texture, Position, null, Color.White, 0f, new Vector2(_texture.Width/2, _texture.Height/2), 1, SpriteEffects.None, 0f);
            gun.Draw(spriteBatch);
            //Hud Power, Fuel, Health
        }

        public override void Reset()
        {
            //Restart ship 

            //Position

            //Ammo
        }

        private void shoot()
        {
            //Click Mouse
            if (Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Pressed)
            {
                //ShootPower++
                ShootPower += 0.1f;
            }
            if (Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Pressed && Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Released || ShootPower > 10)
            {
                stage = Stage.Move;
                gun.reload();
                gun.shoot(ShootPower);
                ShootPower = 0.0f;
            }
            //Create ball
            //bullet create 
            //bullet.update();
            //Change Stage
        }



        private void moving()
        {
            //Moving
            Singleton.Instance.CurrentKey= Keyboard.GetState();
            if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.Up) && moveRange > 0)
            {
                Position = new Vector2(Position.X, Position.Y - speed);
                gun.Position = new Vector2(gun.Position.X, gun.Position.Y - speed);
                moveRange -= 1;
            }

            else if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.Down) && moveRange > 0)
            {
                Position = new Vector2(Position.X, Position.Y + speed);
                gun.Position = new Vector2(gun.Position.X, gun.Position.Y + speed);
                moveRange -= 1;
            }

            if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.Right) && moveRange > 0)
            {
                Position = new Vector2(Position.X + speed, Position.Y);
                gun.Position = new Vector2(gun.Position.X + speed, gun.Position.Y);
                moveRange -= 1;
            }

            else if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.Left) && moveRange > 0)
            {
                Position = new Vector2(Position.X - speed, Position.Y);
                gun.Position = new Vector2(gun.Position.X - speed, gun.Position.Y);
                moveRange -= 1;
            }
        }

        public Stage getStage()
        {
            return stage;
        }
    }
}
