using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Infotecs.Attika.AttikaGui.DataServices;
using Infotecs.Attika.AttikaGui.DataTransferObjects;
using Infotecs.Attika.AttikaGui.GuiMessages;

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

        private void RebuildHeaderList()
        {
            try
            {
                ArticleHeaders = new ObservableCollection<ArticleHeaderViewModel>(
                    from a in _dataService.GetArticleHeaders() select new ArticleHeaderViewModel(a));
            }
            catch (DataServiceException ex)
            {
                Messenger.Default.Send(new ChangeStateMessage {State = ex.Message});
            }
        }

        private void SubscribeToGuiMessages()
        {
            Messenger.Default.Register(this, (RefreshHeaderListMessage message) => RebuildHeaderList());
            Messenger.Default.Register(this, (DeleteArticleMessage message) => OnDeleteArticle(message));
        }

        private void OnDeleteArticle(DeleteArticleMessage message)
        {
            ArticleHeaderViewModel header =
                ((from a in ArticleHeaders where a.Header.ArticleId == message.ArticleId select a)).FirstOrDefault();
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