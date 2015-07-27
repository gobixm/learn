using Infotecs.Attika.AtticaDataModel.Repos;
using Ninject.Modules;

namespace Infotecs.Attika.AttikaConsoleHost.Ninject
{
    public sealed class DefaultModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICommandRepo>().To<StandardCommandRepo>();
            Bind<IQueryRepo>().To<StandardQueryRepo>();
        }
    }
}