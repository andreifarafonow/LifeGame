using GameCore.GameEntities;
using GameCore.GameServices;
using GameCore.GameServices.MapServices;
using GameCore.GameServices.ObjectsServices;
using Microsoft.Extensions.DependencyInjection;
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
            var serviceProvider = new ServiceCollection()
            .AddSingleton<IMap, MatrixMap>()
            .AddTransient<IMapGenerator, RandomMapGenerator>()
            .AddTransient<IGameObjectEstablishment, RandomSettlementAndMoving>()
            .AddSingleton(new Random())
            .AddSingleton<IGameObjectsContainer, ListGameObjectsContainer>()
            .AddSingleton<GameManager>()
            .BuildServiceProvider();

            _gameManager = serviceProvider.GetService<GameManager>();

            Size = size;
            ObjectsNumber = objectsNumber;
        }

        public void Start()
        {
            _gameManager.Initialize(Size, ObjectsNumber);
        }

        public void Step()
        {
            _gameManager.Step();
        }
    }
}
