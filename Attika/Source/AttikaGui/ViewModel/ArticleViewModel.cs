using GalaSoft.MvvmLight;
using Infotecs.Attika.AttikaGui.Model;

namespace Infotecs.Attika.AttikaGui.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ArticleViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;

        public ArticleViewModel(IDataService dataService)
        {
            _dataService = dataService;
        }
    }
}