using System;
using System.Collections.Generic;

namespace GameCore.GameEntities
{
    /// <summary>
    /// Класс неподвижного объекта. Наследуется от GameObject
    /// </summary>
    class SolidObject : GameObject
    {
        public SolidObject()
        {
            TypeOfSolid = (SolidObjectType)Game.randomSingletone.Next(Enum.GetNames(typeof(SolidObjectType)).Length);
        }

        public SolidObjectType TypeOfSolid { get; private set; }

        public enum SolidObjectType
        {
            Stone,
            Tree
        }

        public override string ToString()
        {
            switch (TypeOfSolid)
            {
                case SolidObjectType.Stone:
                    return "Камень";
                case SolidObjectType.Tree:
                    return "Дерево";
                default:
                    return "---";
            }
        }

        public override bool CanLocationAt(WorldCell cell, IEnumerable<GameObject> neighbors)
        {
            switch (TypeOfSolid)
            {
                case SolidObjectType.Stone:
                    return true;
                case SolidObjectType.Tree:
                    return cell.TypeOfCell == WorldCell.CellType.Ground;
                default:
                    throw new Exception();
            }
        }
    }
}
