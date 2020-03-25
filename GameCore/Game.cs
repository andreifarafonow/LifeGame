using GameCore.GameEntities;
using GameCore.GameServices.MapServices;
using GameCore.GameServices.ObjectsServices;
using System;
using System.Drawing;

namespace GameCore
{
    public class Game
    {
        public static Random randomSingletone { get; } = new Random();

        public IMap Map { get => _gameManager.Map; }

        /// <summary>
        /// Список объектов, размещённых на карте
        /// </summary>
        public GameObject[] GameObjects { get => _gameManager.GameObjects; }
        public Size Size { get; }
        public int ObjectsNumber { get; }

        GameManager _gameManager;

        public Game(Size size, int objectsNumber)
        {
            IMap map = new MatrixMap();
            IMapGenerator mapGenerator = new RandomMapGenerator(map, Game.randomSingletone);

            ISettlement settlement = new RandomSettlement(map, Game.randomSingletone);

            _gameManager = new GameManager(mapGenerator, settlement);

            Size = size;
            ObjectsNumber = objectsNumber;
        }

        public void Start()
        {
            _gameManager.Initialize(Size, ObjectsNumber);
        }
    }
}
