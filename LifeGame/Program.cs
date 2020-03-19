using System;
using System.Linq;
using GameCore;
using GameCore.GameInstances;

namespace LifeGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game(5, 10);

            game.Start();

            DisplayGame(game);
        }

        static void DisplayGame(Game game)
        {
            int xGrid = 0, yGrid = 0;

            void CellWriteLine(int x, int y, string text, int line)
            {
                Console.SetCursorPosition(xGrid + x * 9, yGrid + y * 5 + line);

                Console.WriteLine(text);
            }

            for (int y = 0; y < game.Map.GetLength(0); y++)
            {
                for (int x = 0; x < game.Map.GetLength(1); x++)
                {
                    char border = game.Map[y, x].TypeOfCell == WorldCell.CellType.Ground ? '▒' : '▓';                  

                    CellWriteLine(x, y, string.Concat(Enumerable.Repeat(border, 9)), 0);
                    CellWriteLine(x, y, string.Concat(Enumerable.Repeat(border, 9)), 1);
                    CellWriteLine(x, y, string.Concat(Enumerable.Repeat(border, 9)), 2);
                    CellWriteLine(x, y, string.Concat(Enumerable.Repeat(border, 9)), 3);
                    CellWriteLine(x, y, string.Concat(Enumerable.Repeat(border, 9)), 4);

                }
            }

            foreach (var obj in game.GameObjects)
            {
                Console.SetCursorPosition(xGrid + obj.X * 9 + 4, yGrid + obj.Y * 5 + 2);
                Console.Write(obj.ToString());
            }

            Console.SetCursorPosition(100, 100);
        }
    }
}
