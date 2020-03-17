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

            var map = game.Map;

            DisplayMap(map);
        }

        static void DisplayMap(WorldCell[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int k = 0; k < 3; k++)
                {
                    for (int j = 0; j < map.GetLength(1); j++)
                    {
                        Console.Write(map[i, j].TypeOfCell == WorldCell.CellType.Ground ? "▒▒▒▒▒▒" : "▓▓▓▓▓▓");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
