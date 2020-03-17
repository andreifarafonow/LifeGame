using System;
using GameCore;

namespace LifeGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game(5, 10);

            game.Start();

            for (int i = 0; i < game.Map.GetLength(0); i++)
            {
                for (int j = 0; j < game.Map.GetLength(1); j++)
                {
                    Console.Write(game.Map[i, j].TypeOfCell == WorldCell.CellType.Ground ? "▒": "▓");
                }
                Console.WriteLine();
            }
        }

        static void DisplayMap()
        {
            
        }
    }
}
