using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using GameCore.GameEntities;

namespace GameCore
{
    public class Game
    {
        WorldCell[,] map;

        /// <summary>
        /// Карта игры
        /// </summary>
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

        void GenerateRandomMap()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    var type = (WorldCell.CellType)randomSingletone.Next(0, Enum.GetNames(typeof(WorldCell.CellType)).Length);
                    map[i, j] = new WorldCell(new Point(i, j), type);
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
