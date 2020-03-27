using System;
using System.Collections.Generic;
using System.Drawing;
using static GameCore.GameEntities.Animal;

namespace GameCore.GameEntities
{
    public abstract class GameObject
    {
        public GameObject(Random random)
        {
            Random = random;
        }

        public delegate bool PlacementDelegate(WorldCell target, bool collision);

        /// <summary>
        /// Возвращает возможность объекта занимать данную ячейку, а также соседничать с другими объектами, находящимися в данной ячейке
        /// </summary>
        /// <param name="cell">Ячейка мира</param>
        /// <param name="neighbors">Соседи</param>
        /// <returns></returns>
        public abstract bool СanBeLocatedAt(WorldCell cell, IEnumerable<GameObject> neighbors);

        /// <summary>
        /// Идентификатор объекта
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Позиция объекта
        /// </summary>
        public Point Position { get; set; }
        public Random Random { get; }
    }
}
