using System.Drawing;

namespace LifeGame.Services
{
    interface IConfigService
    {
        public Size GameSize { get; }
        public int ObjectCount { get; }
        public int Fps { get; }
    }
}
