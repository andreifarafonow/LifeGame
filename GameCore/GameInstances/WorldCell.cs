using System;
using System.Collections.Generic;
using System.Text;

namespace GameCore.GameInstances
{
    /// <summary>
    /// Класс ячейки мира.
    /// </summary>
    public class WorldCell
    {
        /// <summary>
        /// Создаёт новую ячейку мира по заданным координатам
        /// </summary>
        /// <param name="xPos">Номер столбца в котором содержится ячейка</param>
        /// <param name="yPos">Номер строки в котором содержится ячейка</param>
        public WorldCell(int xPos, int yPos, CellType type)
        {
            Nposition = yPos;
            Mposition = xPos;
            TypeOfCell = type;
        }

        /// <summary>
        /// Номер строки в котором содержится ячейка
        /// </summary>
        public int Nposition { get; private set; }

        /// <summary>
        /// Номер столбца в котором содержится ячейка
        /// </summary>
        public int Mposition { get; private set; }

        /// <summary>
        /// Тип ячейки
        /// </summary>
        public CellType TypeOfCell
        { get; set; }

        public enum CellType
        {
            Water,
            Ground
        }
    }
}
