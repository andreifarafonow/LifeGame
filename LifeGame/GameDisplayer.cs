﻿using GameCore;
using GameCore.GameEntities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LifeGame
{
    static class GameDisplayer
    {
        static int gridLeftMargin = 4, gridTopMargin = 2, 
                   cellWidth = 9, cellHeight = 5;

        static Dictionary<WorldCell.CellType, ConsoleColor> cellColors = new Dictionary<WorldCell.CellType, ConsoleColor>()
        {
            { WorldCell.CellType.Ground,  ConsoleColor.DarkGreen },
            { WorldCell.CellType.Water,  ConsoleColor.Cyan }
        };

        static void DrawCellBackground(int x, int y, WorldCell.CellType cellType)
        {
            for (int i = 0; i < cellHeight; i++)
            {
                Console.ForegroundColor = Console.BackgroundColor = cellColors[cellType];

                Console.SetCursorPosition(gridLeftMargin + x * cellWidth, gridTopMargin + y * cellHeight + i);

                Console.WriteLine(string.Concat(Enumerable.Repeat('█', cellWidth)));
            }
        }

        static void DrawObjectsNumInCell(int x, int y, int num)
        {
            Console.SetCursorPosition(gridLeftMargin + x * cellWidth + 3, gridTopMargin + y * cellHeight + 2);
            Console.Write($"[{num}]");
        }

        static void DrawObjectsNamesInCell(int x, int y, IEnumerable<GameObject> objects)
        {
            int offset = 0;

            foreach (var obj in objects)
            {
                string name = obj.ToString();
                Console.SetCursorPosition(gridLeftMargin + obj.Position.X * cellWidth + 4 - name.Length / 2, gridTopMargin + obj.Position.Y * cellHeight + 2 - offset);
                Console.Write(name);

                if (offset <= 0)
                    offset = -offset + 1;
                else
                    offset = -offset;
            }
        }

        public static void Display(GameManager game)
        {
            for (int y = 0; y < game.Map.Size.Height; y++)
            {
                for (int x = 0; x < game.Map.Size.Width; x++)
                {
                    DrawCellBackground(x, y, game.Map[y, x].TypeOfCell);

                    var objectsOnThisCell = game.GameObjects.Where(obj => obj.Position == new Point(x, y));

                    Console.ForegroundColor = ConsoleColor.Black;

                    if (objectsOnThisCell.Count() > 3)
                    {
                        DrawObjectsNumInCell(x, y, objectsOnThisCell.Count());
                    }
                    else if (objectsOnThisCell.Count() > 0)
                    {
                        DrawObjectsNamesInCell(x, y, objectsOnThisCell);
                    }
                }
            }

            Console.ResetColor();

            Console.SetCursorPosition(0, game.Map.Size.Height * cellHeight + gridTopMargin + 1);
        }
    }
}
