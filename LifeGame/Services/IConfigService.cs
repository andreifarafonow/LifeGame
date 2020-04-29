using System.Drawing;

namespace LifeGame.Services
{
    interface IConfigService
    {
        public Size GameSize { get; }
        public int ObjectsNum { get; }
        public int Fps { get; }
    }
}
