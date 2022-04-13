using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Catapult
{
    class Singleton
    {
        public const int SCREENWIDTH = 1600;
        public const int SCREENHEIGHT = 900;
        private static Singleton instance;

        private Singleton()
        {
        }

        public static Singleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Singleton();
                }
                return instance;
            }
        }
    }
}
