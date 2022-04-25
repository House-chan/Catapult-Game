using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Catapult.GameObjects
{
    class Gun : GameObject
    {
        Vector2 Distance;
        Bullet bullet;

        Texture2D bulletTexture;
        public Gun(Texture2D texture, Texture2D bulletTexture) : base(texture)
        {
            this.bulletTexture = bulletTexture;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, null, Color.White, Rotation + MathHelper.ToRadians(-160f), new Vector2(_texture.Width / 2, _texture.Height / 2), 1, SpriteEffects.None, 0f);
            base.Draw(spriteBatch);
        }

        public override void Reset()
        {
            base.Reset();
        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

        public void shoot()
        {
            //bullet = new Bullet();
            bullet = null;
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
    }
}
