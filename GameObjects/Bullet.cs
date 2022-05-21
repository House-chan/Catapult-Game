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
        public bool haveMass;
        //for BulletType Satellite
        float laser_range;
        int bullet_screen_padding;
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


        public Bullet(Texture2D texture, int bullet, Vector2 Position) : base(texture)
        {
            if(bullet == 0)
            {
                bulletType = BulletType.Normal;
                haveMass = true;
                damage = 50;
            }
            else if(bullet == 1)
            {
                bulletType = BulletType.Heavy;
                haveMass = true;
                damage = 50;
            }
            else if (bullet == 2)
            {
                bulletType = BulletType.Missile;
                haveMass = true;
                damage = 50;
            }
            else if(bullet == 3)
            {
                bulletType = BulletType.Laser;
                haveMass = true;
                damage = 50;
            }
            else if(bullet == 4)
            {
                bulletType = BulletType.NyanCat;
                haveMass = true;
                damage = 50;
            }
            else if(bullet == 5)
            {
                bulletType = BulletType.Satellite;
                haveMass = true;
                damage = 50;
            }
            _texture = texture;

            //satellite = texture[5];
            this.Position = Position;
            bullet_screen_padding = 100;
            //this.bullet = bullet;

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (bulletType)
            {
                case BulletType.Satellite:
                    float cur_range = 0;
                    while (cur_range < laser_range)
                    {
                        //TODO :draw laser here
                        cur_range += 10; // += texture width
                    }
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
        public void Update(GameTime gameTime, List<EnemyShip> enemy, List<Planet> planet)
        {
            Velocity = gravity(planet);
            switch (bulletType)
            {
                case BulletType.Normal:
                    Position += Velocity;
                    break;

                case BulletType.Heavy:
                    Position += Velocity;
                    break;

                case BulletType.Missile:
                    Position += Velocity;
                    break;

                case BulletType.Laser:
                    Position += Velocity;
                    break;

                case BulletType.NyanCat:
                    Position += Velocity;
                    break;

                case BulletType.Satellite:
                    Position += Velocity;
                    break;
            }
            hit(enemy, planet);
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
        public bool hit(List<EnemyShip> EnemyPosition, List<Planet> PlanetPosition)
        {
            if (bulletType == BulletType.Satellite)
            {
                Position = new Vector2(500, 500);
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
                        return true;
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
                        return true;
                    }
                    else
                    {
                        end = false;
                        return false;
                    }
                }
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
                    if (rand.Next(100) < 1)
                    {
                        Position.Y = -bullet_screen_padding;
                        Velocity.X = (float)(speed * -2 * Math.Cos(-Rotation));
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
