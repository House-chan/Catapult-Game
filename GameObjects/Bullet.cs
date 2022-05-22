using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Catapult.GameObjects
{
    class Bullet : GameObject
    { 
        Texture2D satellite;
        float speed;
        Boolean isActive;
        Boolean isPlayerBullet;
        int damage;
        Random rand = new Random();
        public bool end;
        public float haveMass;
        //for BulletType laser
        float laser_range;
        int bullet_screen_padding;
        Texture2D laser_texture;
        float rand_dmg; // totol_damage = damage + rand_dmg
        float p_dmg = 0.3f; // proportion of rand_dmg/damage.
        enum BulletType
        {
            Normal,
            Heavy,
            Missile,
            Laser,
            NyanCat,
            Satellite
        };

        BulletType bulletType;


        public Bullet(Texture2D[] texture, int bullet, Vector2 Position)
        {
            Random rand = new Random();
            
            if (bullet == 0)
            {
                bulletType = BulletType.Normal;
                haveMass = 1.0f;
                _texture = texture[0];
                damage = 40;
            }
            else if(bullet == 1)
            {
                bulletType = BulletType.Heavy;
                haveMass = 3.0f;
                _texture = texture[1];
                damage = 60;
            }
            else if (bullet == 2)
            {
                bulletType = BulletType.Missile;
                haveMass = 1.0f;
                _texture = texture[2];
                damage = 60;
            }
            else if(bullet == 3)
            {
                bulletType = BulletType.Laser;
                laser_range = 1000;
                haveMass = 1.0f;
                _texture = texture[3];
                laser_texture = texture[5];
                damage = 2;
            }
            else if(bullet == 4)
            {
                bulletType = BulletType.NyanCat;
                haveMass = 0.0f;
                _texture = texture[4];
                damage = 50;
            }
            else if(bullet == 5)
            {
                bulletType = BulletType.Satellite;
                haveMass = 0.0f;
                damage = 50;
            }
            width = _texture.Width;
            height = _texture.Height;
            
            this.Position = Position;
            bullet_screen_padding = 100;

            rand_dmg = (float)rand.NextDouble() * 2 * p_dmg + (1f - p_dmg);
            damage = (int)(damage * rand_dmg);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (bulletType)
            {
                case BulletType.Laser:
                    spriteBatch.Draw(laser_texture, new Rectangle((int)Position.X, (int)Position.Y, (int)laser_range, 10), null, Color.Red, (float)Rotation , new Vector2(1, 1), SpriteEffects.None, 0f);                        
                    break;

                default:
                    spriteBatch.Draw(_texture, Position, null, Color.White, Rotation + MathHelper.ToRadians(-160f), new Vector2(_texture.Width / 2, _texture.Height / 2), 1f, SpriteEffects.None, 0f);
                    break;
            }
        }

        public override void Reset()
        {

        }

        //PlayerBullet
        public void Update(GameTime gameTime, List<EnemyShip> enemy, List<Planet> planet, Ship player)
        {
            
            switch (bulletType)
            {
                case BulletType.Normal:
                    Velocity = gravity(planet);
                    Position += Velocity;
                    break;

                case BulletType.Heavy:
                    Velocity = gravity(planet);
                    Position += Velocity;
                    break;

                case BulletType.Missile:
                    Singleton.Instance.CurrentKey = Keyboard.GetState();
                    if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.W))
                    {
                        Velocity *= 1.1f;
                    }

                    else if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.S))
                    {
                        Velocity /= 1.1f;
                        if((Velocity.Y < 1f && Velocity.Y > -1f) || (Velocity.X > -1f && Velocity.X < 1f))
                        {
                            end = true;
                            break;
                        }
                    }

                    if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.A))
                    {
                        Velocity.Y += 1f;
                    }
                    if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.D))
                    {
                        Velocity.Y -= 1f;
                    }


                    Velocity = gravity(planet);
                    Position += Velocity;
                    break;

                case BulletType.Laser:
                    Velocity = gravity(planet);
                    Rotation += 0.1f;
                    Position += Velocity;
                    break;

                case BulletType.NyanCat:
                    Position += Velocity;
                    break;

                case BulletType.Satellite:
                    Velocity = gravity(planet);
                    Position += Velocity;
                    break;
            }
            hit(enemy, planet, player);
        }

        //Enemy Bullet
        public void Update(GameTime gameTime, Ship player, List<Planet> planet)
        {
            Velocity = gravity(planet);
            //Position += gravity(planet);
            Position += Velocity;
            hit(player, planet);
        }

        //Set Bullet (Gun Reload())
        public void shooting(float Rotation, float speed)
        {
            this.Rotation = Rotation;
            this.speed = speed;
            Velocity.X = (float)(-speed * Math.Cos(Rotation));
            Velocity.Y = (float)(-speed * Math.Sin(Rotation));
        }

        //Player Bullet
        public bool hit(List<EnemyShip> EnemyPosition, List<Planet> PlanetPosition, Ship player)
        {
            if (bulletType == BulletType.Laser)
            {
                if (
                    Vector2.Distance(player.Position, Position) <= laser_range &&
                    Math.Abs(Rotation - Math.Atan2(Position.Y - player.Position.Y, Position.X - player.Position.X)) <= Math.Atan2(player.width / (Position.Y - player.Position.Y), player.width / (Position.X - player.Position.X))
                    )
                {
                    player.Health -= damage;
                    end = true;
                }
                //hit planet
                foreach (Planet sprite in PlanetPosition)
                {
                    if (
                        Vector2.Distance(sprite.Position, Position) <= laser_range &&
                        Math.Abs(Rotation - Math.Atan2(Position.Y - sprite.Position.Y, Position.X - sprite.Position.X)) <= Math.Atan2(sprite.width / (Position.Y - sprite.Position.Y), sprite.width / (Position.X - sprite.Position.X))
                        )
                    {
                        sprite.Health -= damage;
                        end = true;
                    }
                }
                //hit ship
                foreach (EnemyShip sprite in EnemyPosition)
                {
                    if (
                        Vector2.Distance(sprite.Position, Position) <= laser_range &&
                        Math.Abs(Rotation - Math.Atan2(Position.Y - sprite.Position.Y, Position.X - sprite.Position.X)) <= Math.Atan2(sprite.width / (Position.Y - sprite.Position.Y), sprite.width / (Position.X - sprite.Position.X))
                        )
                    {
                        sprite.Health -= damage;
                        end = true;
                        
                    }
                    else
                    {
                        end = false;
                    }
                }
                if (Position.X > Singleton.SCREENWIDTH + bullet_screen_padding || Position.X < -bullet_screen_padding || Position.Y > Singleton.SCREENHEIGHT)
                {
                    end = true;
                    return true;
                }                

            }
            else if(bulletType == BulletType.Satellite)
            {
                if(
                    ((Position.X + Singleton.BULLETSIZE >= player.Position.X - player.width / 2 && Position.Y + Singleton.BULLETSIZE >= player.Position.Y) &&
                    (Position.X < (player.Position.X + Singleton.SHIPSIZE + player.width / 2) && Position.Y < player.Position.Y + Singleton.SHIPSIZE)) ||
                    ((Position.X + Singleton.BULLETSIZE >= player.Position.X - player.width / 2 && Position.Y < (player.Position.Y + Singleton.SHIPSIZE)) &&
                    (Position.X < (player.Position.X + Singleton.SHIPSIZE + player.width / 2) && Position.Y + Singleton.BULLETSIZE >= player.Position.Y))
                    )
                {
                    player.Health -= damage;
                    end = true;
                    return true;
                }
                foreach (EnemyShip sprite in EnemyPosition)
                {
                    if (
                        ((Position.X + Singleton.BULLETSIZE >= sprite.Position.X - sprite.width / 2 && Position.Y + Singleton.BULLETSIZE >= sprite.Position.Y) &&
                        (Position.X < (sprite.Position.X + Singleton.SHIPSIZE + sprite.width / 2) && Position.Y < sprite.Position.Y + Singleton.SHIPSIZE)) ||
                        ((Position.X + Singleton.BULLETSIZE >= sprite.Position.X - sprite.width / 2 && Position.Y < (sprite.Position.Y + Singleton.SHIPSIZE)) &&
                        (Position.X < (sprite.Position.X + Singleton.SHIPSIZE + sprite.width / 2) && Position.Y + Singleton.BULLETSIZE >= sprite.Position.Y))
                        )
                    {
                        sprite.Health -= damage;
                        end = true;
                        return true;
                    }
                }

                foreach (Planet sprite in PlanetPosition)
                {
                    if (
                        ((Position.X + Singleton.BULLETSIZE >= sprite.Position.X - sprite.width / 2 && Position.Y + Singleton.BULLETSIZE >= sprite.Position.Y - sprite.height / 2) &&
                        (Position.X < (sprite.Position.X + Singleton.SHIPSIZE - sprite.width / 2) && Position.Y < sprite.Position.Y + Singleton.SHIPSIZE - sprite.height / 2)) ||
                        ((Position.X + Singleton.BULLETSIZE >= sprite.Position.X - sprite.width / 2 && Position.Y < (sprite.Position.Y + Singleton.SHIPSIZE - sprite.height / 2)) &&
                        (Position.X < (sprite.Position.X + Singleton.SHIPSIZE - sprite.width / 2) && Position.Y + Singleton.BULLETSIZE >= sprite.Position.Y - sprite.height / 2))
                        )
                    {
                        sprite.Health -= 1;
                        end = true;
                        return true;
                    }
                }

                //outscreen
                //if (Position.X > Singleton.SCREENWIDTH + bullet_screen_padding || Position.X < -bullet_screen_padding)
                if (Position.X > Singleton.SCREENWIDTH + bullet_screen_padding || Position.X < -bullet_screen_padding || Position.Y > Singleton.SCREENHEIGHT || Position.Y < -bullet_screen_padding)
                {
                    end = true;
                    return true;
                }
                //not hit
                end = false;
                return false;
            }
            else
            {
                //hit enemy
                foreach (EnemyShip sprite in EnemyPosition)
                {
                    if (
                        ((Position.X + Singleton.BULLETSIZE >= sprite.Position.X - sprite.width / 2 && Position.Y + Singleton.BULLETSIZE >= sprite.Position.Y) &&
                        (Position.X < (sprite.Position.X + Singleton.SHIPSIZE + sprite.width / 2) && Position.Y < sprite.Position.Y + Singleton.SHIPSIZE)) ||
                        ((Position.X + Singleton.BULLETSIZE >= sprite.Position.X - sprite.width / 2 && Position.Y < (sprite.Position.Y + Singleton.SHIPSIZE)) &&
                        (Position.X < (sprite.Position.X + Singleton.SHIPSIZE + sprite.width / 2) && Position.Y + Singleton.BULLETSIZE >= sprite.Position.Y))
                        )
                    {
                        sprite.Health -= damage;
                        end = true;
                        return true;
                    }
                }

                foreach (Planet sprite in PlanetPosition)
                {
                    if (
                        ((Position.X + Singleton.BULLETSIZE >= sprite.Position.X - sprite.width / 2 && Position.Y + Singleton.BULLETSIZE >= sprite.Position.Y - sprite.height / 2) &&
                        (Position.X < (sprite.Position.X + Singleton.SHIPSIZE - sprite.width / 2) && Position.Y < sprite.Position.Y + Singleton.SHIPSIZE - sprite.height / 2)) ||
                        ((Position.X + Singleton.BULLETSIZE >= sprite.Position.X - sprite.width / 2 && Position.Y < (sprite.Position.Y + Singleton.SHIPSIZE - sprite.height / 2)) &&
                        (Position.X < (sprite.Position.X + Singleton.SHIPSIZE - sprite.width / 2) && Position.Y + Singleton.BULLETSIZE >= sprite.Position.Y - sprite.height / 2))
                        )
                    {
                        sprite.Health -= 1;
                        end = true;
                        return true;
                    }
                }

                //outscreen
                //if (Position.X > Singleton.SCREENWIDTH + bullet_screen_padding || Position.X < -bullet_screen_padding)
                if (Position.X > Singleton.SCREENWIDTH + bullet_screen_padding || Position.X < -bullet_screen_padding || Position.Y > Singleton.SCREENHEIGHT)
                {
                    end = true;
                    return true; 
                }
                //if (Position.Y > Singleton.SCREENHEIGHT + bullet_screen_padding || Position.Y < -bullet_screen_padding)
                if (Position.Y < -bullet_screen_padding)
                {
                    if (rand.Next(100) > 1)
                    {
                        Position.Y = -bullet_screen_padding;
                        Velocity.X = (float)(speed * 2 * Math.Cos(-Rotation));
                        Velocity.Y = (float)(speed * -2 * Math.Sin(-Rotation));
                        bulletType = BulletType.Satellite;
                        end = false;
                    }
                    else
                    {
                        end = true;
                        return true;
                    }
                }
                //not hit
                end = false;
                return false;
            }
            return false;
        }

        //Overide Enemy Bullet
        public bool hit(Ship Player, List<Planet> PlanetPosition)
        {
            //hit enemy
            if (
                ((Position.X + Singleton.BULLETSIZE >= Player.Position.X - Player.width / 2 && Position.Y + Singleton.BULLETSIZE >= Player.Position.Y) &&
                (Position.X < (Player.Position.X + Singleton.SHIPSIZE + Player.width / 2) && Position.Y < Player.Position.Y + Singleton.SHIPSIZE)) ||
                ((Position.X + Singleton.BULLETSIZE >= Player.Position.X - Player.width / 2 && Position.Y < (Player.Position.Y + Singleton.SHIPSIZE)) &&
                (Position.X < (Player.Position.X + Singleton.SHIPSIZE + Player.width / 2) && Position.Y + Singleton.BULLETSIZE >= Player.Position.Y))
                )
            {
                Player.Health -= damage;
                end = true;
                return true;
            }


            foreach (Planet sprite in PlanetPosition)
            {
                if (
                    ((Position.X + Singleton.BULLETSIZE >= sprite.Position.X - sprite.width / 2 && Position.Y + Singleton.BULLETSIZE >= sprite.Position.Y - sprite.height / 2) &&
                    (Position.X < (sprite.Position.X + Singleton.SHIPSIZE - sprite.width / 2) && Position.Y < sprite.Position.Y + Singleton.SHIPSIZE - sprite.height / 2)) ||
                    ((Position.X + Singleton.BULLETSIZE >= sprite.Position.X - sprite.width / 2 && Position.Y < (sprite.Position.Y + Singleton.SHIPSIZE - sprite.height / 2)) &&
                    (Position.X < (sprite.Position.X + Singleton.SHIPSIZE - sprite.width / 2) && Position.Y + Singleton.BULLETSIZE >= sprite.Position.Y - sprite.height / 2))
                    )
                {
                    sprite.Health -= 1;
                    end = true;
                    return true;
                }
            }

            //outscreen
            if (Position.X > Singleton.SCREENWIDTH || Position.X < 0 || Position.Y > Singleton.SCREENHEIGHT || Position.Y < 0)
            {
                end = true;
                return true;
            }

            //not hit

            end = false;
            return false;
            

        }
        
        //NOT USE THIS MEDTHOID
        Vector2 gravity(List<Planet> Planet)
        {
            Vector2 new_Velocity = Gravity.CalVelocity(Position, Velocity, haveMass, Planet);
            return new_Velocity;
        }
    }
}
