using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Catapult.GameObjects
{
    class Planet : GameObject
    {
        float ShootPower;
        public float Health;

        Vector2 PlayerPosition;
        Vector2 Distance;
        public float range;
        Texture2D gunTexture;

        public float Mass = 1;

        public enum Stage
        {
            Shooting, Move, EndTurn
        }

        public Stage stage;

        public Planet(Texture2D texture) : base(texture)
        {
            Health = 100;
            range = 500;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, null, Color.White, Rotation + MathHelper.ToRadians(-160f), new Vector2(_texture.Width / 2, _texture.Height / 2), 1, SpriteEffects.None, 0f);
            //spriteBatch.Draw(new Rectangle(50, 20, Health, 20), Position, null, Color.White, Rotation + MathHelper.ToRadians(-160f), new Vector2(_texture.Width / 2, _texture.Height / 2), 1, SpriteEffects.None, 0f);
        }

        public override void Reset()
        {
            base.Reset();
        }

        public override void Update(GameTime gameTime)
        {
            

        }

    }
}
