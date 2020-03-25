using System;
using System.Drawing;
using System.IO;
using GameCore;
using GameCore.GameServices.MapServices;
using GameCore.GameServices.ObjectsServices;
using Microsoft.Extensions.Configuration;

namespace LifeGame
{
    class Program
    {
        static IConfigurationRoot configuration;

        static void Main(string[] args)
        {
            configuration = BuildConfiguration();

            MaximizeConsole();

            var game = new Game(LoadGameSizeFromConfig(),
                                LoadObjectsNumFromConfig());

            game.Start();

            GameDisplayer.Display(game);
        }

        static void MaximizeConsole()
        {
            Console.WindowHeight = Console.LargestWindowHeight;
            Console.WindowWidth = Console.LargestWindowWidth;
        }

        static IConfigurationRoot BuildConfiguration()
        {
            return new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                   .Build();
        }

        static Size LoadGameSizeFromConfig()
        {
            return new Size(int.Parse(configuration.GetSection("gameWidth").Value), int.Parse(configuration.GetSection("gameHeight").Value));
        }

        static int LoadObjectsNumFromConfig()
        {
            return int.Parse(configuration.GetSection("objectsNumber").Value);
        }
    }
}
