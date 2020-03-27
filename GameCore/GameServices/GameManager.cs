using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using GameCore.GameEntities;
using GameCore.GameServices.MapServices;
using GameCore.GameServices.ObjectsServices;
using static GameCore.GameEntities.Animal;

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

        IGameObjectsContainer ObjectsContainer { get; }

        Random Random { get; }

        

        /// <summary>
        /// Список объектов, размещённых на карте
        /// </summary>
        public GameObject[] GameObjects
        {
            get => ObjectsContainer.GetAllObjects().ToArray();
        }

        public GameManager(IMapGenerator mapGenerator, ISettlement settlement, IGameObjectsContainer objectsContainer, Random random)
        {
            MapGenerator = mapGenerator;
            Settlement = settlement;
            ObjectsContainer = objectsContainer;
            Random = random;
        }

        /// <summary>
        /// Генерирует карту и заселяет её объектами
        /// </summary>
        /// <param name="size">Размер карты</param>
        /// <param name="objectsNumber">Кол-во объектов для заселения</param>
        public void Initialize(Size size, int objectsNumber)
        {
            Map = MapGenerator.Generate(size);
            Settlement.Populate(objectsNumber);
        }

        Point RandomDirection(Point from)
        {
            int dir = Random.Next(4);

            switch(dir)
            {
                case 0:
                    return new Point(from.X, from.Y + 1);
                case 1:
                    return new Point(from.X, from.Y - 1);
                case 2:
                    return new Point(from.X - 1, from.Y);
                case 3:
                    return new Point(from.X + 1, from.Y);
            }
            throw new Exception();
        }

        public void Step()
        {
            var animals = ObjectsContainer.GetAllObjects().OfType<Animal>();

            foreach (var animal in animals)
            {
                MovingType movingType;
                Point newPosition;

                do
                {
                    movingType = animal.RandomPossibleMovingType();
                    newPosition = RandomDirection(animal.Position);
                }
                while (!animal.CanMoveTo(Map[newPosition.Y, newPosition.X], GameObjects.Where(obj => obj.Position == newPosition), movingType));

                animal.Position = newPosition;
            }
        }
    }
}
