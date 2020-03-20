using System;
using GameCore;

namespace LifeGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WindowHeight = Console.LargestWindowHeight;
            Console.WindowWidth = Console.LargestWindowWidth;

            var game = new Game(5, 10);

            game.Start();

            GameDisplayer.Display(game);
        }

        
    }
}
