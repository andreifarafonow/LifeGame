﻿using System;
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

        bool CollisionIn(int x, int y)
        {
            return GameInstance.GameObjects.Any(obj => obj.X == x && obj.Y == y && obj is SolidObject);
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

                bool collision = CollisionIn(currentX, currentY);

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
            bool collision = CollisionIn(X, Y);

            var inCell = GameInstance.GameObjects.Where(obj => obj.X == X && obj.Y == Y);

            switch (TypeOfAnimal)
            {
                case AnimalType.Fish:
                    return Map[Y, X].TypeOfCell == WorldCell.CellType.Water && !collision;
                case AnimalType.Turtle:
                    return !collision;
                case AnimalType.Rabbit:
                    return Map[Y, X].TypeOfCell == WorldCell.CellType.Ground && !collision;
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
}
