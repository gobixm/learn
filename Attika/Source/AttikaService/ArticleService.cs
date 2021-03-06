﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Web;
using Infotecs.Attika.AtticaDataModel;
using Infotecs.Attika.AtticaDataModel.Repositories;
using Infotecs.Attika.AttikaService.Validators;
using Infotecs.Attika.AttikaSharedDataObjects.DataTransferObjects;
using Infotecs.Attika.AttikaSharedDataObjects.Mappings;

namespace Infotecs.Attika.AttikaService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class ArticleService : IArticleService
    {
        private readonly ICommandRepository _commandRepository;
        private readonly IMapper _mapper;
        private readonly IQueryRepository _queryRepository;

        public ArticleService(ICommandRepository commandRepository, IQueryRepository queryRepository, IMapper mapper)
        {
            _commandRepository = commandRepository;
            _queryRepository = queryRepository;
            _mapper = mapper;
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
                articleToSave = _mapper.Map<Article>(article);
            }
            catch (Exception ex)
            {
                throw new WebFaultException<WebFaultDto>(new WebFaultDto("Ошибка при маппинге статьи", ex.Message),
                                                         HttpStatusCode.BadRequest);
            }
            try
            {
                _commandRepository.CreateArticle(articleToSave);
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
                _commandRepository.CreateComment(Guid.Parse(articleId), _mapper.Map<Comment>(comment));
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
                headers = _queryRepository.GetHeaders();
            }
            catch (Exception ex)
            {
                throw new WebFaultException<WebFaultDto>(
                    new WebFaultDto("Ошибка при получении списка статей из репозитория", ex.Message),
                    HttpStatusCode.InternalServerError);
            }

            try
            {
                IEnumerable<ArticleHeaderDto> mappedHeaders = headers.Select(_mapper.Map<ArticleHeaderDto>);
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
                article = _queryRepository.GetArticle(Guid.Parse(articleId));
            }
            catch (Exception ex)
            {
                throw new WebFaultException<WebFaultDto>(
                    new WebFaultDto("Ошибка при получении статьи из репозитория", ex.Message),
                    HttpStatusCode.InternalServerError);
            }
            if (article == null)
            {
                throw new WebFaultException<WebFaultDto>(
                    new WebFaultDto("Ошибка при получении статьи", "Статьи с ID=" + articleId + " не существует"),
                    HttpStatusCode.NotFound);
            }
            try
            {
                return _mapper.Map<ArticleDto>(article);
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
                _commandRepository.DeleteArticle(articleId);
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
                _commandRepository.DeleteComment(commentId);
            }
            catch (Exception ex)
            {
                throw new WebFaultException<WebFaultDto>(
                    new WebFaultDto("Ошибка при удалении комментария", ex.Message),
                    HttpStatusCode.InternalServerError);
            }
        }
    }
}