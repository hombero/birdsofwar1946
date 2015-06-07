using System;
namespace WindowsGame2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (BoW_main game = new BoW_main())
            {
                game.Window.Title = "Birds of War";
                game.Run();
            }
        }
    }
}

