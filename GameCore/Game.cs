using GameCore.GameEntities;
using GameCore.GameServices.MapServices;
using GameCore.GameServices.ObjectsServices;
using Ninject;
using System;
using System.Drawing;

namespace GameCore
{
    public class Game
    {
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
            NinjectContext.SetUp(new ProductionNinjectConfig());

            _gameManager = NinjectContext.Kernel.Get<GameManager>();

            Size = size;
            ObjectsNumber = objectsNumber;
        }

        public void Start()
        {
            _gameManager.Initialize(Size, ObjectsNumber);
        }
    }
}
