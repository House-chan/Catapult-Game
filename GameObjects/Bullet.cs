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
        float Rotation;
        Vector2 Scale;
        float speed;
        float[,] Velocity;//Vector2?
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
            Satellite,
            NyanCat,
            Nuclear
        };

        BulletType bullet;
        Bullet(Texture2D[] texture, float Rotation, BulletType bullet)
        {
            switch (bullet)
            {
                case BulletType.Normal:
                    _texture = texture[0];
                    break;
                case BulletType.Heavy:
                    _texture = texture[1];
                    break;
                case BulletType.Missile:
                    _texture = texture[2];
                    break;
                case BulletType.Cluster:
                    _texture = texture[3];
                    break;
                case BulletType.Laser:
                    _texture = texture[4];
                    break;
                case BulletType.Satellite:
                    
                    break;
                case BulletType.NyanCat:
                    _texture = texture[6];
                    break;
                case BulletType.Nuclear:
                    _texture = texture[7];
                    break;
            }
            satellite = texture[5];
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Rotation = 0f;

            this.Rotation = Rotation;
            this.bullet = bullet;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, null, Color.White, Rotation, new Vector2(_texture.Width / 2, _texture.Height / 2), 1, SpriteEffects.None, 0f);

        }

        public void Reset()
        {

        }

        public void Update(GameTime gameTime)
        {
            switch (bullet)
            {
                case BulletType.Normal:
                    break;
                case BulletType.Heavy:
                    break;
                case BulletType.Missile:
                    break;
                case BulletType.Cluster:
                    break;
                case BulletType.Laser:
                    break;
                case BulletType.Satellite:
                    break;
                case BulletType.NyanCat:
                    break;
                case BulletType.Nuclear:
                    break;
            }
        }
    }
}
