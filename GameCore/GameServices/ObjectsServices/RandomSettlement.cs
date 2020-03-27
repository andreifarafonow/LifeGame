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
        public RandomSettlement(IMap map, IGameObjectsContainer objectsContainer, Random random)
        {
            Map = map;
            ObjectsContainer = objectsContainer;
            Random = random;
        }

        IMap Map { get; }
        IGameObjectsContainer ObjectsContainer { get; }
        Random Random { get; }

        public void Populate(int objectsNum)
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
                while (!created.СanBeLocatedAt(Map[y, x], ObjectsContainer.GetObjectsInPosition(created.Position)));

                ObjectsContainer.Add(created);
            }
        }
    }
}
