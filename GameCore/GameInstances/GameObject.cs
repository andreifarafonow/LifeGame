using System;
using System.Linq;

namespace GameCore.GameInstances
{
    public abstract class GameObject
    {
        static int LastId { get; set; }


        /// <summary>
        /// Конструирует новый игровой объект в случайной, свойственной для него позиции
        /// </summary>
        /// <param name="game">Объект игры</param>
        protected GameObject(Game game)
        {
            GameInstance = game;
            Map = game.Map;

            Id = ++LastId;
        }

        /// <summary>
        /// Начальное позиционирование объекта на карте
        /// </summary>
        protected void StartPositionSet()
        {
            do
            {
                X = Game.randomSingletone.Next(GameInstance.Width);
                Y = Game.randomSingletone.Next(GameInstance.Height);
            }
            while (!CanLocationAt());
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
        /// Идентификатор объекта
        /// </summary>
        public int Id { get; }

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

    /// <summary>
    /// Класс животного. Наследуется от GameObject
    /// </summary>
    class Animal : GameObject
    {
        public Animal(Game game) : base(game)
        {
            TypeOfAnimal = (AnimalType)Game.randomSingletone.Next(Enum.GetNames(typeof(AnimalType)).Length);

            StartPositionSet();
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

        protected override bool CanLocationAt()
        {
            bool colilision = GameInstance.GameObjects.Any(obj => obj.X == X && obj.Y == Y && obj is SolidObject);

            var inCell = GameInstance.GameObjects.Where(obj => obj.X == X && obj.Y == Y);

            switch (TypeOfAnimal)
            {
                case AnimalType.Fish:
                    return Map[Y, X].TypeOfCell == WorldCell.CellType.Water && !colilision;
                case AnimalType.Turtle:
                    return !colilision;
                case AnimalType.Rabbit:
                    return Map[Y, X].TypeOfCell == WorldCell.CellType.Ground && !colilision;
                default:
                    return true;
            }
        }

        public override string ToString()
        {
            return TypeOfAnimal.ToString();
        }
    }

    /// <summary>
    /// Класс неподвижного объекта. Наследуется от GameObject
    /// </summary>
    class SolidObject : GameObject
    {
        public SolidObject(Game game) : base(game)
        {
            TypeOfSolid = (SolidObjectType)Game.randomSingletone.Next(Enum.GetNames(typeof(SolidObjectType)).Length);

            StartPositionSet();
        }

        public SolidObjectType TypeOfSolid { get; private set; }

        protected override bool CanLocationAt()
        {
            switch (TypeOfSolid)
            {
                case SolidObjectType.Stone:
                    return true;
                case SolidObjectType.Tree:
                    return GameInstance.Map[Y, X].TypeOfCell == WorldCell.CellType.Ground;
                default:
                    return false;
            }
        }

        public enum SolidObjectType
        {
            Stone,
            Tree
        }

        public override string ToString()
        {
            return TypeOfSolid.ToString();
        }
    }
}
