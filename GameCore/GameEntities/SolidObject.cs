using System;

namespace GameCore.GameEntities
{
    /// <summary>
    /// Класс неподвижного объекта. Наследуется от GameObject
    /// </summary>
    class SolidObject : GameObject
    {
        public SolidObject(Game game) : base(game)
        {
            TypeOfSolid = (SolidObjectType)Game.randomSingletone.Next(Enum.GetNames(typeof(SolidObjectType)).Length);

            StartPositionSet();
        }

        public SolidObjectType TypeOfSolid { get; private set; }

        protected override bool CanLocationAt()
        {
            switch (TypeOfSolid)
            {
                case SolidObjectType.Stone:
                    return true;
                case SolidObjectType.Tree:
                    return GameInstance.Map[Y, X].TypeOfCell == WorldCell.CellType.Ground;
                default:
                    return false;
            }
        }

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
    }
}
