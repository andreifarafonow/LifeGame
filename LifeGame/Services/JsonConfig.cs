using Microsoft.Extensions.Configuration;
using System;
using System.Drawing;
using System.IO;

namespace LifeGame.Services
{
    public class JsonConfig : IConfigService
    {
        public Size GameSize { get; }

        public int ObjectCount { get; }

        public int Fps { get; }

        public JsonConfig()
        {
            IConfigurationRoot configuration = BuildConfiguration();

            GameSize = new Size(int.Parse(configuration.GetSection("gameWidth").Value), int.Parse(configuration.GetSection("gameHeight").Value));
            ObjectCount = int.Parse(configuration.GetSection("objectsNumber").Value);
            Fps = int.Parse(configuration.GetSection("fps").Value);
        }

        static IConfigurationRoot BuildConfiguration()
        {
            return new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                   .Build();
        }
    }
}
