using Ninject;

namespace Infotecs.Attika.AttikaConsoleHost.Configurations.ApplicationRoots
{
    public static class NinjectServiceLocator
    {
        private static IKernel _kernel;

        public static IKernel Kernel
        {
            get
            {
                if (_kernel == null)
                {
                    _kernel = new StandardKernel(new DefaultConfigurationModule());
                }
                return _kernel;
            }
        }
    }
}