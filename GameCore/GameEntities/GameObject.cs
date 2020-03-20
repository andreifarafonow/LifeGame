using System;
using System.Linq;

namespace GameCore.GameEntities
{
    public abstract class GameObject
    {
        static int LastId { get; set; }


        /// <summary>
        /// Конструирует новый игровой объект
        /// </summary>
        /// <param name="game">Объект игры</param>
        protected GameObject(Game game)
        {
            GameInstance = game;
            Map = game.Map;

            Id = ++LastId;
        }

        /// <summary>
        /// Начальное позиционирование объекта на карте в случайной, свойственной для него позиции
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

            Speed = SpeedOfType(TypeOfAnimal);

            StartPositionSet();
        }

        private static int SpeedOfType(AnimalType type)
        {
            switch (type)
            {
                case AnimalType.Fish:
                    return 1;
                case AnimalType.Duck:
                    return 2;
                case AnimalType.Rabbit:
                    return 3;
                case AnimalType.Turtle:
                    return 1;
                case AnimalType.Sparrow:
                    return 4;
                default:
                    return 0;
            }
        }

        public AnimalType TypeOfAnimal { get; private set; }

        int Speed { get; }

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
        /// Типы перемещений
        /// </summary>
        public enum MovingType
        {
            Swim,
            Fly,
            Go
        }

        /// <summary>
        /// Направления перемещений
        /// </summary>
        public enum MovingDirection
        {
            Right,
            Down,
            Left,
            Up
        }

        MovingType RandomPossibleMovingType()
        {
            MovingType[] possibleMovings = null;

            switch (TypeOfAnimal)
            {
                case AnimalType.Fish:
                    possibleMovings = new MovingType[]{ MovingType.Swim };
                    break;
                case AnimalType.Duck:
                    possibleMovings = new MovingType[] { MovingType.Swim, MovingType.Fly, MovingType.Go };
                    break;
                case AnimalType.Sparrow:
                    possibleMovings = new MovingType[] { MovingType.Fly, MovingType.Go };
                    break;
                case AnimalType.Turtle:
                    possibleMovings = new MovingType[] { MovingType.Swim, MovingType.Go };
                    break;
                case AnimalType.Rabbit:
                    possibleMovings = new MovingType[] { MovingType.Go };
                    break;
            }

            return possibleMovings[Game.randomSingletone.Next(possibleMovings.Length)];
        }

        bool CheckMoveTo(int x, int y, MovingType movingType)
        {
            throw new NotImplementedException();
            // Выход за границы карты
            if (x < 0 || y < 0 || x >= GameInstance.Width || y >= GameInstance.Height)
                return false;
            if (movingType == MovingType.Swim)
            {
                if (GameInstance.Map[y, x].TypeOfCell == WorldCell.CellType.Ground)
                    return false;
            }
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
            switch (TypeOfAnimal)
            {
                case AnimalType.Duck:
                    return "Утка";
                case AnimalType.Fish:
                    return "Рыба";
                case AnimalType.Rabbit:
                    return "Кролик";
                case AnimalType.Sparrow:
                    return "Воробей";
                case AnimalType.Turtle:
                    return "Черепаха";
                default:
                    return "---";
            }
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
            switch (TypeOfSolid)
            {
                case SolidObjectType.Stone:
                    return "Камень";
                case SolidObjectType.Tree:
                    return "Дерево";
                default:
                    return "---";
            }
        }
    }
}
