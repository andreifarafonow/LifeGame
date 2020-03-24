using GameCore.GameEntities;
using System;
using System.Drawing;

namespace GameCore.GameServices.MapServices
{
    public class MatrixMap : IMap
    {
        WorldCell[,] map;

        public WorldCell this[int y, int x] 
        {
            get => map[y, x];
            set => map[y, x] = value; 
        }

        /// <summary>
        /// Размер игрового поля
        /// </summary>
        public Size Size { get; private set; }

        /// <summary>
        /// Создание пустой карты заданных размеров
        /// </summary>
        /// <param name="size">Размер игрового поля</param>
        public void Initialize(Size size)
        {
            map = new WorldCell[size.Height, size.Width];

            Size = size;
        }
    }
}
