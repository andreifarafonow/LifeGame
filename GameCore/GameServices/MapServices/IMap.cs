using GameCore.GameEntities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GameCore.GameServices.MapServices
{
    public interface IMap
    {
        /// <summary>
        /// Инициализирует пустую карту заданных размеров
        /// </summary>
        /// <param name="size">Размер карты</param>
        public void Initialize(Size size);
        public WorldCell this[int y, int x] { get; set; }

        /// <summary>
        /// Размер карты
        /// </summary>
        public Size Size { get; }
    }
}
