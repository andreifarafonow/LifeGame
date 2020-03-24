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

            IMap map = new MatrixMap();
            IMapGenerator mapGenerator = new RandomMapGenerator(map, Game.randomSingletone);

            ISettlement settlement = new RandomSettlement(map, Game.randomSingletone);

            var game = new Game(mapGenerator, settlement);

            game.Initialize(LoadGameSizeFromConfig());

            game.Start();

            GameDisplayer.Display(game);
        }

        static IConfigurationRoot BuildConfiguration()
        {
            return new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                   .Build();
        }

        static void MaximizeConsole()
        {
            Console.WindowHeight = Console.LargestWindowHeight;
            Console.WindowWidth = Console.LargestWindowWidth;
        }

        static Size LoadGameSizeFromConfig()
        {
            return new Size(int.Parse(configuration.GetSection("gameWidth").Value), int.Parse(configuration.GetSection("gameHeight").Value));
        }
    }
}
