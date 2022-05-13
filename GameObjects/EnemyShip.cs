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
        public float Health;
        float moveRange;

        Vector2 Distance;
        float angle;

        public enum Stage
        {
            Start, Shooting, Move, EndTurn
        }

        public Stage stage;
        public Vector2 bullet;
        Gun gun;

        public EnemyShip(Texture2D texture, Texture2D gunTexture, Texture2D bulletTexture) : base(texture)
        {
            Health = 100;
            moveRange = 100;
            ShootPower = 10;
            stage = Stage.Start;
            gun = new Gun(gunTexture, bulletTexture)
            {
                Position = new Vector2(Position.X + 10, Position.Y + 30)
            };
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, null, Color.White, Rotation, new Vector2(_texture.Width / 2, _texture.Height / 2), 1, SpriteEffects.None, 0f);
            gun.Draw(spriteBatch);
        }

        public override void Reset()
        {
            base.Reset();
        }

        public void Update(GameTime gameTime, Vector2 PlayerPosition, List<Vector2> PlanetPosition)
        {
            switch (stage)
            {
                case Stage.Start:
                    //Moving

                    if (moveRange > 0)
                    {
                        Position += new Vector2(-1, 0);
                        gun.Position += new Vector2(-1, 0);
                        moveRange -= 1;
                    }
                    else
                    {
                        gun.aiming(PlayerPosition);
                        stage = Stage.Shooting;
                    }
                    //if (moveRange > 0)
                    //{
                        
                    //}
                    //else
                    //{
                    //}
                    break;
                case Stage.Shooting:
                    gun.reload();
                    gun.shoot(ShootPower);
                    stage = Stage.Move;
                    break;

                case Stage.Move:
                    //bullet.shooting(Rotation, power);
                    gun.Update(gameTime);
                    if (gun.bullet.hit(PlayerPosition, PlanetPosition))
                    {
                        bullet = gun.bullet.Position;
                        gun.clearBullet();
                        stage = Stage.EndTurn;
                    }
                    break;
                case Stage.EndTurn:
                    break;
            }
            
        }

        public void ResetAction()
        {
            //Action
            stage = Stage.Start;
            //Stat Moving
            moveRange = 100;
        }

        public void SetPosition(Vector2 pos)
        {
            Position = pos;
            gun.Position = new Vector2(pos.X + 10, pos.Y + 30);
        }
    }
}
