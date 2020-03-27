using GameCore.GameServices.ObjectsServices;
using System;
using System.Collections.Generic;

namespace GameCore.GameEntities
{
    /// <summary>
    /// Класс неподвижного объекта. Наследуется от GameObject
    /// </summary>
    class SolidObject : GameObject
    {
        public SolidObject(Random random) : base(random)
        {
            TypeOfSolid = (SolidObjectType)Random.Next(Enum.GetNames(typeof(SolidObjectType)).Length);
        }

        public SolidObjectType TypeOfSolid { get; private set; }

        public enum SolidObjectType
        {
            Stone,
            Tree
        }

        static Dictionary<SolidObjectType, (string name, PlacementDelegate placementСondition)> solidTypeData = new Dictionary<SolidObjectType, (string name, PlacementDelegate placementСondition)>()
        {
            { 
                SolidObjectType.Stone, 
                (
                    "Камень",
                    (cell, collision) => true
                )
            },

            {
                SolidObjectType.Tree, 
                (
                    "Дерево",
                    (cell, collision) => cell.TypeOfCell == WorldCell.CellType.Ground
                )
            }
        };

        public override string ToString()
        {
            return solidTypeData[TypeOfSolid].name;
        }

        public override bool СanBeLocatedAt(WorldCell cell, IEnumerable<GameObject> neighbors)
        {
            return solidTypeData[TypeOfSolid].placementСondition(cell, false);
        }
    }
}
