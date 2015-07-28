using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
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
    public sealed class ArticleViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        private RelayCommand _addCommentCommand;
        private ArticleDto _articleDto;
        private ObservableCollection<CommentViewModel> _comments;
        private RelayCommand _deleteCommand;
        private RelayCommand _saveCommand;

        public ArticleViewModel(ArticleDto articleDto, IDataService dataService) : this(dataService)
        {
            ArticleDto = articleDto;
        }

        public ArticleViewModel(IDataService dataService)
        {
            _dataService = dataService;
            ArticleDto = new ArticleDto
                {
                    Id = Guid.Empty,
                    Created = DateTime.Today,
                    Description = "",
                    Text = "",
                    Title = "Новая статья",
                    Comments = new List<CommentDto>()
                };
        }

        public ArticleDto ArticleDto
        {
            get { return _articleDto; }
            set
            {
                _articleDto = value;
                RaisePropertyChanged(() => Title);
                RaisePropertyChanged(() => Description);
                RaisePropertyChanged(() => Text);
                RaisePropertyChanged(() => Created);
                SaveCommand.RaiseCanExecuteChanged();
                DeleteCommand.RaiseCanExecuteChanged();
                AddCommentCommand.RaiseCanExecuteChanged();
                if ((value != null) && (value.Comments != null))
                    Comments =
                        new ObservableCollection<CommentViewModel>(from c in ArticleDto.Comments
                                                                   select new CommentViewModel(c, _dataService));
                else
                {
                    Comments = null;
                }

                RaisePropertyChanged(() => Comments);
            }
        }

        public RelayCommand AddCommentCommand
        {
            get { return _addCommentCommand ?? (_addCommentCommand = new RelayCommand(AddComment, CanAddComment)); }
        }

        public string Title
        {
            get { return ArticleDto.Title; }
            set
            {
                ArticleDto.Title = value;
                RaisePropertyChanged();
            }
        }

        public string Description
        {
            get { return ArticleDto.Description; }
            set
            {
                ArticleDto.Description = value;
                RaisePropertyChanged();
            }
        }

        public string Text
        {
            get { return ArticleDto.Text; }
            set
            {
                ArticleDto.Text = value;
                RaisePropertyChanged();
            }
        }

        public string Created
        {
            get { return ArticleDto.Created.ToString(CultureInfo.InvariantCulture); }
        }

        public ObservableCollection<CommentViewModel> Comments
        {
            get { return _comments; }
            set
            {
                _comments = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new RelayCommand(Save, CanSave)); }
        }

        public RelayCommand DeleteCommand
        {
            get { return _deleteCommand ?? (_deleteCommand = new RelayCommand(Delete, CanDelete)); }
        }

        private bool CanAddComment()
        {
            return ArticleDto.Id != Guid.Empty;
        }

        private void AddComment()
        {
            Comments.Insert(0, new CommentViewModel(new CommentDto {ArticleId = ArticleDto.Id, Created = DateTime.Now},
                                                    _dataService));
            RaisePropertyChanged(() => Comments);
        }

        private bool CanDelete()
        {
            return ArticleDto.Id != Guid.Empty;
        }

        private void Delete()
        {
            try
            {
                _dataService.DeleteArticle(ArticleDto.Id.ToString());
                Messenger.Default.Send(new DeleteArticleMessage {ArticleId = ArticleDto.Id});
                Messenger.Default.Send(new ChangeStateMessage {State = "ok"});
            }
            catch (Exception ex)
            {
                Messenger.Default.Send(new ChangeStateMessage {State = ex.Message});
            }
        }

        private bool CanSave()
        {
            return ArticleDto.Id == Guid.Empty;
        }

        private void Save()
        {
            ArticleDto.Id = Guid.NewGuid();
            _dataService.NewArticle(ArticleDto);
            Messenger.Default.Send(new RefreshHeaderListMessage());
            ArticleDto = _dataService.GetArticle(ArticleDto.Id.ToString());
        }
    }
}