using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catapult.GameObjects
{
    class AimGuide : GameObject
    {
        public Vector2 start_pos;
        protected float guide_length = 800;
        public bool is_active;
        //protected Gravity gravity;
        protected GravityDemo gravity;
        Ship player;

    
        public AimGuide(Ship _player, GravityDemo _gravity, Texture2D tex) : base(tex)
        {
            player = _player;
            gravity = _gravity;
        }

        //create circle texture.
        //ref:https://stackoverflow.com/questions/2519304/draw-simple-circle-in-xna#:~:text=public%20Texture2D%20GetColoredCircle,height)%3B%0A%20%20%20%20%20%20%20%20return%20texture%3B%0A%20%20%20%20%7D

        private Vector2 get_start_velocity()
        {
            Vector2 gun_direction;
            gun_direction.X = (float)Math.Cos(-player.gun.Rotation + MathHelper.ToRadians(-160f));
            gun_direction.Y = (float)Math.Sin(player.gun.Rotation + MathHelper.ToRadians(-160f));
            return Vector2.Multiply( gun_direction, player.ShootPower);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //dfd
            if(player.gun != null)
            {
                if (player.stage == Ship.Stage.Start)
                {
                    float time = 0.1f;
                    start_pos.X = (float)Math.Cos(player.gun.Rotation + MathHelper.ToRadians(-160f));
                    start_pos.Y = (float)Math.Sin(player.gun.Rotation + MathHelper.ToRadians(-160f));
                    start_pos = Vector2.Multiply(start_pos, 50);
                    start_pos = Vector2.Add(start_pos, player.gun.Position);

                    Vector2 cur_pos = start_pos;
                    Vector2 last_pos = Vector2.Subtract(start_pos, Vector2.Multiply( get_start_velocity(), time) );
                    Vector2 temp_pos;
                    float cur_length = 0;
                    while (cur_length <= guide_length)
                    {
                        spriteBatch.Draw(_texture, cur_pos, null, new Color(255, 255,0, 50), 0f, new Vector2(_texture.Width / 2, _texture.Height / 2), 0.05f, SpriteEffects.None, 0);

                        //update variable
                        temp_pos = cur_pos;
                        cur_pos = Vector2.Add( cur_pos, Vector2.Multiply( gravity.CalVelocity(cur_pos, last_pos, time), time ) );
                        last_pos = temp_pos;
                        cur_length = Vector2.Distance(cur_pos, start_pos);
                    }
                }
            }
            
            base.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            start_pos.X = (float) Math.Cos(player.gun.Rotation + MathHelper.ToRadians(-160f));
            start_pos.Y = (float) Math.Sin(player.gun.Rotation + MathHelper.ToRadians(-160f));
            start_pos = Vector2.Multiply(start_pos, 50);
            start_pos = Vector2.Add(start_pos, player.gun.Position);
            base.Update(gameTime);
        }
    }
}
