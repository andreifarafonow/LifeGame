using GameCore.GameEntities;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GameCore.GameServices.ObjectsServices
{
    class ListGameObjectsContainer : IGameObjectsContainer
    {
        List<GameObject> gameObjects = new List<GameObject>();

        public void Add(GameObject gameObject)
        {
            gameObjects.Add(gameObject);
        }

        public IEnumerable<GameObject> GetAllObjects()
        {
            return gameObjects;
        }

        public IEnumerable<GameObject> GetObjectsInPosition(Point position)
        {
            return gameObjects.Where(obj => obj.Position == position);
        }

        public void Remove(GameObject gameObject)
        {
            gameObjects.Remove(gameObject);
        }
    }
}
