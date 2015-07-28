using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Infotecs.Attika.AtticaDataModel;
using Infotecs.Attika.AtticaDataModel.Repos;
using Infotecs.Attika.AttikaService.DTO;
using Nelibur.ObjectMapper;

namespace Infotecs.Attika.AttikaService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class ArticleService : IArticleService
    {
        private readonly ICommandRepo _commandRepo;
        private readonly IQueryRepo _queryRepo;

        public ArticleService(ICommandRepo commandRepo, IQueryRepo queryRepo)
        {
            _commandRepo = commandRepo;
            _queryRepo = queryRepo;
            TinyMapper.Bind<ArticleDto, Article>();
            TinyMapper.Bind<CommentDto, Comment>();
        }

        public void NewArticle(ArticleDto article)
        {
            var atkl = TinyMapper.Map<Article>(article);
            _commandRepo.CreateArticle(atkl);
        }

        public void AddComment(string articleId, CommentDto comment)
        {
            _commandRepo.CreateComment(Guid.Parse(articleId), TinyMapper.Map<Comment>(comment));
        }

        public IList<ArticleHeaderDto> GetArticleHeaders()
        {
            IEnumerable<ArticleHeader> headers = _queryRepo.GetHeaders();
            IEnumerable<ArticleHeaderDto> mappedHeaders = headers.Select(x => TinyMapper.Map<ArticleHeaderDto>(x));
            return mappedHeaders.ToList();
        }

        public ArticleDto GetArticle(string articleId)
        {
            return TinyMapper.Map<ArticleDto>(_queryRepo.GetArticle(Guid.Parse(articleId)));
        }


        public void DeleteArticle(string articleId)
        {
            _commandRepo.DeleteArticle(articleId);
        }

        public void DeleteComment(string commentId)
        {
            _commandRepo.DeleteComment(commentId);
        }
    }
}