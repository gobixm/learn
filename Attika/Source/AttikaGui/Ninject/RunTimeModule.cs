using Infotecs.Attika.AttikaGui.DataServices;
using Infotecs.Attika.AttikaGui.ViewModel;
using Ninject.Modules;

namespace Infotecs.Attika.AttikaGui.Ninject
{
    public sealed class RunTimeModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDataService>().To<DataService>();
            Bind<MainViewModel>().ToSelf();
            Bind<NavigationViewModel>().ToSelf();
            Bind<ArticleViewModel>().ToSelf();
            Bind<IDataSerializer>().To<WcfJsonSerializer>();
        }
    }
}