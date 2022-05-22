using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Catapult.GameObjects
{
    class EnemyShip : GameObject
    {
        float ShootPower;
        public int Health;
        float moveRange;

        Vector2 Distance;
        float angle;

        public enum Stage
        {
            Start, Shooting, Move, EndTurn
        }

        public Stage stage;
        Gun gun;
        Random rand = new Random();
        SoundEffectInstance shoot, bulletMove, explosion;
        int speed;

        public EnemyShip(Texture2D texture, Texture2D gunTexture, Texture2D[] bulletTexture, SoundEffectInstance shoot, SoundEffectInstance bulletMove, SoundEffectInstance explosion) : base(texture)
        {
            speed = 5;
            Health = 100;
            moveRange = 100;
            ShootPower = Math.Min(10, rand.Next(20));
            this.shoot = shoot;
            this.bulletMove = bulletMove;
            this.explosion = explosion;
            stage = Stage.Start;
            gun = new Gun(gunTexture, bulletTexture)
            {
                Position = new Vector2(Position.X + 10, Position.Y + 30)
            };
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, null, Color.White, Rotation, new Vector2(width / 2, height / 2), 1, SpriteEffects.None, 0f);
            gun.DrawEnemy(spriteBatch);
        }

        public override void Reset()
        {

        }

        public void Update(GameTime gameTime, Ship Player, List<Planet> Planet, List<EnemyShip> Enemy)
        {
            switch (stage)
            {
                case Stage.Start:
                    //Moving

                    if (moveRange > 0)
                    {
                        moving(Player, Planet, Enemy);
                    }
                    if(moveRange <= 0)
                    {
                        gun.aiming(Player.Position);
                        stage = Stage.Shooting;
                    }

                    break;
                case Stage.Shooting:
                    gun.reload();
                    gun.shoot(ShootPower);
                    shoot.Play();
                    stage = Stage.Move;
                    break;

                case Stage.Move:
                    //bullet.shooting(Rotation, power);
                    bulletMove.Play();
                    gun.Update(gameTime, Player, Planet);
                    if (gun.bullet.end)
                    {
                        bulletMove.Stop();
                        explosion.Play();
                        gun.clearBullet();
                        stage = Stage.EndTurn;
                    }
                    break;
                case Stage.EndTurn:
                    break;
            }
            
        }

        private void moving(Ship Player, List<Planet> Planet, List<EnemyShip> Enemy)
        {
            //Moving
            if(Health > 50)
            {
                Velocity.X -= speed;
                Velocity.Y += speed * (Player.Position.Y > Position.Y ? 1 : -1);
            }
            else
            {
                Velocity.X -= speed * (Player.Position.X > Position.X ? 1 : -1);
                Velocity.Y -= speed * (Player.Position.Y > Position.Y ? 1 : -1);
            }
            
            
            if ((this.Velocity.X > 0 && this.IsTouchingLeft(Player)) ||
                (this.Velocity.X < 0 && this.IsTouchingRight(Player)))
            {
                this.Velocity.X = 0;
            }

            if ((this.Velocity.Y > 0 && this.IsTouchingTop(Player)) ||
                (this.Velocity.Y < 0 && this.IsTouchingBottom(Player)))
            {
                this.Velocity.Y = 0;
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

            foreach (var sprite in Enemy)
            {
                if(sprite == this)
                {
                    continue;
                }
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
            if((this.Velocity.Y > 0 && Position.Y + height > Singleton.SCREENHEIGHT) || (this.Velocity.Y < 0 && Position.Y < 0))
            {
                this.Velocity.Y = 0;
            }
            else if((this.Velocity.X > 0 && Position.X + width > Singleton.SCREENWIDTH) || (this.Velocity.X < 0 && Position.X < 0))
            {
                this.Velocity.X = 0;
            }

            Position += Velocity;
            gun.Position += Velocity;
            moveRange -= Math.Abs(Velocity.X) + Math.Abs(Velocity.Y);
            if (Velocity == new Vector2(0, 0)) moveRange = 0;
            Velocity = Vector2.Zero;
        }

        public void ResetAction()
        {
            //Action
            stage = Stage.Start;
            //Stat Moving
            moveRange = 100;
        }

        public void SetPosition(Vector2 pos)
        {
            Position = pos;
            gun.Position = new Vector2(pos.X + 10, pos.Y + 30);
        }
    }
}
