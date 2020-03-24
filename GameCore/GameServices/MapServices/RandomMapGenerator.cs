using GameCore.GameEntities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GameCore.GameServices.MapServices
{
    public class RandomMapGenerator : IMapGenerator
    {
        public RandomMapGenerator(IMap map, Random random)
        {
            Map = map;
            Random = random;
        }

        public IMap Map { get; }
        public Random Random { get; }

        public IMap Generate(Size size)
        {
            Map.Initialize(size);

            for (int i = 0; i < Map.Size.Height; i++)
            {
                for (int j = 0; j < Map.Size.Width; j++)
                {
                    var type = (WorldCell.CellType)Random.Next(0, Enum.GetNames(typeof(WorldCell.CellType)).Length);
                    Map[i, j] = new WorldCell(new Point(j, i), type);
                }
            }

            return Map;
        }
    }
}
