﻿/*
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
using Infotecs.Attika.AttikaGui.Ninject;
using Ninject;

namespace Infotecs.Attika.AttikaGui.ViewModel
{
    /// <summary>
    ///     This class contains static references to all the view models in the
    ///     application and provides an entry point for the bindings.
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public class ViewModelLocator
    {
        private static readonly IKernel Kernel;

        static ViewModelLocator()
        {
            if (ViewModelBase.IsInDesignModeStatic)
                Kernel = new StandardKernel(new DesignTimeModule());
            else
                Kernel = new StandardKernel(new RunTimeModule()); 
        }

        /// <summary>
        ///     Gets the Main property.
        /// </summary>
        [SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main
        {
            get { return Kernel.Get<MainViewModel>(); }
        }

        /// <summary>
        ///     Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
        }
    }
}