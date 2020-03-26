using System;
using System.Collections.Generic;
using System.Linq;

namespace GameCore.GameEntities
{
    /// <summary>
    /// Класс животного. Наследуется от GameObject
    /// </summary>
    public class Animal : GameObject
    {
        public Animal(Random random) : base (random)
        {
            TypeOfAnimal = (AnimalType)random.Next(Enum.GetNames(typeof(AnimalType)).Length);
        }

        static Dictionary<AnimalType, (int speed, MovingType[] possibleMovings, string name)> animalTypeData = new Dictionary<AnimalType, (int speed, MovingType[] possibleMovings, string name)>()
        {
            {
                AnimalType.Duck,
                (
                    2,
                    new MovingType[]
                    {
                        MovingType.Swim, MovingType.Fly, MovingType.Go
                    },
                    "Утка"
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
                    "Рыба"
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
                    "Кролик"
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
                    "Воробей"
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
                    "Черепаха"
                )
            }
        };

        static Dictionary<MovingType, СanMoveTo> movingСonditions = new Dictionary<MovingType, СanMoveTo>()
        {
            {
                MovingType.Fly,
                (target, collision) => target != null
            },

            {
                MovingType.Go,
                (target, collision) => target != null && target.TypeOfCell == WorldCell.CellType.Ground && !collision
            },

            {
                MovingType.Swim,
                (target, collision) => target != null && target.TypeOfCell == WorldCell.CellType.Water && !collision
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
        

        public MovingType RandomPossibleMovingType()
        {
            MovingType[] possibleMovings = animalTypeData[TypeOfAnimal].possibleMovings;

            return possibleMovings[Random.Next(possibleMovings.Length)];
        }
        public override bool СanBeLocatedAt(WorldCell cell, IEnumerable<GameObject> neighbors)
        {
            bool collision = neighbors.Any(obj => obj is SolidObject);

            return animalTypeData[TypeOfAnimal].possibleMovings.Any(x => movingСonditions[x](cell, collision));
        }

        public bool CanMoveTo(WorldCell cell, IEnumerable<GameObject> neighbors, MovingType movingType)
        {
            bool collision = neighbors.Any(obj => obj is SolidObject);

            return movingСonditions[movingType](cell, collision);
        }

        public override string ToString()
        {
            return animalTypeData[TypeOfAnimal].name;
        }
    }
}
