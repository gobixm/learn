using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Infotecs.Attika.AttikaGui.DataTransferObjects;
using Infotecs.Attika.AttikaGui.GuiMessages;

namespace Infotecs.Attika.AttikaGui.ViewModel
{
    public sealed class ArticleHeaderViewModel : ViewModelBase
    {
        private RelayCommand _viewArticleCommand;

        public ArticleHeaderViewModel(ArticleHeaderDto header)
        {
            Header = header;
            Title = header.Title;
        }

        public string Title { get; set; }
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