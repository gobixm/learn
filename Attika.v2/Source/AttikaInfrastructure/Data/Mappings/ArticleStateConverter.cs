using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using AttikaContracts.DataTransferObjects;
using Infotecs.Attika.AttikaInfrastructure.Data.Models;

namespace Infotecs.Attika.AttikaInfrastructure.Data.Mappings
{
    public class ArticleStateConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(ArticleDto);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(ArticleDto);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var dto = (ArticleDto)value;
            var article = new ArticleState
            {
                Created = dto.Created,
                Description = dto.Description,
                Text = dto.Text,
                Title = dto.Title,
                Id = dto.Id
            };
            if (dto.Comments != null)
            {
                article.Comments = (from c in dto.Comments
                                    select
                                        new CommentState
                                        {
                                            ArticleState = article,
                                            ArticleId = c.ArticleId,
                                            Id = c.Id,
                                            Text = c.Text,
                                            Created = c.Created
                                        }).ToList();
            }
            return article;
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destinationType)
        {
            var article = (ArticleState)value;
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
                             new CommentDto { Created = c.Created, Id = c.Id, Text = c.Text, ArticleId = c.ArticleId })
                            .ToList();
                }
                return dto;
            }
            return null;
        }
    }
}
