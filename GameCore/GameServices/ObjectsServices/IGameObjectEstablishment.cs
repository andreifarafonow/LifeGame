using System;
using System.Collections.Generic;
using System.Text;

namespace GameCore.GameServices.ObjectsServices
{
    /// <summary>
    /// Служит для операций создания и перемещения объектов
    /// </summary>
    interface IGameObjectEstablishment
    {
        void Populate(int objectsNum);
    }
}
