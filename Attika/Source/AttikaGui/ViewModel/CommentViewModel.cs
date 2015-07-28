using System;
using System.Globalization;
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
    public sealed class CommentViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        private RelayCommand _saveCommand;

        /// <summary>
        ///     Initializes a new instance of the CommentViewModel class.
        /// </summary>
        public CommentViewModel(CommentDto comment, IDataService dataService)
        {
            Comment = comment;
            _dataService = dataService;
        }

        public CommentDto Comment { get; set; }

        public string Created
        {
            get { return Comment.Created.ToString(CultureInfo.InvariantCulture); }
        }

        public string Text
        {
            get { return Comment.Text; }
            set
            {
                Comment.Text = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new RelayCommand(Save, CanSave)); }
        }

        private bool CanSave()
        {
            return Comment.Id == Guid.Empty;
        }

        private void Save()
        {
            try
            {
                Comment.Id = Guid.NewGuid();
                _dataService.NewComment(Comment.ArticleId.ToString(), Comment);
                Messenger.Default.Send(new ViewArticleMessage {ArticleId = Comment.ArticleId.ToString()});
                Messenger.Default.Send(new ChangeStateMessage {State = "ok"});
            }
            catch (Exception ex)
            {
                Comment.Id = Guid.Empty;
                Messenger.Default.Send(new ChangeStateMessage {State = ex.Message});
            }
        }
    }
}