using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Infotecs.Attika.AttikaGui.DTO;
using Infotecs.Attika.AttikaGui.GuiMessages;

namespace Infotecs.Attika.AttikaGui.ViewModel
{
    /// <summary>
    ///     This class contains properties that a View can data bind to.
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public class ArticleHeaderViewModel : ViewModelBase
    {
        private RelayCommand _viewArticleCommand;

        /// <summary>
        ///     Initializes a new instance of the ArticleHeaderViewModel class.
        /// </summary>
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