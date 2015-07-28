using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Web;
using Infotecs.Attika.AtticaDataModel;
using Infotecs.Attika.AtticaDataModel.Repos;
using Infotecs.Attika.AttikaService.DTO;
using Infotecs.Attika.AttikaService.Validators;
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
            Configure();
        }

        public void NewArticle(ArticleDto article)
        {
            string[] validationErrors;
            if (!article.Validate(out validationErrors, true))
            {
                throw new WebFaultException<WebFaultDto>(
                    new WebFaultDto("Не удалось добавить статью", string.Join("\n", validationErrors)),
                    HttpStatusCode.BadRequest);
            }

            Article articleToSave;
            try
            {
                articleToSave = TinyMapper.Map<Article>(article);
            }
            catch (Exception ex)
            {
                throw new WebFaultException<WebFaultDto>(new WebFaultDto("Ошибка при маппинге статьи", ex.Message),
                                                         HttpStatusCode.BadRequest);
            }
            try
            {
                _commandRepo.CreateArticle(articleToSave);
            }
            catch (Exception ex)
            {
                throw new WebFaultException<WebFaultDto>(
                    new WebFaultDto("Ошибка при добавлении статьи в репозиторий", ex.Message),
                    HttpStatusCode.InternalServerError);
            }
        }

        public void AddComment(string articleId, CommentDto comment)
        {
            string[] validationErrors;
            if (!comment.Validate(out validationErrors))
            {
                throw new WebFaultException<WebFaultDto>(
                    new WebFaultDto("Не удалось добавить комментарий", string.Join("\n", validationErrors)),
                    HttpStatusCode.BadRequest);
            }
            try
            {
                _commandRepo.CreateComment(Guid.Parse(articleId), TinyMapper.Map<Comment>(comment));
            }
            catch (Exception ex)
            {
                throw new WebFaultException<WebFaultDto>(
                    new WebFaultDto("Ошибка при добавлении комментария к статье", ex.Message),
                    HttpStatusCode.InternalServerError);
            }
        }

        public IList<ArticleHeaderDto> GetArticleHeaders()
        {
            IEnumerable<ArticleHeader> headers;
            try
            {
                headers = _queryRepo.GetHeaders();
            }
            catch (Exception ex)
            {
                throw new WebFaultException<WebFaultDto>(
                    new WebFaultDto("Ошибка при получении списка статей из репозитория", ex.Message),
                    HttpStatusCode.InternalServerError);
            }

            try
            {
                IEnumerable<ArticleHeaderDto> mappedHeaders = headers.Select(x => TinyMapper.Map<ArticleHeaderDto>(x));
                return mappedHeaders.ToList();
            }
            catch (Exception ex)
            {
                throw new WebFaultException<WebFaultDto>(
                    new WebFaultDto("Ошибка при маппинге списка статей", ex.Message),
                    HttpStatusCode.InternalServerError);
            }
        }

        public ArticleDto GetArticle(string articleId)
        {
            Article article;
            try
            {
                article = _queryRepo.GetArticle(Guid.Parse(articleId));
            }
            catch (Exception ex)
            {
                throw new WebFaultException<WebFaultDto>(
                    new WebFaultDto("Ошибка при получении статьи из репозитория", ex.Message),
                    HttpStatusCode.InternalServerError);
            }
            try
            {
                return TinyMapper.Map<ArticleDto>(article);
            }
            catch (Exception ex)
            {
                throw new WebFaultException<WebFaultDto>(
                    new WebFaultDto("Ошибка при маппинге статьи", ex.Message),
                    HttpStatusCode.InternalServerError);
            }
        }

        public void DeleteArticle(string articleId)
        {
            try
            {
                _commandRepo.DeleteArticle(articleId);
            }
            catch (Exception ex)
            {
                throw new WebFaultException<WebFaultDto>(
                    new WebFaultDto("Ошибка при удалении статьи", ex.Message),
                    HttpStatusCode.InternalServerError);
            }
        }

        public void DeleteComment(string commentId)
        {
            try
            {
                _commandRepo.DeleteComment(commentId);
            }
            catch (Exception ex)
            {
                throw new WebFaultException<WebFaultDto>(
                    new WebFaultDto("Ошибка при удалении комментария", ex.Message),
                    HttpStatusCode.InternalServerError);
            }
        }

        private void Configure()
        {
            TinyMapper.Bind<ArticleDto, Article>();
            TinyMapper.Bind<CommentDto, Comment>();
        }
    }
}