using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using Infotecs.Attika.AttikaConsoleHost.Configurations.ApplicationRoots;
using Infotecs.Attika.AttikaConsoleHost.InstanceProviders;

namespace Infotecs.Attika.AttikaConsoleHost.Extensions.Attributes
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
                                                                     NinjectServiceLocator.Kernel);

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