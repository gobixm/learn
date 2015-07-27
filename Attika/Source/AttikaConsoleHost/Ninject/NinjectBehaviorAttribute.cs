using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using Ninject;

namespace Infotecs.Attika.AttikaConsoleHost.Ninject
{
    public class NinjectBehaviorAttribute : Attribute, IServiceBehavior
    {
        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase,
                                         Collection<ServiceEndpoint> endpoints,
                                         BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            Type serviceType = serviceDescription.ServiceType;
            IInstanceProvider provider = new NinjectInstanceProvider(serviceType,
                                                                     new StandardKernel(new DefaultModule()));
            foreach (ChannelDispatcherBase channelDispatcherBase in serviceHostBase.ChannelDispatchers)
            {
                var channelDispatcher = (ChannelDispatcher) channelDispatcherBase;
                foreach (EndpointDispatcher endpointDispatcher in channelDispatcher.Endpoints)
                {
                    DispatchRuntime dispatchRuntime = endpointDispatcher.DispatchRuntime;
                    dispatchRuntime.InstanceProvider = provider;
                }
            }
        }
    }
}