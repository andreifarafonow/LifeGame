using System;
using System.Collections.Generic;
using System.Linq;

namespace GameCore.GameEntities
{
    /// <summary>
    /// Класс животного. Наследуется от GameObject
    /// </summary>
    class Animal : GameObject
    {
        public Animal(Random random) : base (random)
        {
            TypeOfAnimal = (AnimalType)random.Next(Enum.GetNames(typeof(AnimalType)).Length);
        }

        static Dictionary<AnimalType, (int speed, MovingType[] possibleMovings, string name, СanBeLocatedDelegate placementСondition)> animalTypeData = new Dictionary<AnimalType, (int speed, MovingType[] possibleMovings, string name, СanBeLocatedDelegate placementСondition)>()
        {
            {
                AnimalType.Duck,
                (
                    2,
                    new MovingType[]
                    {
                        MovingType.Swim, MovingType.Fly, MovingType.Go
                    },
                    "Утка",
                    (cell, collision) => true
                ) 
            },

            {
                AnimalType.Fish,
                (
                    1,
                    new MovingType[]
                    {
                        MovingType.Swim 
                    },
                    "Рыба",
                    (cell, collision) => cell.TypeOfCell == WorldCell.CellType.Water && !collision
                )
            },

            {
                AnimalType.Rabbit,
                (
                    3,
                    new MovingType[]
                    {
                        MovingType.Go
                    },
                    "Кролик",
                    (cell, collision) => cell.TypeOfCell == WorldCell.CellType.Ground && !collision
                )
            },

            {
                AnimalType.Sparrow,
                (
                    4,
                    new MovingType[]
                    {
                        MovingType.Fly, MovingType.Go
                    },
                    "Воробей",
                    (cell, collision) => true
                )
            },

            {
                AnimalType.Turtle,
                (
                    1,
                    new MovingType[]
                    {
                        MovingType.Swim, MovingType.Go
                    },
                    "Черепаха",
                    (cell, collision) => !collision
                )
            }
        };

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
            MovingType[] possibleMovings = animalTypeData[TypeOfAnimal].possibleMovings;

            return possibleMovings[Random.Next(possibleMovings.Length)];
        }

        /*bool CheckMove(int fromX, int fromY, MovingDirection dir, int stepLength, MovingType movingType)
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
                if (currentX < 0 || currentY < 0 || currentX >= GameInstance.Map.Size.Width || currentY >= GameInstance.Map.Size.Height)
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
        }*/

        public override bool СanBeLocatedAt(WorldCell cell, IEnumerable<GameObject> neighbors)
        {
            bool collision = neighbors.Any(obj => obj is SolidObject);

            return animalTypeData[TypeOfAnimal].placementСondition(cell, collision);
        }
        public override string ToString()
        {
            return animalTypeData[TypeOfAnimal].name;
        }
    }
}
