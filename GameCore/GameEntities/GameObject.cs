using System.Drawing;

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
            int x, y;

            do
            {
                x = Game.randomSingletone.Next(GameInstance.MapSize.Width);
                y = Game.randomSingletone.Next(GameInstance.MapSize.Height);

                Position = new Point(x, y);
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
        /// Позиция объекта
        /// </summary>
        public Point Position { get; private set; }

        /// <summary>
        /// Экземпляр игры, в которой существует игровой объект
        /// </summary>
        protected Game GameInstance { get; }

        /// <summary>
        /// Карта игры
        /// </summary>
        protected WorldCell[,] Map { get; }
    }
}
