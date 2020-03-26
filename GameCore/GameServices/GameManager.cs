using System.Collections.Generic;
using System.Drawing;
using GameCore.GameEntities;
using GameCore.GameServices.MapServices;
using GameCore.GameServices.ObjectsServices;

namespace GameCore.GameServices
{
    class GameManager
    {
        /// <summary>
        /// Карта игры
        /// </summary>
        public IMap Map { get; private set; }

        /// <summary>
        /// Генератор карты
        /// </summary>
        IMapGenerator MapGenerator { get; }

        ISettlement Settlement { get; }

        List<GameObject> gameObjects = new List<GameObject>();

        /// <summary>
        /// Список объектов, размещённых на карте
        /// </summary>
        public GameObject[] GameObjects
        {
            get => gameObjects.ToArray();
        }

        public GameManager(IMapGenerator mapGenerator, ISettlement settlement)
        {
            MapGenerator = mapGenerator;
            Settlement = settlement;
        }

        /// <summary>
        /// Генерирует карту и заселяет её объектами
        /// </summary>
        /// <param name="size">Размер карты</param>
        /// <param name="objectsNumber">Кол-во объектов для заселения</param>
        public void Initialize(Size size, int objectsNumber)
        {
            Map = MapGenerator.Generate(size);
            Settlement.Populate(objectsNumber, gameObjects);
        }
    }
}
