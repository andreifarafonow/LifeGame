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

        //review: слово neighbors вводит в заблуждение, т.к. первое, что можно подумать - это объекты на соседних клетках.
        //Должно быть достаточно передать только cell, а объекты на клетке получать внутри метода (для этого у Cell должен быть метод, возвращающий объекты на ней)
        //Чем меньше аргументов у метода, тем лучше.
        
        //(а для этого в конструктор Cell должен передаваться соответствующий сервис из DI, у которого можно спросить список объектов, чей Position совпадает с координатами клетки)
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

        //review: Random не может понадобится никому снаружи (и это нарушит инкапсуляцию), сделай просто поле private Random _random;
        public Random Random { get; }
    }
}
