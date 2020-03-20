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
        /// <param name="xPos">Номер столбца в котором содержится ячейка</param>
        /// <param name="yPos">Номер строки в котором содержится ячейка</param>
        public WorldCell(int xPos, int yPos, CellType type)
        {
            Y = yPos;
            X = xPos;
            TypeOfCell = type;
        }

        /// <summary>
        /// Номер столбца в котором содержится ячейка
        /// </summary>
        public int X { get; private set; }

        /// <summary>
        /// Номер строки в котором содержится ячейка
        /// </summary>
        public int Y { get; private set; }

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
