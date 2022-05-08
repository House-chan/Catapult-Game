using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Catapult.GameObjects
{
    class Bullet
    {
        Texture2D _texture;
        Texture2D satellite;
        Vector2 Position;
        Vector2 Velocity;
        float Rotation;
        Vector2 Scale;
        float speed;
        Boolean isActive;
        Boolean isPlayerBullet;


        public string Name;

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

        public Bullet(Texture2D texture, float Rotation, int bullet, float power, Vector2 Position)
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

            speed = power;

            //satellite = texture[5];
            this.Position = Position;
            Scale = Vector2.One;
            //Rotation = 0f;
            this.Rotation = Rotation;
            //this.bullet = bullet;
            Velocity.X = (float)(-speed * Math.Cos(Rotation));
            Velocity.Y = (float)(-speed * Math.Sin(Rotation));
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, null, Color.White, Rotation, new Vector2(_texture.Width / 2, _texture.Height / 2), 10, SpriteEffects.None, 0f);
        }

        public void Reset()
        {

        }

        public virtual void Update(GameTime gameTime)
        {
            Position += Velocity;
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
    }
}
