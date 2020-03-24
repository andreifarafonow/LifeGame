using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using GameCore.GameEntities;
using GameCore.GameServices.MapServices;

namespace GameCore
{
    public class Game
    {
        /// <summary>
        /// Карта игры
        /// </summary>
        public IMap Map { get; private set; }
        public IMapGenerator MapGenerator { get; }
        bool IsInitialized { get; set; }

        protected List<GameObject> gameObjects = new List<GameObject>();

        /// <summary>
        /// Список объектов, размещённых на карте
        /// </summary>
        public GameObject[] GameObjects 
        {
            get => gameObjects.ToArray();
        }

        public static Random randomSingletone { get; } = new Random();

        /// <param name="size">размер игрового поля</param>
        public Game(IMapGenerator mapGenerator)
        {
            MapGenerator = mapGenerator;
        }

        Size Size { get; set; }

        public void Initialize(Size size)
        {
            Size = size;
            IsInitialized = true;
        }

        /// <summary>
        /// Запускает игру
        /// </summary>
        public void Start()
        {
            if (!IsInitialized)
                throw new Exception("Объект игры не проинициализирован");

            Map = MapGenerator.Generate(Size);
            GenerateObjectsOnMap(400);
        }

        /// <summary>
        /// Расставляет объекты на карте случайным образом
        /// </summary>
        /// <param name="num">Кол-во объектов для расстановки</param>
        private void GenerateObjectsOnMap(int num)
        {
            int solidCount = randomSingletone.Next(num);

            for (int i = 0; i < num; i++)
            {
                GameObject created;

                if (i < solidCount)
                    created = new SolidObject(this);
                else
                    created = new Animal(this);

                gameObjects.Add(created);
            }
        }
    }

    
}
