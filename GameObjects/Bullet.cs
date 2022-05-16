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
                damage = 50;
            }
            else if(bullet == 1)
            {
                bulletType = BulletType.Heavy;
                damage = 50;
            }
            else if (bullet == 2)
            {
                bulletType = BulletType.Missile;
                damage = 50;
            }
            else if(bullet == 3)
            {
                bulletType = BulletType.Cluster;
                damage = 50;
            }
            else if(bullet == 4)
            {
                bulletType = BulletType.Laser;
                damage = 50;
            }
            else if(bullet == 5)
            {
                bulletType = BulletType.NyanCat;
                damage = 50;
            }
            else if(bullet == 6)
            {
                bulletType = BulletType.Nuclear;
                damage = 50;
            }
            else if(bullet == 7)
            {
                bulletType = BulletType.Satellite;
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

        public void Update(GameTime gameTime, List<Planet> planet)
        {
            //Position += gravity(planet);
            //Position += new Vector2(speed, speed);
            switch (bulletType)
            {
                case BulletType.Normal:
                    Position += gravity(planet);
                    break;

                case BulletType.Heavy:
                    Position += gravity(planet);
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
                    return true;
                }
                //if (IsTouchingTop(Pos) ||
                //    IsTouchingBottom(Pos)) return true;
                //if (IsTouchingLeft(Pos) ||
                //    IsTouchingRight(Pos)) return true;
                //if ((this.Velocity.X > 0 && this.IsTouchingLeft(sprite)) ||
                //    (this.Velocity.X < 0 && this.IsTouchingRight(sprite)))
                //{
                //    this.Velocity.X = 0;
                //}

                //if ((this.Velocity.Y > 0 && this.IsTouchingTop(sprite)) ||
                //    (this.Velocity.Y < 0 && this.IsTouchingBottom(sprite)))
                //{
                //    this.Velocity.Y = 0;
                //}

            }

            //foreach (Planet sprite in PlanetPosition)
            //{
            //    if (
            //        ((Position.X + Singleton.BULLETSIZE >= sprite.Position.X && Position.Y + Singleton.BULLETSIZE >= sprite.Position.Y) &&
            //        (Position.X < (sprite.Position.X + Singleton.SHIPSIZE) && Position.Y < sprite.Position.Y + Singleton.SHIPSIZE)) ||
            //        ((Position.X + Singleton.BULLETSIZE >= sprite.Position.X && Position.Y < (sprite.Position.Y + Singleton.SHIPSIZE)) &&
            //        (Position.X < (sprite.Position.X + Singleton.SHIPSIZE) && Position.Y + Singleton.BULLETSIZE >= sprite.Position.Y))
            //        )
            //    {
            //        sprite.Health -= 1;
            //        return true;
            //    }
           
            //}

            //outscreen
            if (Position.X > Singleton.SCREENWIDTH || Position.X < 0 || Position.Y > Singleton.SCREENHEIGHT || Position.Y < 0)
            {
                return true;
            }
                
            //not hit
            else
            {
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
                return true;
            }
            

            //foreach (Planet Pos in PlanetPosition)
            //{
            //    if (
            //        ((Position.X + Singleton.BULLETSIZE >= Pos.Position.X && Position.Y + Singleton.BULLETSIZE >= Pos.Position.Y) &&
            //        (Position.X < (Pos.Position.X + Singleton.SHIPSIZE) && Position.Y < Pos.Position.Y + Singleton.SHIPSIZE)) ||
            //        ((Position.X + Singleton.BULLETSIZE >= Pos.Position.X && Position.Y < (Pos.Position.Y + Singleton.SHIPSIZE)) &&
            //        (Position.X < (Pos.Position.X + Singleton.SHIPSIZE) && Position.Y + Singleton.BULLETSIZE >= Pos.Position.Y))
            //        )
            //    {
            //        Pos.Health -= 1;
            //        return true;
            //    }
            //}

            //outscreen
            if (Position.X > Singleton.SCREENWIDTH || Position.X < 0 || Position.Y > Singleton.SCREENHEIGHT || Position.Y < 0)
            {
                return true;
            }

            //not hit
            else
            {
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
                    sum = Vector2.Add(sum, Vector2.Multiply(r_hat, planet.Mass / 500));
                }
            }
            new_Velocity = Vector2.Multiply(sum, Singleton.G) + Velocity;


            return new_Velocity;
        }
    }
}
