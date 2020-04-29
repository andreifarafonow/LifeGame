using System;
using System.Collections.Generic;
using System.Text;

namespace LifeGame
{
    public static class ConsoleHelper
    {
        public static void MaximizeConsole()
        {
            Console.WindowHeight = Console.LargestWindowHeight;
            Console.WindowWidth = Console.LargestWindowWidth;
        }
    }
}
