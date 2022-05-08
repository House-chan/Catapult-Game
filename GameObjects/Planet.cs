using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Catapult.GameObjects
{
    class Planet : GameObject
    {
        float Health;
        
        public Planet(Texture2D texture) : base(texture)
        {
            
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
