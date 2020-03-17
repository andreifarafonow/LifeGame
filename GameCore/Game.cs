using System;

namespace GameCore
{
    public class Game
    {
        WorldCell[,] map;

        public WorldCell[,] Map
        {
            get => (WorldCell[,])map.Clone(); 
        }

        public static Random randomSingletone { get; } = new Random();

        /// <param name="N">Высота игрового поля</param>
        /// <param name="M">Ширина игрового поля</param>
        public Game(int n, int m)
        {
            Height = n;
            Width = m;

            map = new WorldCell[n, m];
        }

        /// <summary>
        /// Запускает игру
        /// </summary>
        public void Start()
        {
            GenerateRandomMap();
        }

        void GenerateRandomMap()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    var type = (WorldCell.CellType)randomSingletone.Next(0, Enum.GetNames(typeof(WorldCell.CellType)).Length);
                    map[i, j] = new WorldCell(i, j, type);
                }
            }
        }

        /// <summary>
        /// Высота игрового поля
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Ширина игрового поля
        /// </summary>
        public int Width { get; private set; }
    }

    abstract class GameObject
    {
        
    }

    /// <summary>
    /// Класс ячейки мира.
    /// </summary>
    public class WorldCell
    {
        /// <summary>
        /// Создаёт новую ячейку мира по заданным координатам
        /// </summary>
        /// <param name="nPos">Номер строки в котором содержится ячейка</param>
        /// <param name="mPos">Номер столбца в котором содержится ячейка</param>
        public WorldCell(int nPos, int mPos, CellType type)
        {
            Nposition = nPos;
            Mposition = mPos;
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
