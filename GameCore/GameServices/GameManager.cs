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
        Random Random { get; }

        List<GameObject> gameObjects = new List<GameObject>();

        /// <summary>
        /// Список объектов, размещённых на карте
        /// </summary>
        public GameObject[] GameObjects
        {
            get => gameObjects.ToArray();
        }

        public GameManager(IMapGenerator mapGenerator, ISettlement settlement, Random random)
        {
            MapGenerator = mapGenerator;
            Settlement = settlement;
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
            Settlement.Populate(objectsNumber, gameObjects);
        }

        public enum MovingDirection
        {
            Right,
            Down,
            Left,
            Up
        }

        Point PosAfterDir(Point from, MovingDirection dir)
        {
            switch(dir)
            {
                case MovingDirection.Down:
                    return new Point(from.X, from.Y + 1);
                case MovingDirection.Up:
                    return new Point(from.X, from.Y - 1);
                case MovingDirection.Left:
                    return new Point(from.X - 1, from.Y);
                case MovingDirection.Right:
                    return new Point(from.X + 1, from.Y);
            }
            throw new Exception();
        }

        public void Step()
        {
            var animals = gameObjects.OfType<Animal>();

            foreach (var animal in animals)
            {
                MovingType movingType;
                MovingDirection dir;

                Point newPosition;

                do
                {
                    movingType = animal.RandomPossibleMovingType();
                    dir = (MovingDirection)Random.Next(Enum.GetNames(typeof(MovingDirection)).Length);

                    newPosition = PosAfterDir(animal.Position, dir);
                }
                while (!animal.CanMoveTo(Map[newPosition.Y, newPosition.X], GameObjects.Where(obj => obj.Position == newPosition), movingType));

                animal.Position = newPosition;
            }
        }
    }
}
