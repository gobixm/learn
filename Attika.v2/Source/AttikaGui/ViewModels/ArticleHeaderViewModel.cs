using AttikaContracts.DataTransferObjects;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Infotecs.Attika.AttikaGui.Messages.Gui;

namespace Infotecs.Attika.AttikaGui.ViewModels
{
    public sealed class ArticleHeaderViewModel : ViewModelBase
    {
        private string _title;
        private RelayCommand _viewArticleCommand;

        public ArticleHeaderViewModel(ArticleHeaderDto header)
        {
            Header = header;
            Title = header.Title;
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }

        public ArticleHeaderDto Header { get; private set; }

        public RelayCommand ViewArticleCommand
        {
            get { return _viewArticleCommand ?? (_viewArticleCommand = new RelayCommand(ViewArticle)); }
        }

        private void ViewArticle()
        {
            Messenger.Default.Send(new ViewArticleMessage {ArticleId = Header.ArticleId.ToString()});
        }
    }
}