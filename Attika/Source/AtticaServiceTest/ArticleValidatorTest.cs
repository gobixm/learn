using System;
using System.Collections.Generic;
using Infotecs.Attika.AttikaService.DTO;
using Infotecs.Attika.AttikaService.Validators;
using Xunit;

namespace AtticaServiceTest
{
    public class ArticleValidatorTest
    {
        private ArticleDto CreateValidArticle()
        {
            Guid articleId = Guid.Parse("A49C4E5D-A9B1-4364-9E43-CF0329D7BA10");
            return new ArticleDto
                {
                    Created = DateTime.Today,
                    Description = "description",
                    Title = "title",
                    Id = articleId,
                    Text = "text",
                    Comments = new List<CommentDto>
                        {
                            new CommentDto
                                {
                                    ArticleId = articleId,
                                    Created = DateTime.Today,
                                    Id = Guid.Parse("A49C4E5D-A9B1-4364-9E43-CF0329D7BA11"),
                                    Text = "comment1"
                                },
                            new CommentDto
                                {
                                    ArticleId = articleId,
                                    Created = DateTime.Today,
                                    Id = Guid.Parse("A49C4E5D-A9B1-4364-9E43-CF0329D7BA12"),
                                    Text = "comment1"
                                },
                            new CommentDto
                                {
                                    ArticleId = articleId,
                                    Created = DateTime.Today,
                                    Id = Guid.Parse("A49C4E5D-A9B1-4364-9E43-CF0329D7BA13"),
                                    Text = "comment1"
                                }
                        }
                };
        }

        [Fact]
        private void AssumeArticleTitleMoreThan100()
        {
            ArticleDto articleDto = CreateValidArticle();
            articleDto.Title = "".PadLeft(101);
            string[] errors;
            Assert.False(articleDto.Validate(out errors));
        }

        [Fact]
        private void AssumeArticleTitleMoreThan100ReturnsError()
        {
            ArticleDto articleDto = CreateValidArticle();
            articleDto.Title = "".PadLeft(101);
            string[] errors;
            articleDto.Validate(out errors);
            Assert.Equal("Заголовок статьи не может превышать 100 символов.", errors[0].Trim());
        }

        [Fact]
        private void AssumeArticleTitleLengthEqual100()
        {
            ArticleDto articleDto = CreateValidArticle();
            articleDto.Title = "".PadLeft(100);
            string[] errors;
            Assert.True(articleDto.Validate(out errors));
        }

        [Fact]
        private void AssumeArticleTitleLessThan100()
        {
            ArticleDto articleDto = CreateValidArticle();
            articleDto.Title = "".PadLeft(99);
            string[] errors;
            Assert.True(articleDto.Validate(out errors));
        }

        [Fact]
        private void AssumeArticleTextMoreThan200()
        {
            ArticleDto articleDto = CreateValidArticle();
            articleDto.Text = "".PadLeft(201);
            string[] errors;
            Assert.False(articleDto.Validate(out errors));
        }

        [Fact]
        private void AssumeArticleTextMoreThan200ReturnsError()
        {
            ArticleDto articleDto = CreateValidArticle();
            articleDto.Text = "".PadLeft(201);
            string[] errors;
            articleDto.Validate(out errors);
            Assert.Equal("Текст статьи не может превышать 200 символов.", errors[0].Trim());
        }

        [Fact]
        private void AssumeArticleTextLengthEqualsThan200()
        {
            ArticleDto articleDto = CreateValidArticle();
            articleDto.Text = "".PadLeft(200);
            string[] errors;
            Assert.True(articleDto.Validate(out errors));
        }

        [Fact]
        private void AssumeArticleTextLessThan200()
        {
            ArticleDto articleDto = CreateValidArticle();
            articleDto.Text = "".PadLeft(199);
            string[] errors;
            Assert.True(articleDto.Validate(out errors));
        }

        [Fact]
        private void AssumeArticleHaveBadComment()
        {
            ArticleDto articleDto = CreateValidArticle();
            articleDto.Comments[0].Text = "".PadLeft(200);
            string[] errors;
            Assert.False(articleDto.Validate(out errors, true));
        }
    }
}