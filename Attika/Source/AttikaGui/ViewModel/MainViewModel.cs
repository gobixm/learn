using GalaSoft.MvvmLight;
using Infotecs.Attika.AttikaGui.Model;

namespace Infotecs.Attika.AttikaGui.ViewModel
{
    /// <summary>
    ///     This class contains properties that the main View can data bind to.
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        private ArticleViewModel _articleViewModel;
        private NavigationViewModel _navigationViewModel;

        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;
        }

        public NavigationViewModel NavigationViewModel
        {
            get { return _navigationViewModel ?? (_navigationViewModel = new NavigationViewModel(_dataService)); }
        }

        public ArticleViewModel ArticleViewModel
        {
            get { return _articleViewModel ?? (_articleViewModel = new ArticleViewModel(_dataService)); }
        }
    }
}