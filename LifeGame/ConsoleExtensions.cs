using GameCore.GameEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LifeGame
{
    public static class ConsoleExtensions
    {
        public static ConsoleColor GetConsoleColor(this WorldCell.CellType celltype)
        {
            Dictionary<WorldCell.CellType, ConsoleColor> cellColors = new Dictionary<WorldCell.CellType, ConsoleColor>()
            {
                { WorldCell.CellType.Ground,  ConsoleColor.DarkGreen },
                { WorldCell.CellType.Water,  ConsoleColor.Cyan }
            };

            if (cellColors.ContainsKey(celltype))
                return cellColors[celltype];
            else
                throw new Exception("Для данного типа ячейки не задан фоновый цвет");
        }
    }
}
