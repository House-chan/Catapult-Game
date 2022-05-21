// class contain constant value.
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;


namespace Catapult
{
    class Singleton
    {
        public const int SCREENWIDTH = 1600;
        public const int SCREENHEIGHT = 900;
        private static Singleton instance;

        public KeyboardState PreviousKey, CurrentKey;
        public MouseState PreviousMouse, CurrentMouse;

        public const int SHIPSIZE = 125;
        public const int BULLETSIZE = 15;

        public const float G = 1500;

        public float sound = 1.0f;
        public float music = 1.0f;

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
