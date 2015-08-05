using System;
using System.Globalization;
using AttikaContracts.DataTransferObjects;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Infotecs.Attika.AttikaGui.DataServices;
using Infotecs.Attika.AttikaGui.Messages.Gui;

namespace Infotecs.Attika.AttikaGui.ViewModels
{
    public sealed class CommentViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        private RelayCommand _deleteCommand;
        private RelayCommand _saveCommand;

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

        public RelayCommand DeleteCommand
        {
            get { return _deleteCommand ?? (_deleteCommand = new RelayCommand(Delete, CanDelete)); }
        }

        public RelayCommand SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new RelayCommand(Save, CanSave)); }
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

        private bool CanDelete()
        {
            return Comment.Id != Guid.Empty;
        }

        private bool CanSave()
        {
            return Comment.Id == Guid.Empty;
        }

        private void Delete()
        {
            try
            {
                _dataService.DeleteComment(Comment.ArticleId.ToString(), Comment.Id.ToString());
                Messenger.Default.Send(new CommentDeletedMessage { Comment = Comment });
                Messenger.Default.Send(new ChangeStateMessage { State = "ok" });
            }
            catch (DataServiceException ex)
            {
                Messenger.Default.Send(new ChangeStateMessage { State = ex.ToString() });
            }
        }

        private void Save()
        {
            try
            {
                Comment.Id = Guid.NewGuid();
                _dataService.NewComment(Comment.ArticleId.ToString(), Comment);
                SaveCommand.RaiseCanExecuteChanged();
                DeleteCommand.RaiseCanExecuteChanged();
                Messenger.Default.Send(new ChangeStateMessage { State = "ok" });
            }
            catch (DataServiceException ex)
            {
                Comment.Id = Guid.Empty;
                Messenger.Default.Send(new ChangeStateMessage { State = ex.ToString() });
            }
        }
    }
}
