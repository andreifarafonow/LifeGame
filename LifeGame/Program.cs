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

            void CellWriteLine(int x, string text)
            {
                Console.SetCursorPosition(xGrid + x * 9, Console.CursorTop);

                Console.WriteLine(text);
            }

            for (int y = 0; y < game.Map.GetLength(0); y++)
            {
                for (int x = 0; x < game.Map.GetLength(1); x++)
                {
                    char border = game.Map[y, x].TypeOfCell == WorldCell.CellType.Ground ? '▒' : '▓';                  

                    CellWriteLine(x, string.Concat(Enumerable.Repeat(border, 9)));
                    CellWriteLine(x, string.Concat(Enumerable.Repeat(border, 9))); 
                    CellWriteLine(x, string.Concat(Enumerable.Repeat(border, 9)));
                    CellWriteLine(x, string.Concat(Enumerable.Repeat(border, 9)));
                    CellWriteLine(x, string.Concat(Enumerable.Repeat(border, 9)));

                    Console.SetCursorPosition(40, 40);
                }
            }

            foreach (var obj in game.GameObjects)
            {
                Console.SetCursorPosition(xGrid + obj.X * 9 + 4, yGrid + obj.Y * 5 + 2);
                Console.Write(obj.ToString());
            }
        }
    }
}
