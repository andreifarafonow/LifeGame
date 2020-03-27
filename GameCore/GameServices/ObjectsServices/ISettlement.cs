using GameCore.GameEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameCore.GameServices.ObjectsServices
{
    public interface ISettlement
    {
        /// <summary>
        /// Населяет мир указанным числом объектов
        /// </summary>
        /// <param name="objectsNum">Число объектов для заселения</param>
        /// <param name="objects">Список объектов игры, в который будут добавляться новые</param>
        void Populate(int objectsNum);
    }
}
