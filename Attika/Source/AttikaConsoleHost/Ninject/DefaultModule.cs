using Infotecs.Attika.AtticaDataModel.Repositories;
using Ninject.Modules;

namespace Infotecs.Attika.AttikaConsoleHost.Ninject
{
    public sealed class DefaultModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICommandRepository>().To<StandardCommandRepository>();
            Bind<IQueryRepository>().To<StandardQueryRepository>();
        }
    }
}