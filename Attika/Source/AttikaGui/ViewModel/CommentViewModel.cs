using System.Globalization;
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
    public class CommentViewModel : ViewModelBase
    {
        private readonly CommentDto _comment;

        /// <summary>
        ///     Initializes a new instance of the CommentViewModel class.
        /// </summary>
        public CommentViewModel(CommentDto comment)
        {
            _comment = comment;
        }

        public CommentDto Comment
        {
            get { return _comment; }
        }

        public string Created
        {
            get { return _comment.Created.ToString(CultureInfo.InvariantCulture); }
        }

        public string Text
        {
            get { return _comment.Text; }
            set
            {
                _comment.Text = value;
                RaisePropertyChanged();
            }
        }
    }
}