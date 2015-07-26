using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using Ninject;

namespace Infotecs.Attika.AttikaConsoleHost.Ninject
{
    public class NinjectInstanceProvider : IInstanceProvider
    {
        private readonly IKernel _kernel;
        private readonly Type _serviceType;

        public NinjectInstanceProvider(Type serviceType, IKernel kernel)
        {
            _serviceType = serviceType;
            _kernel = kernel;
        }

        public object GetInstance(InstanceContext instanceContext)
        {
            return GetInstance(instanceContext, null);
        }

        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            return _kernel.Get(_serviceType);
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
        }
    }
}