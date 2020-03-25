using GameCore.GameEntities;
using GameCore.GameServices.MapServices;
using Ninject;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace GameCore.GameServices.ObjectsServices
{
    public class RandomSettlement : ISettlement
    {
        public RandomSettlement(IMap map, Random random)
        {
            Map = map;
            Random = random;
        }

        public IMap Map { get; }
        public Random Random { get; }

        public void Populate(int objectsNum, List<GameObject> objects)
        {
            int solidCount = Random.Next(objectsNum);

            for (int i = 0; i < objectsNum; i++)
            {
                GameObject created;

                if (i < solidCount)
                    created = new SolidObject(Random);
                else
                    created = new Animal(Random);

                int x, y;

                do
                {
                    x = Random.Next(Map.Size.Width);
                    y = Random.Next(Map.Size.Height);

                    created.Position = new Point(x, y);
                }
                while (!created.СanBeLocatedAt(Map[y, x], objects.Where(obj => obj.Position == created.Position)));

                objects.Add(created);
            }
        }
    }
}
