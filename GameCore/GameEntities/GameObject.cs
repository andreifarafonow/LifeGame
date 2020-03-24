using GameCore.GameServices.MapServices;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace GameCore.GameEntities
{
    public abstract class GameObject
    {

        /// <summary>
        /// Возвращает возможность объекта занимать данную ячейку, а также соседничать с другими объектами, находящимися в данной ячейке
        /// </summary>
        /// <param name="cell">Ячейка мира</param>
        /// <param name="neighbors">Соседи</param>
        /// <returns></returns>
        public abstract bool CanLocationAt(WorldCell cell, IEnumerable<GameObject> neighbors);

        /// <summary>
        /// Идентификатор объекта
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Позиция объекта
        /// </summary>
        public Point Position { get; set; }
    }
}
