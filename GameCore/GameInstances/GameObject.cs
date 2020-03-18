using System;
using System.Collections.Generic;
using System.Text;

namespace GameCore.GameInstances
{
    abstract class GameObject
    {
        public GameObject(int xPos, int yPos, Game game)
        {
            X = xPos;
            Y = yPos;
            GameInstance = game;
            Map = game.Map;
        }

        /// <summary>
        /// Возвращает возможность позиционирования объекта в данных координатах
        /// </summary>
        /// <param name="map">Карта игры</param>
        /// <param name="x">Позиция объекта по X</param>
        /// <param name="y">Позиция объекта по Y</param>
        /// <returns></returns>
        protected abstract bool CanLocationAt();

        /// <summary>
        /// Номер столбца карты в котором находится объект
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Номер строки карты в котором находится объект
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Экземпляр игры, в которой существует игровой объект
        /// </summary>
        protected Game GameInstance { get; }

        /// <summary>
        /// Карта игры
        /// </summary>
        protected WorldCell[,] Map { get; }
    }

    class Animal : GameObject
    {
        private Animal(int xPos, int yPos, AnimalType type, Game game) : base(xPos, yPos, game)
        {
            TypeOfAnimal = type;
        }

        public AnimalType TypeOfAnimal { get; private set; }

        public enum AnimalType
        {
            Fish,
            Duck,
            /// <summary>
            /// Воробей
            /// </summary>
            Sparrow,
            Turtle,
            Rabbit
        }

        /// <summary>
        /// Генерирует животное случайного вида в свойственной ему среде
        /// </summary>
        /// <returns>Объект животного</returns>
        public static Animal GenerateRandomAnimal(Game game)
        {
            var type = (AnimalType)Game.randomSingletone.Next(Enum.GetNames(typeof(AnimalType)).Length);

            Animal result = new Animal(0, 0, type, game);

            do
            {
                result.X = Game.randomSingletone.Next(game.Width);
                result.Y = Game.randomSingletone.Next(game.Height);
            }
            while (!result.CanLocationAt());

            return result;
        }

        protected override bool CanLocationAt()
        {
            switch (TypeOfAnimal)
            {
                case AnimalType.Fish:
                    return Map[Y, X].TypeOfCell == WorldCell.CellType.Water;
            }
        }
    }
}
