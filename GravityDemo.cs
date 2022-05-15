//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using Catapult.GameObjects;

//namespace Catapult
//{
//    class GravityDemo : Gravity
//    {
//        protected Vector2 ceatral_point = new Vector2(Singleton.SCREENWIDTH / 2, Singleton.SCREENHEIGHT / 2);
//        protected float G = 5000;
//        protected float mass = 1;

//        public GravityDemo() : base()
//        { }

//        public GravityDemo(Bullet[] bullets) : base()
//        {
//            bullets = bullets;
//        }

//        public GravityDemo(Bullet bullet) : base()
//        {
//            base.bullets = new Bullet[1];
//            base.bullets[0] = bullet;
//        }


//        protected Vector2 cal_velocity(Bullet bullet, float elapsedTime)
//        {
//            //calculate sum of gravity forces on bullet
//            Vector2 new_Velocity = bullet.Velocity;
//            Vector2 r_hat;
//            Vector2 sum = Vector2.Zero;
//            Vector2 r;
//            float dis;


//            r = ceatral_point - bullet.Position;
//            dis = Vector2.Distance(bullet.Position, ceatral_point);
//            r_hat = Vector2.Normalize(Vector2.Subtract(ceatral_point, bullet.Position));
//            sum = Vector2.Multiply(r_hat, mass / (float)Math.Pow(dis, 2));

//            new_Velocity = Vector2.Multiply(sum, G * elapsedTime) + bullet.Velocity;


//            return new_Velocity;
//        }

//        public Vector2 CalVelocity(Vector2 position, Vector2 velocity, float elapsedTime)
//        {
//            //calculate sum of gravity forces on bullet
//            Vector2 new_Velocity = velocity;
//            Vector2 r_hat;
//            Vector2 sum = Vector2.Zero;
//            Vector2 r;
//            float dis;


//            r = ceatral_point - position;
//            dis = Vector2.Distance(position, ceatral_point);
//            r_hat = Vector2.Normalize(Vector2.Subtract(ceatral_point, position));
//            sum = Vector2.Multiply(r_hat, mass / (float)Math.Pow(dis, 2));

//            new_Velocity = Vector2.Multiply(sum, G * elapsedTime) + velocity;


//            return new_Velocity;
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

        
//    }
//}