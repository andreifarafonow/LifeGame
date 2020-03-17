using System;

namespace GameCore
{
    public class Game
    {
        /// <param name="N">Высота игрового поля</param>
        /// <param name="M">Ширина игрового поля</param>
        public Game(int n, int m)
        {
            N = n;
            M = m;
        }

        /// <summary>
        /// Запускает игру
        /// </summary>
        public void Start()
        {
            
        }

        /// <summary>
        /// Высота игрового поля
        /// </summary>
        public int N { get; private set; }

        /// <summary>
        /// Ширина игрового поля
        /// </summary>
        public int M { get; private set; }
    }

    abstract class GameObject
    {
        
    }

    /// <summary>
    /// Класс ячейки мира.
    /// </summary>
    class WorldCell
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
        { get; private set; }

        public enum CellType
        {
            Water,
            Ground
        }
    }
}
