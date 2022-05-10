using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Catapult.GameObjects
{
    class Bullet : GameObject
    { 
        Texture2D satellite;
        float speed;
        Boolean isActive;
        Boolean isPlayerBullet;


        enum BulletType
        {
            Normal,
            Heavy,
            Missile,
            Cluster,
            Laser,
            NyanCat,
            Nuclear,
            Satellite
        };

        BulletType bulletType;

        public Bullet(Texture2D texture, int bullet, Vector2 Position) : base(texture)
        {
            if(bullet == 0)
            {
                bulletType = BulletType.Normal;

            }
            else if(bullet == 1)
            {
                bulletType = BulletType.Heavy;

            }
            else if (bullet == 2)
            {
                bulletType = BulletType.Missile;

            }
            else if(bullet == 3)
            {
                bulletType = BulletType.Cluster;

            }
            else if(bullet == 4)
            {
                bulletType = BulletType.Laser;

            }
            else if(bullet == 5)
            {
                bulletType = BulletType.NyanCat;

            }
            else if(bullet == 6)
            {
                bulletType = BulletType.Nuclear;

            }
            else if(bullet == 7)
            {
                bulletType = BulletType.Satellite;

            }
            _texture = texture;

            //satellite = texture[5];
            this.Position = Position;
            //this.bullet = bullet;
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, null, Color.White, Rotation + MathHelper.ToRadians(-160f),new Vector2(_texture.Width / 2, _texture.Height / 2), 0.1f, SpriteEffects.None, 0f);
        }

        public override void Reset()
        {

        }

        public override void Update(GameTime gameTime)
        {
            Position += Velocity;
            //Position += new Vector2(speed, speed);
            //switch (bulletType)
            //{
            //    case BulletType.Normal:
            //        Position += Velocity;
            //        break;

            //    case BulletType.Heavy:
            //        Position += Velocity;
            //        break;

            //    case BulletType.Missile:
            //        Position += Velocity;
            //        break;

            //    case BulletType.Cluster:
            //        Position += Velocity;
            //        break;

            //    case BulletType.Laser:
            //        Position += Velocity;
            //        break;

            //    case BulletType.NyanCat:
            //        Position += Velocity;
            //        break;

            //    case BulletType.Nuclear:
            //        Position += Velocity;
            //        break;

            //    case BulletType.Satellite:
            //        Position += Velocity;
            //        break;
            //}
        }

        public void shooting(float Rotation, float speed)
        {
            this.Rotation = Rotation;
            this.speed = speed;
            Velocity.X = (float)(-speed * Math.Cos(Rotation));
            Velocity.Y = (float)(-speed * Math.Sin(Rotation));
        }

        public bool hit()
        {
            if (Position.X > Singleton.SCREENWIDTH || Position.X < 0 || Position.Y > Singleton.SCREENHEIGHT || Position.Y < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
