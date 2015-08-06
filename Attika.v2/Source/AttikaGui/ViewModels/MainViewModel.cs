using System;
using AttikaContracts.DataTransferObjects;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Infotecs.Attika.AttikaClient;
using Infotecs.Attika.AttikaGui.Messages.Gui;
using DataServiceException = Infotecs.Attika.AttikaGui.DataServices.DataServiceException;

namespace Infotecs.Attika.AttikaGui.ViewModels
{
    public sealed class MainViewModel : ViewModelBase
    {
        private readonly IClientService _clientService;
        private ArticleViewModel _articleViewModel;
        private NavigationViewModel _navigationViewModel;
        private string _state;

        public MainViewModel(IClientService clientService)
        {
            _clientService = clientService;
            SubscribeToGuiMessages();
        }

        public ArticleViewModel ArticleViewModel
        {
            get { return _articleViewModel ?? (_articleViewModel = new ArticleViewModel(_clientService)); }
        }

        public NavigationViewModel NavigationViewModel
        {
            get { return _navigationViewModel ?? (_navigationViewModel = new NavigationViewModel(_clientService)); }
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

        private void OnAddArticle(AddArticleMessage message)
        {
            ArticleViewModel.ArticleDto = message.Article;
        }

        private void OnChangeState(ChangeStateMessage message)
        {
            State = message.State;
        }

        private void OnViewArticle(ViewArticleMessage message)
        {
            ArticleDto article;
            try
            {
                article = _clientService.GetArticle(message.ArticleId);
            }
            catch (DataServiceException ex)
            {
                State = ex.ToString();
                return;
            }

            ArticleViewModel.ArticleDto = article;
        }

        private void SubscribeToGuiMessages()
        {
            Messenger.Default.Register(this, (AddArticleMessage message) => OnAddArticle(message));
            Messenger.Default.Register(this, (ViewArticleMessage message) => OnViewArticle(message));
            Messenger.Default.Register(this, (ChangeStateMessage message) => OnChangeState(message));
        }
    }
}
