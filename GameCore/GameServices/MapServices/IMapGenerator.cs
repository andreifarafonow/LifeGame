using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GameCore.GameServices.MapServices
{
    public interface IMapGenerator
    {
        IMap Generate(Size size);
    }
}
