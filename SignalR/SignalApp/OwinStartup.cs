using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Autofac;
using Microsoft.AspNet.SignalR;
using Autofac.Integration.SignalR;
using System.Reflection;
using System.Web.Http;
using Autofac.Integration.WebApi;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;

[assembly: OwinStartup(typeof(SignalApp.OwinStartup))]

namespace SignalApp
{
    public class OwinStartup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            GlobalConfiguration.Configure(WebApiConfig.Register);
            
            var config = GlobalConfiguration.Configuration;
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterHubs(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(config);
            builder.RegisterHubs(Assembly.GetExecutingAssembly());
            builder.RegisterInstance(new GameService()).As<IGameService>().SingleInstance();
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            GlobalHost.DependencyResolver = new AutofacDependencyResolver(container);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var hubConfig = new HubConfiguration();            
            hubConfig.Resolver = new AutofacDependencyResolver(container);

            app.UseAutofacMiddleware(container);
            //app.MapSignalR("/signalr", config);            
            app.MapSignalR();
        }         
    }
}
