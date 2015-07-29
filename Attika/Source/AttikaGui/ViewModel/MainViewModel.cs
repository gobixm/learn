using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Infotecs.Attika.AttikaGui.DataServices;
using Infotecs.Attika.AttikaGui.DataTransferObjects;
using Infotecs.Attika.AttikaGui.GuiMessages;

namespace Infotecs.Attika.AttikaGui.ViewModel
{
    /// <summary>
    ///     This class contains properties that the main View can data bind to.
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public sealed class MainViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        private ArticleViewModel _articleViewModel;
        private NavigationViewModel _navigationViewModel;
        private string _state;

        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;
            SubscribeToGuiMessages();
        }

        public NavigationViewModel NavigationViewModel
        {
            get { return _navigationViewModel ?? (_navigationViewModel = new NavigationViewModel(_dataService)); }
        }

        public ArticleViewModel ArticleViewModel
        {
            get { return _articleViewModel ?? (_articleViewModel = new ArticleViewModel(_dataService)); }
        }

        public string State
        {
            get { return _state; }
            set
            {
                _state = value;
                RaisePropertyChanged();
            }
        }

        private void SubscribeToGuiMessages()
        {
            Messenger.Default.Register(this, (AddArticleMessage message) => OnAddArticle(message));
            Messenger.Default.Register(this, (ViewArticleMessage message) => OnViewArticle(message));
            Messenger.Default.Register(this, (ChangeStateMessage message) => OnChangeState(message));
        }

        private void OnChangeState(ChangeStateMessage message)
        {
            State = message.State;
        }

        private void OnViewArticle(ViewArticleMessage message)
        {
            ArticleDto article = null;
            try
            {
                article = _dataService.GetArticle(message.ArticleId);
            }
            catch (DataServiceException ex)
            {
                State = ex.ToString();
                return;
            }

            ArticleViewModel.ArticleDto = article;
        }

        private void OnAddArticle(AddArticleMessage message)
        {
            ArticleViewModel.ArticleDto = message.Article;
        }
    }
}