using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catapult.GameObjects
{
    class Ship : GameObject
    {
        float ShootPower;
        float Health;
        float moving;
        public enum Stage
        {
            Moving, Aiming, Shooting, EndTurn
        }
        public Stage stage;
        //Gun gun;

        public Ship(Texture2D texture) : base(texture)
        {

        }
        public override void Update(GameTime gameTime)
        {
            switch (stage)
            {
                case Stage.Moving:
                    moveTo();
                    stage = Stage.Aiming;
                    break;

                case Stage.Aiming:
                    //when click change stage save angle and Position

                    break;

                case Stage.Shooting:
                    shoot();
                    break;

                case Stage.EndTurn:

                    break;

            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Draw ship and method ammo
            spriteBatch.Draw(_texture, Position, null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
        }

        public override void Reset()
        {
            //Restart ship 
        }

        private void shoot()
        {
            //Create ball 
        }

        private void moveTo()
        {

        }
    }
}
