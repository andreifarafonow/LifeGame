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

        bool CanMoveTo(WorldCell target, IEnumerable<GameObject> objectsOnTargetCell, MovingType movingType)
        {
            // Выход за пределы карты
            if (target == null)
                return false;

            bool collision = objectsOnTargetCell.Any(obj => obj is SolidObject);

            if (movingType == MovingType.Swim)
            {
                return target.TypeOfCell == WorldCell.CellType.Water && !collision;
            }
            else if (movingType == MovingType.Go)
            {
                return target.TypeOfCell == WorldCell.CellType.Ground && !collision;
            }

            return true;
        }

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
