using System;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Infotecs.Attika.AttikaGui.DTO;
using Infotecs.Attika.AttikaGui.Model;

namespace Infotecs.Attika.AttikaGui.ViewModel
{
    /// <summary>
    ///     This class contains properties that a View can data bind to.
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public class NavigationViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;

        public NavigationViewModel(IDataService dataService)
        {
            _dataService = dataService;
            _articleHeaders = new ObservableCollection<ArticleHeaderViewModel>();
            foreach (var header in _dataService.GetArticleHeaders())
            {
                _articleHeaders.Add(new ArticleHeaderViewModel(header));
            }
            RaisePropertyChanged("ArticleHeaders");
        }

        private RelayCommand _addArticleCommand;
        public RelayCommand AddArticleCommand
        {
            get { return _addArticleCommand ?? (_addArticleCommand = new RelayCommand(AddArticle)); }
        }

        public ObservableCollection<ArticleHeaderViewModel> ArticleHeaders
        {
            get { return _articleHeaders; }
            set
            {
                _articleHeaders = value;
                RaisePropertyChanged();
            }
        }

        private void AddArticle()
        {
            _dataService.NewArticle(new ArticleDto(){Id = Guid.NewGuid(), Text = "random text", Title = "Random Title", Created = DateTime.Now});
        }

        private ObservableCollection<ArticleHeaderViewModel> _articleHeaders;

    }
}