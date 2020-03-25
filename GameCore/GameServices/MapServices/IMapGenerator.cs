using System.Drawing;

namespace GameCore.GameServices.MapServices
{
    public interface IMapGenerator
    {
        IMap Generate(Size size);
    }
}
