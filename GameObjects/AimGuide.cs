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
        protected float guide_length = 400;
        public bool is_active;
        //protected Gravity gravity;
        protected GravityDemo gravity;
        Ship player;
        float shootpower = 5.0f;

    
        public AimGuide(Ship _player, GravityDemo _gravity, Texture2D tex) : base(tex)
        {
            player = _player;
            gravity = _gravity;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //dfd
            if(player.gun != null)
            {
                if (player.getStage() == Ship.Stage.Start || player.getStage() == Ship.Stage.Shooting)
                {
                    shootpower = player.ShootPower;
                    float time = 10f;
                    Vector2 start_velocity;
                    start_velocity.X = (float)(-shootpower * Math.Cos(player.gun.Rotation));
                    start_velocity.Y = (float)(-shootpower * Math.Sin(player.gun.Rotation));
                    Vector2 cur_velocity = start_velocity;
                    Vector2 position = player.gun.Position;

                    for (int i = 0; i < guide_length; i++)
                    {
                        position += cur_velocity;
                        spriteBatch.Draw(_texture, position, null, new Color(255, 255, 255, 100), 0f, new Vector2(_texture.Width / 2, _texture.Height / 2), 0.03f*(1-(float)(i+1)/guide_length), SpriteEffects.None, 0);
                        cur_velocity = gravity.CalVelocity(position, cur_velocity, time);

                    }
                }
            }
            base.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            if (Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Pressed)
            {
                //ShootPower++
                shootpower += 0.1f;
            }
            if (Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Pressed && Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Released || shootpower > 10)
            {
                shootpower = 0.0f;
            }
            base.Update(gameTime);
        }
    }
}
