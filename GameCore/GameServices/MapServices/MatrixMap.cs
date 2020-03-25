using GameCore.GameEntities;
using System.Drawing;

namespace GameCore.GameServices.MapServices
{
    public class MatrixMap : IMap
    {
        WorldCell[,] map;

        public WorldCell this[int y, int x] 
        {
            get => map[y, x];
            set => map[y, x] = value; 
        }

        public Size Size { get; private set; }

        public void Initialize(Size size)
        {
            map = new WorldCell[size.Height, size.Width];

            Size = size;
        }
    }
}
