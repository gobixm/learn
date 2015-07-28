using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Infotecs.Attika.AtticaDataModel;
using Infotecs.Attika.AttikaService.DTO;

namespace Infotecs.Attika.AttikaService.Mapping
{
    public class ArticleDtoConverter : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof (Article);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
                                         Type destinationType)
        {
            var dto = (ArticleDto) value;
            var article = new Article
                {
                    Created = dto.Created,
                    Description = dto.Description,
                    Text = dto.Text,
                    Title = dto.Title,
                    Id = dto.Id
                };
            if (dto.Comments != null)
            {
                article.Comments = (from c in article.Comments
                                    select
                                        new Comment
                                            {
                                                Article = article,
                                                ArticleId = c.ArticleId,
                                                Id = c.Id,
                                                Text = c.Text,
                                                Created = c.Created
                                            }).ToList();
            }
            return article;
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof (Article);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var article = (Article) value;
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
                    (from c in article.Comments select new CommentDto {Created = c.Created, Id = c.Id, Text = c.Text})
                        .ToList();
            }
            return dto;
        }
    }
}