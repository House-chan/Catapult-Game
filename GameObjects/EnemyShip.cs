using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Catapult.GameObjects
{
    class EnemyShip : GameObject
    {
        float ShootPower;
        float Health;
        float moveRange;

        Vector2 PlayerPosition;
        Vector2 Distance;
        float angle;
        Texture2D gunTexture;
        public enum Stage
        {
            Start, Shooting, Move, EndTurn
        }

        public Stage stage;

        public EnemyShip(Texture2D texture, Texture2D gun) : base(texture)
        {
            Health = 100;
            moveRange = 100;
            stage = Stage.Start;
            gunTexture = gun;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, null, Color.White, Rotation, new Vector2(_texture.Width / 2, _texture.Height / 2), 1, SpriteEffects.None, 0f);
            spriteBatch.Draw(gunTexture, new Vector2(this.Position.X + 90, this.Position.Y + 30), null, Color.White, angle + MathHelper.ToRadians(-160f), new Vector2(gunTexture.Width / 2, gunTexture.Height / 2), 1, SpriteEffects.None, 0f);
        }

        public override void Reset()
        {
            base.Reset();
        }

        public override void Update(GameTime gameTime)
        {
            switch (stage)
            {
                case Stage.Start:
                    if (moveRange > 0)
                    {
                        Position += new Vector2(-1, 0);
                        moveRange -= 1;
                    }
                    else
                    {
                        stage = Stage.Shooting;
                    }
                    break;
                case Stage.Shooting:
                    Distance.Y = -Singleton.Instance.CurrentMouse.Position.Y + (_texture.Height / 2) + PlayerPosition.Y;
                    Distance.X = -Singleton.Instance.CurrentMouse.Position.X + (_texture.Width / 2) + PlayerPosition.X;
                    angle = (float)Math.Atan2(Distance.Y, Distance.X);
                    break;
                case Stage.Move:
                    break;
                case Stage.EndTurn:
                    break;
            }
            
        }
        public Stage getStage()
        {
            return stage;
        }

    }
}
