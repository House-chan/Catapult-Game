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
        public float ShootPower;
        public int Health;
        public float moveRange;

        public enum Stage
        {
            Start, Shooting, Move, EndTurn
        }
        public Stage stage;

        public Gun gun;
        AimGuide guide;

        int speed;

        public Ship(Texture2D texture, Texture2D gunTexture, Texture2D[] bulletTexture, Texture2D GuideLine) : base(texture)
        {
            speed = 5;
            moveRange = 1500;
            Health = 100;
            ShootPower = 5.0f;
            stage = Stage.Start;
            gun = new Gun(gunTexture, bulletTexture)
            {
                Position = new Vector2(this.Position.X + 90, this.Position.Y + 30)
            };
            guide = new AimGuide(GuideLine);

        }

        public void Update(GameTime gameTime, List<EnemyShip> Enemy, List<Planet> Planet)
        {
            switch (stage)
            {
                case Stage.Start:
                    //Moving
                    moving(Enemy, Planet);

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
                    gun.Update(gameTime, Enemy, Planet);
                    if (gun.bullet.end)
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

        public void Draw(SpriteBatch spriteBatch, List<Planet> planet)
        {
            //Draw ship and method ammo
            spriteBatch.Draw(_texture, Position, null, Color.White, 0f, new Vector2(_texture.Width/2, _texture.Height/2), 1, SpriteEffects.None, 0f);
            gun.Draw(spriteBatch);
            guide.Draw(spriteBatch, this, planet);
            //switch (stage)
            //{
            //    case Stage.Start:

            //        break;
            //    case Stage.Shooting:

            //        break;
            //}
            //Hud Power, Fuel, Health
        }

        public override void Reset()
        {
            //Restart ship 
            Position = new Vector2(200, 500);
            gun.Position = new Vector2(290, 530);
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
            if (Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Pressed && Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Released || ShootPower > 15)
            {
                stage = Stage.Move;
                gun.reload();
                gun.shoot(ShootPower);
                ShootPower = 5.0f;
            }
            //Create ball
            //bullet create 
            //bullet.update();
            //Change Stage
        }



        private void moving(List<EnemyShip> Enemy, List<Planet> Planet)
        {
            //Moving
            Singleton.Instance.CurrentKey= Keyboard.GetState();
            if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.W) && moveRange > 0)
            {
                Velocity.Y -= speed;   
            }

            else if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.S) && moveRange > 0)
            {
                Velocity.Y += speed;
            }

            if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.D) && moveRange > 0)
            {
                Velocity.X += speed;
            }

            else if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.A) && moveRange > 0)
            {
                Velocity.X -= speed;
            }

            foreach (var sprite in Enemy)
            {
                if ((this.Velocity.X > 0 && this.IsTouchingLeft(sprite)) ||
                    (this.Velocity.X < 0 && this.IsTouchingRight(sprite)))
                {
                    this.Velocity.X = 0;
                }

                if ((this.Velocity.Y > 0 && this.IsTouchingTop(sprite)) ||
                    (this.Velocity.Y < 0 && this.IsTouchingBottom(sprite)))
                {
                    this.Velocity.Y = 0;
                }
            }

            foreach (var sprite in Planet)
            {
                if ((this.Velocity.X > 0 && this.IsTouchingLeft(sprite)) ||
                    (this.Velocity.X < 0 && this.IsTouchingRight(sprite)))
                {
                    this.Velocity.X = 0;
                }

                if ((this.Velocity.Y > 0 && this.IsTouchingTop(sprite)) ||
                    (this.Velocity.Y < 0 && this.IsTouchingBottom(sprite)))
                {
                    this.Velocity.Y = 0;
                }
            }


            if ((this.Velocity.X > 0 && Position.X > Singleton.SCREENWIDTH - Singleton.SHIPSIZE / 2) ||
                    (this.Velocity.X < 0 && Position.X < Singleton.SHIPSIZE / 2))
            {
                this.Velocity.X = 0;
            }

            if ((this.Velocity.Y > 0 && Position.Y > Singleton.SCREENHEIGHT - Singleton.SHIPSIZE / 2) ||
                (this.Velocity.Y < 0 && Position.Y < Singleton.SHIPSIZE / 2))
            {
                this.Velocity.Y = 0;
            }

            //if (Position.X < Singleton.SHIPSIZE / 2)
            //{
            //    Position.X = Singleton.SHIPSIZE / 2;
                
            //}
            //if (Position.X > Singleton.SCREENWIDTH - Singleton.SHIPSIZE / 2) Position.X = Singleton.SCREENWIDTH - Singleton.SHIPSIZE / 2;
            //if (Position.Y < Singleton.SHIPSIZE / 2) Position.Y = Singleton.SHIPSIZE / 2;
            //if (Position.Y > Singleton.SCREENHEIGHT - Singleton.SHIPSIZE / 2) Position.Y = Singleton.SCREENHEIGHT - Singleton.SHIPSIZE / 2;
            

            Position += Velocity;
            gun.Position += Velocity;
            moveRange -= Math.Abs(Velocity.X) + Math.Abs(Velocity.Y);
            Velocity = Vector2.Zero;
        }

        public void ResetAction()
        {
            moveRange = 500;
            stage = Stage.Start;
        }

        public void SetPosition(Vector2 pos)
        {
            Position = pos;
            gun.Position = new Vector2(pos.X + 90, pos.Y + 30);
        }

    }
}
