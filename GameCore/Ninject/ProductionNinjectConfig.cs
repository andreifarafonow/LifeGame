using GameCore.GameServices.MapServices;
using GameCore.GameServices.ObjectsServices;
using Ninject.Modules;
using System;

namespace GameCore
{
	public class ProductionNinjectConfig : NinjectModule
	{
		public override void Load()
		{
			Bind<IMap>().To<MatrixMap>().InSingletonScope();
			Bind<IMapGenerator>().To<RandomMapGenerator>();
			Bind<IGameObjectEstablishment>().To<RandomSettlementAndMoving>();
			Bind<Random>().ToConstant(new Random());
			Bind<IGameObjectsContainer>().To<ListGameObjectsContainer>().InSingletonScope();
		}
	}
}
