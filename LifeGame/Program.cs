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
            int gridLeftMargin = 0, gridTopMargin = 0;

            void DrawCell(int xStart, int yStart, int x, int y, int cellWidth, int cellHeight, char brush)
            {
                for (int i = 0; i < cellHeight; i++)
                {
                    Console.SetCursorPosition(xStart + x * cellWidth, yStart + y * cellHeight + i);

                    Console.WriteLine(string.Concat(Enumerable.Repeat(brush, cellWidth)));
                }
            }

            for (int y = 0; y < game.Map.GetLength(0); y++)
            {
                for (int x = 0; x < game.Map.GetLength(1); x++)
                {
                    char border = game.Map[y, x].TypeOfCell == WorldCell.CellType.Ground ? '▒' : '▓';

                    DrawCell(gridLeftMargin, gridTopMargin, x, y, 9, 5, border);

                    var objectsOnThisCell = game.GameObjects.Where(obj => obj.X == x && obj.Y == y);

                    if (objectsOnThisCell.Count() > 3)
                    {
                        Console.SetCursorPosition(gridLeftMargin + x * 9 + 3, gridTopMargin + y * 5 + 2);
                        Console.Write($"[{objectsOnThisCell.Count()}]");
                    }
                    else if(objectsOnThisCell.Count() > 0)
                    {
                        int offset = 0;

                        foreach (var obj in objectsOnThisCell)
                        {
                            string name = obj.ToString();
                            Console.SetCursorPosition(gridLeftMargin + obj.X * 9 + 4 - name.Length / 2, gridTopMargin + obj.Y * 5 + 2 - offset);
                            Console.Write(name);

                            if(offset <= 0)
                            {
                                offset = -offset + 1;
                            }
                            else
                            {
                                offset = -offset;
                            }
                        }
                    }
                }
            }

            /*foreach (var obj in game.GameObjects)
            {
                string name = obj.ToString();
                Console.SetCursorPosition(xGrid + obj.X * 9 + 4 - name.Length / 2, yGrid + obj.Y * 5 + 2);
                Console.Write(name);
            }*/

            Console.SetCursorPosition(0, game.Height * 5);
        }
    }
}
