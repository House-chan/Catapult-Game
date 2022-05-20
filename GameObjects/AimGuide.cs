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
        int BulletType = 0;


        public AimGuide(Texture2D tex) : base(tex)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch, Ship player, List<Planet> planet)
        {
            switch (BulletType)
            {
                case 0:
                    DrawFor0(spriteBatch, player, planet);
                    break;

                case 1:
                    DrawFor1(spriteBatch, player, planet);
                    break;

                case 2:
                    DrawFor2(spriteBatch, player, planet);
                    break;

                case 3:
                    DrawFor3(spriteBatch, player, planet);
                    break;

                case 4:
                    DrawFor4(spriteBatch, player, planet);
                    break;

                case 5:
                    DrawFor5(spriteBatch, player, planet);
                    break;

                case 6:
                    DrawFor6(spriteBatch, player, planet);
                    break;

                case 7:
                    DrawFor7(spriteBatch, player, planet);
                    break;
            }
        }
        
        public void SetAimGuideBulletType(int type)
        {
            BulletType = type;
        }

        public void DrawFor0(SpriteBatch spriteBatch, Ship player, List<Planet> planet)
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

        public void DrawFor1(SpriteBatch spriteBatch, Ship player, List<Planet> planet)
        {
            DrawFor0(spriteBatch, player, planet);
        }

        public void DrawFor2(SpriteBatch spriteBatch, Ship player, List<Planet> planet)
        {
            DrawFor0(spriteBatch, player, planet);
        }

        public void DrawFor3(SpriteBatch spriteBatch, Ship player, List<Planet> planet)
        {
            DrawFor0(spriteBatch, player, planet);
        }

        public void DrawFor4(SpriteBatch spriteBatch, Ship player, List<Planet> planet)
        {
            DrawFor0(spriteBatch, player, planet);
        }

        public void DrawFor5(SpriteBatch spriteBatch, Ship player, List<Planet> planet)
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

        public void DrawFor6(SpriteBatch spriteBatch, Ship player, List<Planet> planet)
        {
            DrawFor0(spriteBatch, player, planet);
        }

        public void DrawFor7(SpriteBatch spriteBatch, Ship player, List<Planet> planet)
        {
            DrawFor0(spriteBatch, player, planet);
        }
    }
}
