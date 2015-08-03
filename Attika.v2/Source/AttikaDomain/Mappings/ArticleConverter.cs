using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Infotecs.Attika.AttikaDomain.Aggregates;
using Infotecs.Attika.AttikaInfrastructure.Data.DataTransferObjects;
using Infotecs.Attika.AttikaInfrastructure.Data.Models;

namespace Infotecs.Attika.AttikaDomain.Mappings
{
    public sealed class ArticleConverter : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof (ArticleDto);
        }

        //what if dto changes
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destinationType)
        {
            var article = (Article) value;
            var dto = new ArticleDto
            {
                Created = article.Created,
                Description = article.Description,
                Text = article.Text,
                Title = article.Title,
                Id = article.Id
            };
            if (article.Comments != null)
            {
                dto.Comments = (from c in article.Comments
                    select
                        new CommentDto()
                        {
                            ArticleId = article.Id,
                            Id = c.Id,
                            Text = c.Text,
                            Created = c.Created
                        }).ToList();
            }
            return article;
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof (ArticleState);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var article = (ArticleState) value;
            if (article != null)
            {
                var dto = new ArticleDto
                {
                    Id = article.Id,
                    Created = article.Created,
                    Text = article.Text,
                    Description = article.Description,
                    Title = article.Title
                };
                if (article.Comments != null)
                {
                    dto.Comments =
                        (from c in article.Comments
                            select
                                new CommentDto {Created = c.Created, Id = c.Id, Text = c.Text, ArticleId = c.ArticleId})
                            .ToList();
                }
                return dto;
            }
            return null;
        }
    }
}