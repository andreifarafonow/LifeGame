using System;
using System.IO;
using GameCore;
using Microsoft.Extensions.Configuration;

namespace LifeGame
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                                               .SetBasePath(Directory.GetCurrentDirectory())
                                               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                               .Build();


            Console.WindowHeight = Console.LargestWindowHeight;
            Console.WindowWidth = Console.LargestWindowWidth;

            var game = new Game(int.Parse(configuration.GetSection("gameHeight").Value), int.Parse(configuration.GetSection("gameWidth").Value));

            game.Start();

            GameDisplayer.Display(game);
        }
    }
}
