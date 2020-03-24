using GameCore.GameEntities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GameCore.GameServices.MapServices
{
    public interface IMap
    {
        public void Initialize(Size size);
        public WorldCell this[int y, int x] { get; set; }
        public Size Size { get; }
    }
}
