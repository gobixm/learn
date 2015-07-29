/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:AttikaGui.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using System.Diagnostics.CodeAnalysis;
using GalaSoft.MvvmLight;
using Infotecs.Attika.AttikaGui.NinjectModules;
using Ninject;

namespace Infotecs.Attika.AttikaGui.ViewModel
{
    public sealed class ViewModelLocator
    {
        private static readonly IKernel Kernel;

        static ViewModelLocator()
        {
            if (ViewModelBase.IsInDesignModeStatic)
                Kernel = new StandardKernel(new DesignTimeModule());
            else
                Kernel = new StandardKernel(new RunTimeModule());
        }

        [SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main
        {
            get { return Kernel.Get<MainViewModel>(); }
        }

        public static void Cleanup()
        {
        }
    }
}