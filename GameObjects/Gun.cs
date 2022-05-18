using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Catapult.GameObjects
{


    class Gun : GameObject
    {
        public int bulletType;
        Vector2 Distance;
        public Bullet bullet;
        //public float power;
        bool bulletCreate = false;
        int[] ammo = { 99, 3, 2, 2, 1, 1, 1 };
        Texture2D[] bulletTexture;
        float velocity;

        public Gun(Texture2D texture, Texture2D[] bulletTexture) : base(texture)
        {
            this.bulletTexture = bulletTexture;
            //Normal,
            //Heavy,
            //Missile,
            //Cluster,
            //Laser,
            //NyanCat,
            //Nuclear,
            //Satellite
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, null, Color.White, Rotation + MathHelper.ToRadians(-170f), new Vector2(_texture.Width / 2, _texture.Height / 2), 1, SpriteEffects.None, 0f);
            spriteBatch.Draw(bulletTexture[bulletType], new Vector2(50, 700), null, Color.White, 0, new Vector2(bulletTexture[bulletType].Width / 2, bulletTexture[bulletType].Height / 2), 1, SpriteEffects.None, 0f);
            if (bulletCreate)
            {
                bullet.Draw(spriteBatch);
            }
        }

        public void DrawEnemy(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, null, Color.White, Rotation + MathHelper.ToRadians(10f), new Vector2(_texture.Width / 2, _texture.Height / 2), 1, SpriteEffects.None, 0f);
            if (bulletCreate)
            {
                bullet.Draw(spriteBatch);
            }
        }

        public override void Reset()
        {
            base.Reset();
        }

        public void Update(GameTime gameTime, List<EnemyShip> enemy, List<Planet> planet)
        {
            bullet.Update(gameTime, enemy, planet);
        }

        public void Update(GameTime gameTime, Ship player, List<Planet> planet)
        {
            bullet.Update(gameTime, player, planet);
        }

        public void shoot(float power)
        {
            bullet.shooting(Rotation, power);
        }

        public void aiming()
        {
            if (Singleton.Instance.CurrentMouse.Position.Y <= Singleton.SCREENHEIGHT && Singleton.Instance.CurrentMouse.Position.Y >= 0 &&
                Singleton.Instance.CurrentMouse.Position.X <= Singleton.SCREENWIDTH && Singleton.Instance.CurrentMouse.Position.X >= 0)
            {
                Distance.Y = -Singleton.Instance.CurrentMouse.Position.Y + (_texture.Height / 2) + Position.Y;
                Distance.X = -Singleton.Instance.CurrentMouse.Position.X + (_texture.Width / 2) + Position.X;
            }

            Rotation = (float)Math.Atan2(Distance.Y, Distance.X);
            Velocity.X = (float)(Math.Cos(Rotation));
            Velocity.Y = (float)(Math.Sin(Rotation));
        }

        public void aiming(Vector2 PlayerPosition)
        {

            //if (Singleton.Instance.CurrentMouse.Position.Y <= 560)
            //{
            Distance.Y = -PlayerPosition.Y + (_texture.Height / 2) + Position.Y;
            Distance.X = -PlayerPosition.X + (_texture.Width / 2) + Position.X;

            Rotation = (float)Math.Atan2(Distance.Y, Distance.X);
            Velocity.X = (float)(Math.Cos(Rotation));
            Velocity.Y = (float)(Math.Sin(Rotation));
            
        }

        public void reload()
        {
            bullet = new Bullet(bulletTexture[bulletType], bulletType, Position);
            bulletCreate = true;
            //bullet = null;
        }

        public void changeAmmo()
        {
            Singleton.Instance.CurrentKey = Keyboard.GetState();
            //change Ammo
            if (Singleton.Instance.PreviousKey.IsKeyDown(Keys.Right) && !Singleton.Instance.CurrentKey.IsKeyDown(Keys.Right))
            {
                bulletType++;
                if (bulletType > 3)
                {
                    bulletType = 0;
                }
            }

            else if (Singleton.Instance.PreviousKey.IsKeyDown(Keys.Left) && !Singleton.Instance.CurrentKey.IsKeyDown(Keys.Left))
            {
                bulletType--;
                if (bulletType < 0)
                {
                    bulletType = 3;
                }
            }
            Singleton.Instance.PreviousKey = Singleton.Instance.CurrentKey;
        }

        public void clearBullet()
        {
            bulletCreate = false;
            bullet = null;
        }
    }
}
