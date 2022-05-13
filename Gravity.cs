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
        Planet planet;
        Gravity(Planet[] planets) {
            planets = planets;  
        }

        Gravity(Planet planet)
        {
            planet = planet;
        }

        public Vector2 get_velocity(Bullet bullet, float elapsedTime)
        {
            //calculate sum of gravity forces on bullet
            Vector2 new_Velocity = bullet.Velocity;
            Vector2 r_hat, sum = Vector2.Zero;
            float r;
            
            if (bullet.isActive)
            {
                if (planets.Length > 0)
                {
                    foreach (Planet planet in planets)
                    {
                        r = Vector2.Distance(bullet.Position, planet.Position);
                        r_hat = Vector2.Normalize(bullet.Position - planet.Position);
                        sum = Vector2.Add(sum, Vector2.Multiply(r_hat, planet.Mass / (float)Math.Pow(r, 2)));
                    }
                    new_Velocity = Vector2.Multiply(sum, G * elapsedTime) + bullet.Velocity;
                }

            }
            else
            {
                new_Velocity = Vector2.Zero;
            }


            return new_Velocity;
        }

        public void update(Bullet bullet, float elapsedTime)
        {
            //calculate sum of gravity forces on bullet
            if (bullet.isActive)
            {
                bullet.Velocity = get_velocity(bullet, elapsedTime);
            }
        }
    }
}
