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

        public void Draw(SpriteBatch spriteBatch, Ship player, List<Planet> planet, int BulletType)
        {
            switch (BulletType)
            {
                case 0:
                    DrawForDefault(spriteBatch, player, planet);
                    break;

                case 1:
                    DrawForDefault(spriteBatch, player, planet);
                    break;

                case 2:
                    DrawForDefault(spriteBatch, player, planet);
                    break;

                case 3:
                    DrawForPassThrough(spriteBatch, player, planet);
                    break;

                case 4:
                    DrawForPassThrough(spriteBatch, player, planet);
                    break;
            }
        }

        public void DrawForDefault(SpriteBatch spriteBatch, Ship player, List<Planet> planet)
        {
            if (player.stage == Ship.Stage.Start || player.stage == Ship.Stage.Shooting)
            {
                shootpower = player.ShootPower;
                Vector2 start_velocity;
                start_velocity.X = (float)(-shootpower * Math.Cos(player.gun.Rotation));
                start_velocity.Y = (float)(-shootpower * Math.Sin(player.gun.Rotation));
                Vector2 cur_velocity = start_velocity;
                Vector2 position = player.gun.Position;

                for (int i = 0; i < guide_length*2; i++)
                {
                    position += cur_velocity;
                    if (i % 2 == 0)
                    {
                        spriteBatch.Draw(_texture, position, null, new Color(255, 255, 255, 100), 0f, new Vector2(_texture.Width / 2, _texture.Height / 2), 0.3f * (1.7f - (float)(i + 1) / guide_length), SpriteEffects.None, 0);
                    }
                    cur_velocity = Gravity.CalVelocity(position, cur_velocity, planet);
                }
            }
        }

        void DrawForPassThrough(SpriteBatch spriteBatch, Ship player, List<Planet> planet)
        {
            if (player.stage == Ship.Stage.Start || player.stage == Ship.Stage.Shooting)
            {
                shootpower = player.ShootPower;
                Vector2 start_velocity;
                start_velocity.X = (float)(-shootpower * Math.Cos(player.gun.Rotation));
                start_velocity.Y = (float)(-shootpower * Math.Sin(player.gun.Rotation));
                Vector2 cur_velocity = start_velocity;
                Vector2 position = player.gun.Position;

                for (int i = 0; i < guide_length * 2; i++)
                {
                    position += cur_velocity;
                    if (i % 2 == 0)
                    {
                        spriteBatch.Draw(_texture, position, null, new Color(255, 255, 255, 100), 0f, new Vector2(_texture.Width / 2, _texture.Height / 2), 0.3f * (1.7f - (float)(i + 1) / guide_length), SpriteEffects.None, 0);
                    }
                }
            }
        }

    }
}
