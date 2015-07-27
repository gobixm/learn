using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Infotecs.Attika.AttikaGui.DTO;
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
        private ArticleDto _articleDto;
        private ObservableCollection<CommentViewModel> _comments;
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
                if ((value != null) && (value.Comments != null))
                    Comments =
                        new ObservableCollection<CommentViewModel>(from c in ArticleDto.Comments
                                                                   select new CommentViewModel(c));
                else
                {
                    Comments = null;
                }

                RaisePropertyChanged(() => Comments);
            }
        }

        public string Title
        {
            get { return ArticleDto.Title; }
            set
            {
                ArticleDto.Title = Title;
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

        private bool CanSave()
        {
            return ArticleDto.Id == Guid.Empty;
        }

        private void Save()
        {
            ArticleDto.Id = Guid.NewGuid();
            _dataService.NewArticle(ArticleDto);
        }
    }
}