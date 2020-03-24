using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GameCore.GameEntities
{
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

        bool CollisionIn(Point point)
        {
            return GameInstance.GameObjects.OfType<SolidObject>().Any(obj => obj.Position == point);
        }

        bool CheckMove(int fromX, int fromY, MovingDirection dir, int stepLength, MovingType movingType)
        {
            for (int i = 1; i <= stepLength; i++)
            {
                int currentX = fromX, currentY = fromY;

                switch (dir)
                {
                    case MovingDirection.Right:
                        currentX = fromX + i;
                        break;
                    case MovingDirection.Down:
                        currentY = currentY + i;
                        break;
                    case MovingDirection.Left:
                        currentX = fromX - i;
                        break;
                    case MovingDirection.Up:
                        currentY = fromY - i;
                        break;
                }

                // Выход за пределы карты
                if (currentX < 0 || currentY < 0 || currentX >= GameInstance.Width || currentY >= GameInstance.Height)
                    return false;

                bool collision = CollisionIn(new Point(currentX, currentY));

                if (movingType == MovingType.Swim)
                {
                    if (Map[currentY, currentX].TypeOfCell == WorldCell.CellType.Ground)
                        return false;
                    if (collision)
                        return false;
                }
                else if (movingType == MovingType.Go)
                {
                    if (Map[currentY, currentX].TypeOfCell == WorldCell.CellType.Water)
                        return false;
                    if (collision)
                        return false;
                }
            }

            return true;
        }

        protected override bool CanLocationAt()
        {
            bool collision = CollisionIn(Position);

            var inCell = GameInstance.GameObjects.Where(obj => obj.Position == Position);

            switch (TypeOfAnimal)
            {
                case AnimalType.Fish:
                    return Map[Position.Y, Position.X].TypeOfCell == WorldCell.CellType.Water && !collision;
                case AnimalType.Turtle:
                    return !collision;
                case AnimalType.Rabbit:
                    return Map[Position.Y, Position.X].TypeOfCell == WorldCell.CellType.Ground && !collision;
                default:
                    return true;
            }
        }

        Dictionary<AnimalType, string> animalNames = new Dictionary<AnimalType, string>() 
        {
            { AnimalType.Duck, "Утка" },
            { AnimalType.Fish, "Рыба" },
            { AnimalType.Rabbit, "Кролик" },
            { AnimalType.Sparrow, "Воробей" },
            { AnimalType.Turtle, "Черепаха" }
        };

        public override string ToString()
        {
            return animalNames[TypeOfAnimal];
        }
    }
}
