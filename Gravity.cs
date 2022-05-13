using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using Catapult.GameObjects;

namespace Catapult
{
    class Gravity
    {

        const float G = 1;
        Planet[] planets;
        Bullet[] bullets;
        public Gravity(Bullet[] bullets, Planet[] planets)
        {
            planets = planets;
            bullets = bullets;
        }

        public Gravity(Bullet bullet, Planet[] planets)
        {
            Array.Resize(ref bullets, bullets.Length + 1);
            bullets[bullets.Length - 1] = bullet;
            
            planets = planets;
        }

        public Gravity(Bullet[] bullets, Planet planet)
        {
            bullets = bullets;

            Array.Resize(ref planets, planets.Length + 1);
            planets[planets.Length - 1] = planet;
        }

        public Gravity(Bullet bullet, Planet planet)
        {
            Array.Resize(ref bullets, bullets.Length + 1);
            bullets[bullets.Length - 1] = bullet;

            Array.Resize(ref planets, planets.Length + 1);
            planets[planets.Length - 1] = planet;

        }

        protected Vector2 cal_velocity(Bullet bullet, float elapsedTime)
        {
            //calculate sum of gravity forces on bullet
            Vector2 new_Velocity = bullet.Velocity;
            Vector2 r_hat, sum = Vector2.Zero;
            Vector2 r;
            float dis;

            
            if (planets.Length > 0)
            {
                foreach (Planet planet in planets)
                {
                    r = planet.Position - bullet.Position;
                    dis = Vector2.Distance(bullet.Position, planet.Position);
                    // might use Vector2.Subtract
                    r_hat = Vector2.Normalize(planet.Position - bullet.Position);   
                    sum = Vector2.Add(sum, Vector2.Multiply(r_hat, planet.Mass / (float)Math.Pow(dis, 2)));
                }
                new_Velocity = Vector2.Multiply(sum, G * elapsedTime) + bullet.Velocity;
            }

            return new_Velocity;
        }

        public void update(Bullet bullet, float elapsedTime)
        {
            //calculate sum of gravity forces on bullet
            if (bullet.isActive)
            {
                bullet.Velocity = cal_velocity(bullet, elapsedTime);
            }
        }

        public void update(Bullet bullet)
        {
            //calculate sum of gravity forces on bullet
            if (bullet.isActive)
            {
                bullet.Velocity = cal_velocity(bullet, 1);
            }
        }
        public void update(float elapsedTime)
        {
            //calculate sum of gravity forces on bullet
            foreach (Bullet bullet in bullets)
            {
                if (bullet.isActive)
                {
                    bullet.Velocity = cal_velocity(bullet, elapsedTime);
                }
            }
        }
        public void update()
        {
            //calculate sum of gravity forces on bullet
            foreach (Bullet bullet in bullets)
            {
                if (bullet.isActive)
                {
                    bullet.Velocity = cal_velocity(bullet, 1);
                }
            }
        }
    }
}
