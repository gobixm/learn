using System;
using System.Linq;
using Infotecs.Attika.AttikaGui.DataServices;
using Infotecs.Attika.AttikaGui.ViewModels;
using Ninject.Modules;

namespace Infotecs.Attika.AttikaGui.NinjectModules
{
    public sealed class RunTimeModule : NinjectModule
    {
        public override void Load()
        {
            string[] args = Environment.GetCommandLineArgs();
            if ((from a in args where a == "-classic" select a).Any())
            {
                Bind<IDataService>().To<DataService>();
            }
            else
            {
                Bind<IDataService>().To<MessagedDataService>();
            }
            Bind<MainViewModel>().ToSelf();
            Bind<NavigationViewModel>().ToSelf();
            Bind<ArticleViewModel>().ToSelf();
            Bind<IDataSerializer>().To<WcfJsonSerializer>();
        }
    }
}