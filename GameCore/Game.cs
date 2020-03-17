using System;

namespace GameCore
{
    public class Game
    {
        /// <param name="N">Высота игрового поля</param>
        /// <param name="M">Ширина игрового поля</param>
        public Game(int n, int m)
        {
            N = n;
            M = m;
        }

        /// <summary>
        /// Запускает игру
        /// </summary>
        public void Start()
        {
            
        }

        /// <summary>
        /// Высота игрового поля
        /// </summary>
        public int N { get; private set; }

        /// <summary>
        /// Ширина игрового поля
        /// </summary>
        public int M { get; private set; }
    }

    public abstract class GameObject
    {
        
    }

    
}
