using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Infotecs.Attika.AttikaGui.DTO;
using Infotecs.Attika.AttikaGui.GuiMessages;
using Infotecs.Attika.AttikaGui.Model;

namespace Infotecs.Attika.AttikaGui.ViewModel
{
    /// <summary>
    ///     This class contains properties that a View can data bind to.
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public sealed class NavigationViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;

        private RelayCommand _addArticleCommand;
        private ObservableCollection<ArticleHeaderViewModel> _articleHeaders;

        public NavigationViewModel(IDataService dataService)
        {
            _dataService = dataService;
            SubscribeToMessages();
            RebuildHeaderList();
        }

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

        private void RebuildHeaderList()
        {
            ArticleHeaders = new ObservableCollection<ArticleHeaderViewModel>(
                from a in _dataService.GetArticleHeaders() select new ArticleHeaderViewModel(a));
        }

        private void SubscribeToMessages()
        {
            Messenger.Default.Register(this, (RefreshHeaderListMessage message) => RebuildHeaderList());
        }

        private void AddArticle()
        {
            Messenger.Default.Send(
                new AddArticleMessage
                    {
                        Article = new ArticleDto
                            {
                                Description = "",
                                Text = "",
                                Title = "Новая статья"
                            }
                    });
        }
    }
}