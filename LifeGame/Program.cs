using System;
using System.Drawing;
using System.IO;
using System.Threading;
using GameCore;
using Microsoft.Extensions.Configuration;

namespace LifeGame
{
    class Program
    {
        static IConfigurationRoot configuration;

        static void Main()
        {
            configuration = BuildConfiguration();

            MaximizeConsole();

            var game = new Game(LoadGameSizeFromConfig(),
                                LoadObjectsNumFromConfig());

            game.Start();

            while (true)
            {
                GameDisplayer.Display(game);
                Thread.Sleep(1000 / LoadFpsFromConfig());
                game.Step();
            }
        }

        //review: Лучше сделать ConsoleHelper и вынести это туда. Боремся за выполнение SRP.
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

        //review: Методы, которые читают из конфига, надо вынести в отдельный класс ConfigService (который поместить в DI)
        //Назвать их тогда можно GetGameSize, GetObjectCount и т.д. 
        //Или лучше сделать их read-only свойствами
        static Size LoadGameSizeFromConfig()
        {
            return new Size(int.Parse(configuration.GetSection("gameWidth").Value), int.Parse(configuration.GetSection("gameHeight").Value));
        }

        //review: Когда речь о ко-ве чего-то, правильнее называть идентификатор ObjectCount.
        //Да, Object - в единственном числе. Число объектОВ - это ObjectCount, как ни странно )
        static int LoadObjectsNumFromConfig()
        {
            return int.Parse(configuration.GetSection("objectsNumber").Value);
        }

        //review: эта функция дергается на каждом цикле, лучше в ней сделать lazy initialization, когда перенесешь  ее  в ConfigService.
        static int LoadFpsFromConfig()
        {
            return int.Parse(configuration.GetSection("fps").Value);
        }
    }
}
