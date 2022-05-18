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
        float G;


        public Bullet(Texture2D texture, int bullet, Vector2 Position) : base(texture)
        {
            if(bullet == 0)
            {
                bulletType = BulletType.Normal;
                G = 1500;
                damage = 50;
            }
            else if(bullet == 1)
            {
                bulletType = BulletType.Heavy;
                G = 5000;
                damage = 50;
            }
            else if (bullet == 2)
            {
                bulletType = BulletType.Missile;
                G = 2000;
                damage = 50;
            }
            else if(bullet == 3)
            {
                bulletType = BulletType.Cluster;
                G = 1500;
                damage = 50;
            }
            else if(bullet == 4)
            {
                bulletType = BulletType.Laser;
                G = 1500;
                damage = 50;
            }
            else if(bullet == 5)
            {
                bulletType = BulletType.NyanCat;
                G = 1500;
                damage = 50;
            }
            else if(bullet == 6)
            {
                bulletType = BulletType.Nuclear;
                G = 1500;
                damage = 50;
            }
            else if(bullet == 7)
            {
                bulletType = BulletType.Satellite;
                G = 1500;
                damage = 50;
            }
            _texture = texture;

            //satellite = texture[5];
            this.Position = Position;
            //this.bullet = bullet;
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, null, Color.White, Rotation + MathHelper.ToRadians(-160f),new Vector2(_texture.Width / 2, _texture.Height / 2), 1f, SpriteEffects.None, 0f);
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
                    Position += gravity(planet);
                    break;

                case BulletType.Cluster:
                    Position += gravity(planet);
                    break;

                case BulletType.Laser:
                    Position += gravity(planet);
                    break;

                case BulletType.NyanCat:
                    Position += gravity(planet);
                    break;

                case BulletType.Nuclear:
                    Position += gravity(planet);
                    break;

                case BulletType.Satellite:
                    Position += gravity(planet);
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

            //hit enemy
            foreach (EnemyShip sprite in EnemyPosition)
            {
                if (
                    ((Position.X + Singleton.BULLETSIZE >= sprite.Position.X  && Position.Y + Singleton.BULLETSIZE >= sprite.Position.Y) &&
                    (Position.X < (sprite.Position.X + Singleton.SHIPSIZE) && Position.Y < sprite.Position.Y + Singleton.SHIPSIZE)) ||
                    ((Position.X + Singleton.BULLETSIZE >= sprite.Position.X && Position.Y < (sprite.Position.Y + Singleton.SHIPSIZE)) &&
                    (Position.X < (sprite.Position.X + Singleton.SHIPSIZE) && Position.Y + Singleton.BULLETSIZE >= sprite.Position.Y))
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
            if (Position.X > Singleton.SCREENWIDTH || Position.X < 0)
            {
                if(Position.Y > Singleton.SCREENHEIGHT || Position.Y < 0)
                {
                    if(rand.Next(100) < 10)
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

            //hit enemy
            
            if (
                ((Position.X + Singleton.BULLETSIZE >= Player.Position.X && Position.Y + Singleton.BULLETSIZE >= Player.Position.Y) &&
                (Position.X < (Player.Position.X + Singleton.SHIPSIZE) && Position.Y < Player.Position.Y + Singleton.SHIPSIZE)) ||
                ((Position.X + Singleton.BULLETSIZE >= Player.Position.X && Position.Y < (Player.Position.Y + Singleton.SHIPSIZE)) &&
                (Position.X < (Player.Position.X + Singleton.SHIPSIZE) && Position.Y + Singleton.BULLETSIZE >= Player.Position.Y))
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
            }
        }

        Vector2 gravity(List<Planet> Planet)
        {
            Vector2 new_Velocity = Velocity;
            Vector2 r_hat, sum = Vector2.Zero;
            Vector2 r;
            float dis;


            foreach (Planet planet in Planet)
            {
                r = planet.Position - Position;
                
                dis = Vector2.Distance(Position, planet.Position);
                if(dis < planet.range)
                {
                    r_hat = Vector2.Normalize(Vector2.Subtract(planet.Position, Position));
                    sum = Vector2.Add(sum, Vector2.Multiply(r_hat, planet.Mass / 5000));
                }
            }
            new_Velocity = Vector2.Multiply(sum, G) + Velocity;


            return new_Velocity;
        }
    }
}
