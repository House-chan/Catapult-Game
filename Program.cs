using System;

namespace Catapult
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new GameScene())
                game.Run();
        }
    }
}
