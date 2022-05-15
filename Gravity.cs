//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using Catapult.GameObjects;

//namespace Catapult
//{
//    class Gravity
//    {

//        protected float G = 1;
//        protected Planet[] planets;
//        protected Bullet[] bullets;
        
//        protected Gravity(){}
        
//        public Gravity(Bullet[] bullets, Planet[] planets)
//        {
//            this.planets = planets;
//            this.bullets = bullets;
//        }

//        public Gravity(Bullet bullet, Planet[] planets)
//        {
//            bullets = new Bullet[1];
//            bullets[0] = bullet;

//            this.planets = planets;
//        }

//        public Gravity(Bullet[] bullets, Planet planet)
//        {
//            this.bullets = bullets;

//            planets = new Planet[1];
//            planets[0] = planet;
//        }

//        public Gravity(Bullet bullet, Planet planet)
//        {
//            bullets = new Bullet[1];
//            bullets[0] = bullet;

//            planets = new Planet[1];
//            planets[0] = planet;

//        }

//        protected Vector2 cal_velocity(Bullet bullet, float elapsedTime)
//        {
//            //calculate sum of gravity forces on bullet
//            Vector2 new_Velocity = bullet.Velocity;
//            Vector2 r_hat, sum = Vector2.Zero;
//            Vector2 r;
//            float dis;

            
//            if (planets.Length > 0)
//            {
//                foreach (Planet planet in planets)
//                {
//                    r = planet.Position - bullet.Position;
//                    dis = Vector2.Distance(bullet.Position, planet.Position);
//                    r_hat = Vector2.Normalize(Vector2.Subtract(planet.Position, bullet.Position));
//                    sum = Vector2.Add(sum, Vector2.Multiply(r_hat, planet.Mass / (float)Math.Pow(dis, 2)));
//                }
//                new_Velocity = Vector2.Multiply(sum, G * elapsedTime) + bullet.Velocity;
//            }

//            return new_Velocity;
//        }
//        public Vector2 CalPosition(Vector2 position, float elapsedTime)
//        {
//            //calculate sum of gravity forces on bullet
//            Vector2 new_Pos = position;
//            Vector2 r_hat, sum = Vector2.Zero;
//            Vector2 r;
//            float dis;


//            if (planets.Length > 0)
//            {
//                foreach (Planet planet in planets)
//                {
//                    r = planet.Position - position;
//                    dis = Vector2.Distance(position, planet.Position);
//                    r_hat = Vector2.Normalize(Vector2.Subtract(planet.Position, position));
//                    sum = Vector2.Add(sum, Vector2.Multiply(r_hat, planet.Mass / (float)Math.Pow(dis, 2)));
//                }
//                new_Pos = Vector2.Multiply(sum, G * elapsedTime) + position;
//            }

//            return new_Pos;
//        }

//        public void Update(Bullet bullet, float elapsedTime)
//        {
//            //calculate sum of gravity forces on bullet
//            if (bullet != null)
//            {
//                if (bullet.isActive)
//                {
//                    bullet.Velocity = cal_velocity(bullet, 1);
//                }
//            }
//        }

//        public void Update(Bullet bullet)
//        {
//            //calculate sum of gravity forces on bullet
//            if (bullet != null)
//            {
//                if (bullet.isActive)
//                {
//                    bullet.Velocity = cal_velocity(bullet, 1);
//                }
//            }
//        }

//        // Update all bullets in array.
//        public void Update(float elapsedTime)
//        {
//            //calculate sum of gravity forces on bullet
//            foreach (Bullet bullet in bullets)
//            {
//                if (bullet != null)
//                {
//                    if (bullet.isActive)
//                    {
//                        bullet.Velocity = cal_velocity(bullet, 1);
//                    }
//                }
//            }
//        }
//        public void Update()
//        {
//            //calculate sum of gravity forces on bullet
//            foreach (Bullet bullet in bullets)
//            {
//                if (bullet != null)
//                {
//                    if (bullet.isActive)
//                    {
//                        bullet.Velocity = cal_velocity(bullet, 1);
//                    }
//                }
//            }

//        }
//    }
//}
