using System;
using System.Collections.Generic;
using GameCore.GameInstances;

namespace GameCore
{
    public class Game
    {
        WorldCell[,] map;

        public WorldCell[,] Map
        {
            get => (WorldCell[,])map.Clone(); 
        }

        protected List<GameObject> gameObjects = new List<GameObject>();

        /// <summary>
        /// Список объектов, размещённых на карте
        /// </summary>
        public GameObject[] GameObjects 
        {
            get => gameObjects.ToArray();
        }

        public static Random randomSingletone { get; } = new Random();

        /// <param name="N">Высота игрового поля</param>
        /// <param name="M">Ширина игрового поля</param>
        public Game(int n, int m)
        {
            Height = n;
            Width = m;

            map = new WorldCell[n, m];
        }

        /// <summary>
        /// Запускает игру
        /// </summary>
        public void Start()
        {
            GenerateRandomMap();
            GenerateObjectsOnMap(100);
        }

        private void GenerateObjectsOnMap(int num)
        {
            for (int i = 0; i < num; i++)
            {
                gameObjects.Add(new SolidObject(this));
            }
        }

        void GenerateRandomMap()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    var type = (WorldCell.CellType)randomSingletone.Next(0, Enum.GetNames(typeof(WorldCell.CellType)).Length);
                    map[i, j] = new WorldCell(i, j, type);
                }
            }
        }

        /// <summary>
        /// Высота игрового поля
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Ширина игрового поля
        /// </summary>
        public int Width { get; private set; }
    }

    
}
