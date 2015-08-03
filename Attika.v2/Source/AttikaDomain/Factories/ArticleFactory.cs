using System;
using System.Linq;
using Infotecs.Attika.AttikaDomain.Aggregates;
using Infotecs.Attika.AttikaDomain.Entities;
using Infotecs.Attika.AttikaDomain.Factories.Contracts;
using Infotecs.Attika.AttikaDomain.Validators.Contracts;
using Infotecs.Attika.AttikaInfrastructure.Data.DataTransferObjects;
using Infotecs.Attika.AttikaInfrastructure.Data.Models;
using Infotecs.Attika.AttikaInfrastructure.Data.Repositories;
using Infotecs.Attika.AttikaInfrastructure.Services.Contracts;

namespace Infotecs.Attika.AttikaDomain.Factories
{
    public sealed class ArticleFactory : IArticleFactory
    {
        private readonly IValidator<Article> _articleValidator;
        private readonly IValidator<Comment> _commentValidator;
        private readonly IMappingService _mappingService;
        private readonly IQueryRepository _queryRepository;

        public ArticleFactory(IQueryRepository queryRepository, IValidator<Article> articleValidator,
                              IValidator<Comment> commentValidator, IMappingService mappingService)
        {
            _queryRepository = queryRepository;
            _articleValidator = articleValidator;
            _commentValidator = commentValidator;
            _mappingService = mappingService;
        }

        public Article CreateArticleFromRepository(Guid id)
        {
            ArticleState articleState = _queryRepository.GetArticle(id);
            if (articleState == null)
            {
                return null;
            }
            Article article = Article.Create(articleState);
            return article;
        }

        public Article CreateArticle(ArticleDto articleDto)
        {
            var state = _mappingService.Map<ArticleState>(articleDto);
            Article article = Article.Create(state);
            string[] errors;
            if (!_articleValidator.Validate(article, out errors))
            {
                throw new ArgumentException(string.Join("\n", errors));
            }
            if (article.Comments != null)
            {
                if (article.Comments.Any(comment => !_commentValidator.Validate(comment, out errors)))
                {
                    throw new ArgumentException(string.Join("\n", errors));
                }
            }
            return article;
        }
    }
}