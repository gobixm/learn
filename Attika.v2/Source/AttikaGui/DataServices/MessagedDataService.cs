﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using AttikaContracts.DataTransferObjects;
using AttikaContracts.Messages;
using Nelibur.ServiceModel.Clients;

namespace Infotecs.Attika.AttikaGui.DataServices
{
    public sealed class MessagedDataService : IDataService
    {
        private readonly JsonServiceClient _webClient;

        public MessagedDataService()
        {
            _webClient = new JsonServiceClient(ConfigurationManager.ConnectionStrings["host"].ConnectionString);
        }

        public IEnumerable<ArticleHeaderDto> GetArticleHeaders()
        {
            try
            {
                var response = _webClient.Get<GetArticleHeadersResponse>(new GetArticleHeadersRequest());
                return response.Headers;
            }
            catch (Exception ex)
            {
                throw new DataServiceException(new FaultDto(ex.Message, ex.ToString()));
            }
        }

        public ArticleDto GetArticle(string articleId)
        {
            try
            {
                var response = _webClient.Get<GetArticleResponse>(new GetArticleRequest {Id = articleId});
                return response.Article;
            }
            catch (Exception ex)
            {
                throw new DataServiceException(new FaultDto(ex.Message, ex.ToString()));
            }
        }

        public void NewArticle(ArticleDto article)
        {
            try
            {
                _webClient.Post(new NewArticleRequest {Article = article});
            }
            catch (Exception ex)
            {
                throw new DataServiceException(new FaultDto(ex.Message, ex.ToString()));
            }
        }

        public void NewComment(string articleId, CommentDto comment)
        {
            try
            {
                _webClient.Post(new AddArticleCommentRequest {ArticleId = articleId, Comment = comment});
            }
            catch (Exception ex)
            {
                throw new DataServiceException(new FaultDto(ex.Message, ex.ToString()));
            }
        }

        public void DeleteArticle(string articleId)
        {
            try
            {
                _webClient.Delete(new DeleteArticleRequest {ArticleId = articleId});
            }
            catch (Exception ex)
            {
                throw new DataServiceException(new FaultDto(ex.Message, ex.ToString()));
            }
        }

        public void DeleteComment(string articleId, string commentId)
        {
            try
            {
                _webClient.Delete(new DeleteArticleCommentRequest {ArticleId = articleId, CommentId = commentId});
            }
            catch (Exception ex)
            {
                throw new DataServiceException(new FaultDto(ex.Message, ex.ToString()));
            }
        }

        private string BuildQueryParams(object message)
        {
            var pairs = new List<string>();
            foreach (PropertyInfo property in message.GetType().GetProperties())
            {
                pairs.Add(string.Format("{0}={1}", property.Name, property.GetValue(message)));
            }
            return string.Join("&", pairs);
        }
    }
}