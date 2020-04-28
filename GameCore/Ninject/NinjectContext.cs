using Ninject;
using Ninject.Modules;

//review: А зачем ты ис
public static class NinjectContext
{
	public static IKernel Kernel { get; private set; }

	public static void SetUp(params INinjectModule[] modules)
	{
		Kernel = new StandardKernel(modules);
	}
}