﻿using System;
using System.Collections.Generic;
using System.Linq;
using AttikaContracts.DataTransferObjects;

namespace Infotecs.Attika.AttikaGui.DesignTimes
{
    public static class DesignDataFixture
    {
        public static List<ArticleHeaderDto> BuildHeaders(IEnumerable<ArticleDto> articles)
        {
            return
                articles.Select(articleDto => new ArticleHeaderDto { ArticleId = articleDto.Id, Title = articleDto.Title })
                    .ToList();
        }

        public static List<ArticleDto> GetFakeArticles()
        {
            var articles = new List<ArticleDto>();
            for (int i = 0; i < 32; i++)
            {
                var article = new ArticleDto
                {
                    Description = "Test description " + i,
                    Created = DateTime.Today + TimeSpan.FromHours(i),
                    Id = Guid.NewGuid(),
                    Text = "Test text " + i,
                    Title = "Test title " + i,
                    Comments = new List<CommentDto>()
                };
                for (int j = 0; j < 32; j++)
                {
                    article.Comments.Add(new CommentDto
                    {
                        ArticleId = article.Id,
                        Created = DateTime.Today + TimeSpan.FromHours(j),
                        Id = Guid.NewGuid(),
                        Text = "Comment text " + i + "-" + j
                    });
                }
                articles.Add(article);
            }
            return articles;
        }
    }
}
