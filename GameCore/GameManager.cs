using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using GameCore.GameEntities;
using GameCore.GameServices.MapServices;
using GameCore.GameServices.ObjectsServices;

namespace GameCore
{
    public class GameManager
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

        bool IsInitialized { get; set; }

        List<GameObject> gameObjects = new List<GameObject>();

        /// <summary>
        /// Список объектов, размещённых на карте
        /// </summary>
        public GameObject[] GameObjects 
        {
            get => gameObjects.ToArray();
        }

        public static Random randomSingletone { get; } = new Random();

        public GameManager(IMapGenerator mapGenerator, ISettlement settlement)
        {
            MapGenerator = mapGenerator;
            Settlement = settlement;
        }

        Size Size { get; set; }
        int ObjectsNumber { get; set; }

        public void Initialize(Size size, int objectsNumber)
        {
            Size = size;
            ObjectsNumber = objectsNumber;

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
            Settlement.Populate(ObjectsNumber, gameObjects);
        }
    }

    
}
