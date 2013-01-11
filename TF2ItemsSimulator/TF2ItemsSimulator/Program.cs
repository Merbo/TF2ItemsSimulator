using System;

namespace TF2ItemsSimulator
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Main game = new Main())
            {
                game.Run();
            }
        }
    }
#else
    static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This must be run on XBOX or Windows.");
        }
    }
#endif
}

