using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
            Start, Shooting, EndTurn
        }
        public Stage stage;

        KeyboardState _currentKey;
        MouseState _mouseState, _previousMouseState;

        int speed;
        //Gun gun;

        public Ship(Texture2D texture) : base(texture)
        {
            speed = 5;
            moving = 100;
            stage = Stage.Start;
        }
        public override void Update(GameTime gameTime)
        {
            switch (stage)
            {
                case Stage.Start:
                    moveTo();
                    aiming();
                    
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
            spriteBatch.Draw(_texture, Position, null, Color.White, 0f, Vector2.Zero, 0.25f, SpriteEffects.None, 0f);

            //Hud Power, Fuel, Health
        }

        public override void Reset()
        {
            //Restart ship 
        }

        private void shoot()
        {
            //Click Mouse
            
            //Create ball

            //Change Stage
        }

        private void aiming()
        {
            if (_mouseState.LeftButton == ButtonState.Pressed && _previousMouseState.LeftButton == ButtonState.Released)
            {
                stage = Stage.Shooting;
            }
        }

        private void moveTo()
        {
            //Moving
            _currentKey = Keyboard.GetState();
            if (_currentKey.IsKeyDown(Keys.Up) && moving > 0)
            {
                Position = new Vector2(Position.X, Position.Y - speed);
                moving -= 1;
            }

            else if (_currentKey.IsKeyDown(Keys.Down) && moving > 0)
            {
                Position = new Vector2(Position.X, Position.Y + speed);
                moving -= 1;
            }

            if (_currentKey.IsKeyDown(Keys.Right) && moving > 0)
            {
                Position = new Vector2(Position.X + speed, Position.Y);
                moving -= 1;
            }

            else if (_currentKey.IsKeyDown(Keys.Left) && moving > 0)
            {
                Position = new Vector2(Position.X - speed, Position.Y);
                moving -= 1;
            }
        }
    }
}
