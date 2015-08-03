using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Infotecs.Attika.AttikaGui.DataServices;
using Infotecs.Attika.AttikaGui.DataTransferObjects;
using Infotecs.Attika.AttikaGui.Messages.Gui;
using NLog;

namespace Infotecs.Attika.AttikaGui.ViewModels
{
    public sealed class ArticleViewModel : ViewModelBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IDataService _dataService;
        private RelayCommand _addCommentCommand;
        private ArticleDto _articleDto;
        private RelayCommand _badCommand;
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

        public RelayCommand BadCommand
        {
            get { return _badCommand ?? (_badCommand = new RelayCommand(Bad)); }
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

        private void Bad()
        {
            string guid = Guid.NewGuid().ToString();
            try
            {
                ArticleDto = _dataService.GetArticle(guid);
            }
            catch (DataServiceException ex)
            {
                Messenger.Default.Send(new ChangeStateMessage {State = ex.ToString()});
                Logger.Warn("Ошибка при попытке получения статьи с Id={0} : {1}", guid, ex);
            }
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
            catch (DataServiceException ex)
            {
                Messenger.Default.Send(new ChangeStateMessage {State = ex.ToString()});
            }
        }

        private bool CanSave()
        {
            return ArticleDto.Id == Guid.Empty;
        }

        private void Save()
        {
            ArticleDto.Id = Guid.NewGuid();
            try
            {
                _dataService.NewArticle(ArticleDto);
            }
            catch (DataServiceException ex)
            {
                Messenger.Default.Send(new ChangeStateMessage {State = ex.ToString()});
                return;
            }

            Messenger.Default.Send(new NewArticleAddedMessage {Article = ArticleDto});
        }
    }
}