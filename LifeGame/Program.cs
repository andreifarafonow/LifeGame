using System;
using System.Drawing;
using System.IO;
using System.Threading;
using GameCore;
using LifeGame.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LifeGame
{
    class Program
    {
        static void Main()
        {
            var serviceProvider = new ServiceCollection()
            .AddSingleton<IConfigService, JsonConfig>()
            .AddSingleton<ConsoleGamePresentation>()
            .BuildServiceProvider();

            var config = serviceProvider.GetService<IConfigService>();
            var consoleGamePresentation = serviceProvider.GetService<ConsoleGamePresentation>();

            ConsoleHelper.MaximizeConsole();

            var game = new Game(config.GameSize, config.ObjectCount);

            game.Start();

            while (true)
            {
                consoleGamePresentation.Display(game);
                Thread.Sleep(1000 / config.Fps);
                game.Step();
            }
        }
        
    }
}
