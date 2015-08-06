using System;
using System.Collections.ObjectModel;
using System.Linq;
using AttikaContracts.DataTransferObjects;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Infotecs.Attika.AttikaClient;
using Infotecs.Attika.AttikaGui.Messages.Gui;
using DataServiceException = Infotecs.Attika.AttikaGui.DataServices.DataServiceException;

namespace Infotecs.Attika.AttikaGui.ViewModels
{
    public sealed class NavigationViewModel : ViewModelBase
    {
        private readonly IClientService _clientService;

        private RelayCommand _addArticleCommand;
        private ObservableCollection<ArticleHeaderViewModel> _articleHeaders;

        public NavigationViewModel(IClientService clientService)
        {
            _clientService = clientService;
            SubscribeToGuiMessages();
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

        private void OnDeleteArticle(ArticleDeletedMessage deletedMessage)
        {
            ArticleHeaderViewModel header =
                ((from a in ArticleHeaders where a.Header.ArticleId == deletedMessage.ArticleId select a))
                    .FirstOrDefault();
            if (header == null)
            {
                return;
            }
            int index = ArticleHeaders.IndexOf(header);
            ArticleHeaders.RemoveAt(index);
            if (index < ArticleHeaders.Count)
            {
                Messenger.Default.Send(new ViewArticleMessage
                {
                    ArticleId = ArticleHeaders[index].Header.ArticleId.ToString()
                });
            }
            else
            {
                AddArticle();
            }
        }

        private void OnNewArticleAdded(NewArticleAddedMessage message)
        {
            ArticleHeaders.Add(
                new ArticleHeaderViewModel(new ArticleHeaderDto
                {
                    ArticleId = message.Article.Id,
                    Title = message.Article.Title
                }));
            RaisePropertyChanged(() => ArticleHeaders);
        }

        private void RebuildHeaderList()
        {
            try
            {
                ArticleHeaders = new ObservableCollection<ArticleHeaderViewModel>(
                    from a in _clientService.GetArticleHeaders() select new ArticleHeaderViewModel(a));
            }
            catch (DataServiceException ex)
            {
                Messenger.Default.Send(new ChangeStateMessage { State = ex.Message });
            }
        }

        private void SubscribeToGuiMessages()
        {
            Messenger.Default.Register(this, (RefreshHeaderListMessage message) => RebuildHeaderList());
            Messenger.Default.Register(this, (ArticleDeletedMessage message) => OnDeleteArticle(message));
            Messenger.Default.Register(this, (NewArticleAddedMessage message) => OnNewArticleAdded(message));
        }
    }
}
