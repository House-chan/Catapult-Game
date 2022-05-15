using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catapult.GameObjects
{
    class AimGuide : GameObject
    {
        public Vector2 start_pos;
        protected float guide_length = 100;
        public bool is_active;
        //protected Gravity gravity;
        Ship player;
        float shootpower = 5.0f;


        public AimGuide(Texture2D tex) : base(tex)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch, Ship player, List<Planet> planet)
        {
                if (player.stage == Ship.Stage.Start || player.stage == Ship.Stage.Shooting)
                {
                    shootpower = player.ShootPower;
                    Vector2 start_velocity;
                    start_velocity.X = (float)(-shootpower * Math.Cos(player.gun.Rotation));
                    start_velocity.Y = (float)(-shootpower * Math.Sin(player.gun.Rotation));
                    Vector2 cur_velocity = start_velocity;
                    Vector2 position = player.gun.Position;

                    for (int i = 0; i < guide_length; i++)
                    {
                        position += cur_velocity;
                        spriteBatch.Draw(_texture, position, null, new Color(255, 255, 255, 100), 0f, new Vector2(_texture.Width / 2, _texture.Height / 2), 0.2f * (1 - (float)(i + 1) / guide_length), SpriteEffects.None, 0);
                        cur_velocity = Gravity(position, cur_velocity, planet);
                   
                    }
                }
        }
        Vector2 Gravity(Vector2 position, Vector2 Velocity, List<Planet> Planet)
        {
            Vector2 new_Velocity = Velocity;
            Vector2 r_hat, sum = Vector2.Zero;
            Vector2 r;
            float dis;


            foreach (Planet planet in Planet)
            {
                r = planet.Position - position;

                dis = Vector2.Distance(position, planet.Position);
                if (dis < planet.range)
                {
                    r_hat = Vector2.Normalize(Vector2.Subtract(planet.Position, position));
                    sum = Vector2.Add(sum, Vector2.Multiply(r_hat, planet.Mass / (float)Math.Pow(dis / 4, 2)));
                }
            }
            new_Velocity = Vector2.Multiply(sum, Singleton.G) + Velocity;


            return new_Velocity;
        }
    }


}
