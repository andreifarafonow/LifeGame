using System;

namespace GameCore.GameInstances
{
    public abstract class GameObject
    {
        /// <summary>
        /// Конструирует новый игровой объект в случайной, свойственной для него позиции
        /// </summary>
        /// <param name="game">Объект игры</param>
        public GameObject(Game game)
        {
            // Позиционируем элемент на карте
            do
            {
                X = Game.randomSingletone.Next(game.Width);
                Y = Game.randomSingletone.Next(game.Height);
            }
            while (!CanLocationAt());

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
        public Animal(Game game) : base(game)
        {
            TypeOfAnimal = (AnimalType)Game.randomSingletone.Next(Enum.GetNames(typeof(AnimalType)).Length); ;
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
            throw new NotImplementedException();
            /*bool CollisionWithSolid() => GameInstance.GameObjects
                .Where(obj => obj.X == X && obj.Y == Y)
                .Any(obj => obj is SolidObject);

            switch (TypeOfAnimal)
            {
                case AnimalType.Fish:
                    return Map[Y, X].TypeOfCell == WorldCell.CellType.Water && !CollisionWithSolid();

            }*/
        }
    }

    class SolidObject : GameObject
    {
        public SolidObject(Game game) : base(game)
        {
            TypeOfSolid = (SolidObjectType)Game.randomSingletone.Next(Enum.GetNames(typeof(SolidObjectType)).Length); ;
        }

        public SolidObjectType TypeOfSolid { get; }

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
