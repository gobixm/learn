using Infotecs.Attika.AttikaGui.Model;
using Infotecs.Attika.AttikaGui.ViewModel;
using Ninject.Modules;

namespace Infotecs.Attika.AttikaGui.Ninject
{
    public class DesignTimeModule :NinjectModule
    {
        public override void Load()
        {
            Bind<IDataService>().To<DataService>();
            Bind<MainViewModel>().ToSelf();
            Bind<NavigationViewModel>().ToSelf();
            Bind<ArticleViewModel>().ToSelf();
        }
    }
}