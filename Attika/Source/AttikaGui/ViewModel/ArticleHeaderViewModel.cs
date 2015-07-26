using GalaSoft.MvvmLight;
using Infotecs.Attika.AttikaGui.DTO;

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
        private ArticleHeaderDto _header;

        /// <summary>
        ///     Initializes a new instance of the ArticleHeaderViewModel class.
        /// </summary>
        public ArticleHeaderViewModel(ArticleHeaderDto header)
        {
            _header = header;
            Title = header.Title;
        }

        public string Title { get; set; }
    }
}