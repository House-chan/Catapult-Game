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

        enum BulletType
        {
            Normal,
            Heavy,
            Missile,
            Cluster,
            Laser,
            NyanCat,
            Nuclear,
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
                bulletType = BulletType.Cluster;
                haveMass = true;
                damage = 50;
            }
            else if(bullet == 4)
            {
                bulletType = BulletType.Laser;
                haveMass = true;
                damage = 50;
            }
            else if(bullet == 5)
            {
                bulletType = BulletType.NyanCat;
                haveMass = true;
                damage = 50;
            }
            else if(bullet == 6)
            {
                bulletType = BulletType.Nuclear;
                haveMass = true;
                damage = 50;
            }
            else if(bullet == 7)
            {
                bulletType = BulletType.Satellite;
                laser_range = 100;
                haveMass = true;
                damage = 1;
            }
            _texture = texture;

            //satellite = texture[5];
            this.Position = Position;
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

        public void Update(GameTime gameTime, List<EnemyShip> enemy, List<Planet> planet)
        {
            Velocity = gravity(planet);
            //Position += gravity(planet);
            //Position += new Vector2(speed, speed);
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

                case BulletType.Cluster:
                    Position += Velocity;
                    break;

                case BulletType.Laser:
                    Position += Velocity;
                    break;

                case BulletType.NyanCat:
                    Position += Velocity;
                    break;

                case BulletType.Nuclear:
                    Position += Velocity;
                    break;

                case BulletType.Satellite:
                    Position += Velocity;
                    break;
            }
            hit(enemy, planet);
        }

        public void Update(GameTime gameTime, Ship player, List<Planet> planet)
        {
            Velocity = gravity(planet);
            //Position += gravity(planet);
            Position += Velocity;
            hit(player, planet);
        }

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
            int bullet_screen_padding = 100;
            //hit enemy
            foreach (EnemyShip sprite in EnemyPosition)
            {
                if (
                    ////Left Up-Down
                    //((Position.X + width >= sprite.Position.X - sprite.width / 2 && Position.Y + height >= sprite.Position.Y) &&
                    //(Position.X + width >= sprite.Position.X - sprite.width / 2 && Position.Y < sprite.Position.Y + sprite.height / 2)) &&

                    ////Right Up-Down
                    //((Position.X + width <= sprite.Position.X + sprite.width && Position.Y + height >= sprite.Position.Y) &&
                    //(Position.X + width <= sprite.Position.X + sprite.width && Position.Y < sprite.Position.Y + sprite.height / 2)) &&

                    ////Up Left-Right
                    //((Position.X + width >= sprite.Position.X - sprite.width / 2 && Position.Y + height >= sprite.Position.Y) &&
                    //(Position.X + width <= sprite.Position.X + sprite.width && Position.Y + height >= sprite.Position.Y)) &&

                    ////Down Left-Right
                    //((Position.X + width >= sprite.Position.X - sprite.width / 2 && Position.Y < sprite.Position.Y + sprite.height / 2) &&
                    //(Position.X + width <= sprite.Position.X + sprite.width && Position.Y < sprite.Position.Y + sprite.height / 2)) 

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
            if (Position.X > Singleton.SCREENWIDTH + bullet_screen_padding || Position.X < -bullet_screen_padding)
            {
                if(Position.Y > Singleton.SCREENHEIGHT + bullet_screen_padding || Position.Y < bullet_screen_padding)
                {
                    if(rand.Next(100) < 1)
                    {
                        //_texture =
                        Position.Y = 50;
                        Velocity.X = (float)(speed * -2 * Math.Cos(-Rotation));
                        Velocity.Y = (float)(speed * -2 * Math.Sin(-Rotation));
                        bulletType = BulletType.Satellite;
                        end = false;
                        return false;
                    }
                    else
                    {
                        end = true;
                        return true;
                    }

                }
                else
                {
                    end = true;
                    return true;
                }
                
            }
                
            //not hit
            else
            {
                end = false;
                return false;
            }
        }

        //Overide Enemy Bullet
        public bool hit(Ship Player, List<Planet> PlanetPosition)
        {
            switch (bulletType)
            {
                case BulletType.Satellite:
                    //hit ship
                    if (
                        Vector2.Distance(Player.Position, Position) <= laser_range &&
                        Math.Abs(Rotation - Math.Atan2(Position.Y - Player.Position.Y, Position.X - Player.Position.X)) <= Math.Atan2(Player.width / (Position.Y - Player.Position.Y), Player.width / (Position.X - Player.Position.X))
                       )
                    {
                        Player.Health -= damage;
                        end = true;
                        return true;
                    }
                    else
                    {
                        end = false;
                        return false;
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
                            return true;
                        }
                        else
                        {
                            end = false;
                            return false;
                        }
                    }
                    break;

                default:

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
                    else
                    {
                        end = false;
                        return false;
                    };
                    break;
            }
        }
        
        //NOT USE THIS MEDTHOID
        Vector2 gravity(List<Planet> Planet)
        {
            Vector2 new_Velocity = Gravity.CalVelocity(Position, Velocity, haveMass, Planet);
            return new_Velocity;
        }
    }
}
