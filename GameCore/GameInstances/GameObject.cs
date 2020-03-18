using System;
using System.Collections.Generic;
using System.Text;

namespace GameCore.GameInstances
{
    abstract class GameObject
    {
        public GameObject(int xPos, int yPos)
        {
            X = xPos;
            Y = yPos;
        }

        /// <summary>
        /// Номер столбца карты в котором находится объект
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Номер строки карты в котором находится объект
        /// </summary>
        public int Y { get; set; }
    }

    class Animal : GameObject
    {
        private Animal(int xPos, int yPos, AnimalType type) : base(xPos, yPos)
        {

        }

        enum AnimalType
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

        public static Animal GenerateRandomAnimal()
        {
            throw new NotImplementedException();
        }
    }
}
