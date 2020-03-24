using System.Drawing;

namespace GameCore.GameEntities
{
    /// <summary>
    /// Класс ячейки мира.
    /// </summary>
    public class WorldCell
    {
        /// <summary>
        /// Создаёт новую ячейку мира по заданным координатам
        /// </summary>
        /// <param name="position">Номер строки и столбца в котором содержится ячейка</param>
        public WorldCell(Point position, CellType type)
        {
            Position = position;
            TypeOfCell = type;
        }

        /// <summary>
        /// Позиция ячейки
        /// </summary>
        public Point Position { get; private set; }

        /// <summary>
        /// Тип ячейки
        /// </summary>
        public CellType TypeOfCell { get; set; }

        public enum CellType
        {
            Water,
            Ground
        }
    }
}
