using System;
using System.Collections.Generic;
using Infotecs.Attika.AttikaDomain.Aggregates;
using Infotecs.Attika.AttikaDomain.Factories.Contracts;
using Infotecs.Attika.AttikaDomain.Validators.Contracts;
using Infotecs.Attika.AttikaInfrastructure.Data.DataTransferObjects;
using Infotecs.Attika.AttikaInfrastructure.Data.Models;
using Infotecs.Attika.AttikaInfrastructure.Data.Repositories;
using Infotecs.Attika.AttikaInfrastructure.Data.Repositories.Exceptions;
using Infotecs.Attika.AttikaInfrastructure.Services.Contracts;

namespace Infotecs.Attika.AttikaDomain.Factories
{
    public class ArticleFactory : IArticleFactory
    {
        private readonly ICommentFactory _commentFactory;
        private readonly IMappingService _mappingService;
        private readonly IQueryRepository _queryRepository;
        private readonly IValidator<Article> _validator;

        public ArticleFactory(IQueryRepository queryRepository, IValidator<Article> validator,
            ICommentFactory commentFactory,
            IMappingService mappingService)
        {
            _queryRepository = queryRepository;
            _validator = validator;
            _commentFactory = commentFactory;
            _mappingService = mappingService;
        }

        public Article CreateArticle(Guid id)
        {
            ArticleState articleState;
            articleState = _queryRepository.GetArticle(id);
            if (articleState == null)
            {
                return null;
            }
            var article = Article.Create(articleState);
            return article;
        }

        public Article NewArticle(string title, string description, string text)
        {
            var article = Article.Create(new ArticleState
            {
                Comments = new List<CommentState>(),
                Created = DateTime.Now,
                Description = description,
                Id = Guid.NewGuid(),
                Text = text,
                Title = title
            });
            string[] errors;
            if (!_validator.Validate(article, out errors))
            {
                throw new ArgumentException(string.Join("\n", errors));
            }
            return article;
        }

        public Article CreateArticle(ArticleDto articleDto)
        {
            var state = _mappingService.Map<ArticleState>(articleDto);
            var article = Article.Create(state);
            string[] errors;
            if (!_validator.Validate(article, out errors))
            {
                throw new ArgumentException(string.Join("\n", errors));
            }
            return article;
        }
    }
}