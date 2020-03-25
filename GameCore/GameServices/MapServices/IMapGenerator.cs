using System.Drawing;

namespace GameCore.GameServices.MapServices
{
    public interface IMapGenerator
    {
        /// <summary>
        /// Генерирует карту заданных размеров
        /// </summary>
        /// <param name="size">Размер карты</param>
        /// <returns></returns>
        IMap Generate(Size size);
    }
}
