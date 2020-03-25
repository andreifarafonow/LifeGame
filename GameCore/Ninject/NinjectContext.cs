using Ninject;
using Ninject.Modules;

public static class NinjectContext
{
	public static IKernel Kernel { get; private set; }

	public static void SetUp(params INinjectModule[] modules)
	{
		Kernel = new StandardKernel(modules);
	}
}