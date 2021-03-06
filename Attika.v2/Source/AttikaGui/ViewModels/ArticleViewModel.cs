﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using AttikaContracts.DataTransferObjects;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Infotecs.Attika.AttikaClient;
using Infotecs.Attika.AttikaGui.Messages.Gui;
using NLog;
using DataServiceException = Infotecs.Attika.AttikaGui.DataServices.DataServiceException;

namespace Infotecs.Attika.AttikaGui.ViewModels
{
    public sealed class ArticleViewModel : ViewModelBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IClientService _clientService;
        private RelayCommand _addCommentCommand;
        private ArticleDto _articleDto;
        private RelayCommand _badCommand;
        private ObservableCollection<CommentViewModel> _comments;
        private RelayCommand _deleteCommand;
        private RelayCommand _saveCommand;

        public ArticleViewModel(ArticleDto articleDto, IClientService clientService) : this(clientService)
        {
            ArticleDto = articleDto;
        }

        public ArticleViewModel(IClientService clientService)
        {
            _clientService = clientService;
            ArticleDto = new ArticleDto
            {
                Id = Guid.Empty,
                Created = DateTime.Today,
                Description = "",
                Text = "",
                Title = "Новая статья",
                Comments = new List<CommentDto>()
            };
            SubscribeToGuiMessages();
        }

        public RelayCommand AddCommentCommand
        {
            get { return _addCommentCommand ?? (_addCommentCommand = new RelayCommand(AddComment, CanAddComment)); }
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
                {
                    Comments =
                        new ObservableCollection<CommentViewModel>(from c in ArticleDto.Comments
                                                                   select new CommentViewModel(c, _clientService));
                }
                else
                {
                    Comments = null;
                }

                RaisePropertyChanged(() => Comments);
            }
        }

        public RelayCommand BadCommand
        {
            get { return _badCommand ?? (_badCommand = new RelayCommand(Bad)); }
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

        public string Created
        {
            get { return ArticleDto.Created.ToString(CultureInfo.InvariantCulture); }
        }

        public RelayCommand DeleteCommand
        {
            get { return _deleteCommand ?? (_deleteCommand = new RelayCommand(Delete, CanDelete)); }
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

        public RelayCommand SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new RelayCommand(Save, CanSave)); }
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

        public string Title
        {
            get { return ArticleDto.Title; }
            set
            {
                ArticleDto.Title = value;
                RaisePropertyChanged();
            }
        }

        private void AddComment()
        {
            Comments.Insert(0, new CommentViewModel(new CommentDto { ArticleId = ArticleDto.Id, Created = DateTime.Now },
                _clientService));
            RaisePropertyChanged(() => Comments);
        }

        private void Bad()
        {
            string guid = Guid.NewGuid().ToString();
            try
            {
                ArticleDto = _clientService.GetArticle(guid);
            }
            catch (DataServiceException ex)
            {
                Messenger.Default.Send(new ChangeStateMessage { State = ex.ToString() });
                Logger.Warn("Ошибка при попытке получения статьи с Id={0} : {1}", guid, ex);
            }
        }

        private bool CanAddComment()
        {
            return ArticleDto.Id != Guid.Empty;
        }

        private bool CanDelete()
        {
            return ArticleDto.Id != Guid.Empty;
        }

        private bool CanSave()
        {
            return ArticleDto.Id == Guid.Empty;
        }

        private void Delete()
        {
            try
            {
                _clientService.DeleteArticle(ArticleDto.Id.ToString());
                Messenger.Default.Send(new ArticleDeletedMessage { ArticleId = ArticleDto.Id });
                Messenger.Default.Send(new ChangeStateMessage { State = "ok" });
            }
            catch (DataServiceException ex)
            {
                Messenger.Default.Send(new ChangeStateMessage { State = ex.ToString() });
            }
        }

        private void OnCommentDeleted(CommentDeletedMessage message)
        {
            CommentViewModel comment =
                (from c in Comments where c.Comment.Id == message.Comment.Id select c).FirstOrDefault();
            Comments.Remove(comment);
        }

        private void Save()
        {
            ArticleDto.Id = Guid.NewGuid();
            ArticleDto.Created = DateTime.Today;
            try
            {
                _clientService.NewArticle(ArticleDto);
            }
            catch (DataServiceException ex)
            {
                Messenger.Default.Send(new ChangeStateMessage { State = ex.ToString() });
                return;
            }

            Messenger.Default.Send(new NewArticleAddedMessage { Article = ArticleDto });
        }

        private void SubscribeToGuiMessages()
        {
            Messenger.Default.Register(this, (CommentDeletedMessage message) => OnCommentDeleted(message));
        }
    }
}
