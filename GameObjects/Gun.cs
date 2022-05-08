using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Catapult.GameObjects
{


    class Gun : GameObject
    {
        int bulletType;
        Vector2 Distance;
        Bullet bullet;
        public float power;
        bool bulletCreate = false;
        int[] ammo = { 99, 3, 2, 2, 1, 1, 1 };
        Texture2D bulletTexture;
        public Gun(Texture2D texture, Texture2D bulletTexture) : base(texture)
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
            spriteBatch.Draw(_texture, Position, null, Color.White, Rotation + MathHelper.ToRadians(-160f), new Vector2(_texture.Width / 2, _texture.Height / 2), 1, SpriteEffects.None, 0f);
            if (bulletCreate)
            {
                bullet.Draw(spriteBatch);
            }
        }

        public override void Reset()
        {
            base.Reset();
        }

        public override void Update(GameTime gameTime)
        {
            bullet.Update(gameTime);
        }

        public void shoot()
        {
            //bullet
            //bullet = null;
        }

        public void aiming()
        {

            //if (Singleton.Instance.CurrentMouse.Position.Y <= 560)
            //{
            Distance.Y = -Singleton.Instance.CurrentMouse.Position.Y + (_texture.Height / 2) + Position.Y;
            Distance.X = -Singleton.Instance.CurrentMouse.Position.X + (_texture.Width / 2) + Position.X;

            Rotation = (float)Math.Atan2(Distance.Y, Distance.X);
            //if(angle > )
            //}
            //check angle
            //gun.update()
        }

        public void reload()
        {
            bullet = new Bullet(bulletTexture, Rotation, 0, power, Position);
            bulletCreate = true;
            //bullet = null;
        }

        public void changeAmmo()
        {
            //change Ammo
            if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.Right))
            {
                bulletType++;
                if (bulletType > 6)
                {
                    bulletType = 0;
                }
            }

            else if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.Left))
            {
                bulletType--;
                if (bulletType < 0)
                {
                    bulletType = 6;
                }
            }
        }
    }
}
