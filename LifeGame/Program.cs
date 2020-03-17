using System;
using GameCore;

namespace LifeGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game(10, 10);

            game.A[7] = 5;
        }
    }
}
