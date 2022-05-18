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

        protected static float G = 3000;
        protected static float planet_diameter = 0;

        public static Vector2 CalVelocity(Vector2 position, Vector2 Velocity, List<Planet> Planet)
        {
            Vector2 new_Velocity = CalVelocity(position, Velocity, true, Planet);
            return new_Velocity;
        }

        public static Vector2 CalVelocity(Vector2 position, Vector2 Velocity, bool is_obj_have_mass, List<Planet> Planet)
        {
            Vector2 new_Velocity = Velocity;
            Vector2 r_hat, sum = Vector2.Zero;
            Vector2 r;
            float dis;


            foreach (Planet planet in Planet)
            {
                r = planet.Position - position;

                dis = Vector2.Distance(position, planet.Position);
                if (dis < planet.range && planet_diameter < dis)
                {
                    r_hat = Vector2.Normalize(Vector2.Subtract(planet.Position, position));
                    sum = Vector2.Add(sum, Vector2.Multiply(r_hat, planet.Mass / (float)Math.Pow((dis - planet_diameter), 2)));
                }
            }
            if (is_obj_have_mass)
            {
                new_Velocity = Vector2.Multiply(sum, G) + Velocity;
            }
            else
            {
                new_Velocity = Velocity;
            }


            return new_Velocity;
        }
    }
}
