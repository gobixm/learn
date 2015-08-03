using System;
using System.Collections.Generic;
using System.Text;
using Infotecs.Attika.AttikaService.Validators;
using Infotecs.Attika.AttikaSharedDataObjects.DataTransferObjects;
using Xunit;

namespace AtticaServiceTest
{
    public class ArticleValidatorTest
    {
        [Theory]
        [InlineData(101)]
        private void TestInvalidArticleTitle(int titleLength)
        {
            var title = new string('x', titleLength);
            var article = new ArticleDto {Title = title};
            string[] errors;
            var valid = article.Validate(out errors, true);
            Assert.False(valid, string.Join(";",errors));
        }

        [Theory]
        [InlineData(201)]
        private void TestInvalidArticleText(int textLength)
        {
            var text = new string('x', textLength);
            var article = new ArticleDto { Title = text };
            string[] errors;
            var valid = article.Validate(out errors, true);
            Assert.False(valid, string.Join(";", errors));
        }

        [Theory]
        [InlineData(51)]
        private void TestInvalidArticleCommentText(int textLength)
        {
            var text = new string('x', textLength);
            var article = new ArticleDto { Title = text, Comments = new List<CommentDto>(){new CommentDto(){Text = text}}};
            string[] errors;
            var valid = article.Validate(out errors, true);
            Assert.False(valid, string.Join(";", errors));
        }
    }
}