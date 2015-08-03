using System.Collections.Generic;
using Infotecs.Attika.AttikaService.Validators;
using Infotecs.Attika.AttikaSharedDataObjects.DataTransferObjects;
using Xunit;

namespace AtticaServiceTest
{
    public class ArticleValidatorTest
    {
        private string GetStringWithDesiredLength(int length)
        {
            return new string('x', length);
        }

        [Theory]
        [InlineData(100)]
        [InlineData(0)]
        private void Check_ArticleTitle_Valid(int titleLength)
        {
            string title = GetStringWithDesiredLength(titleLength);
            var article = new ArticleDto {Title = title};
            string[] errors;
            bool valid = article.Validate(out errors, true);
            Assert.True(valid);
        }

        [Theory]
        [InlineData(101)]
        private void Check_ArticleTitle_Invalid(int titleLength)
        {
            string title = GetStringWithDesiredLength(titleLength);
            var article = new ArticleDto {Title = title};
            string[] errors;
            bool valid = article.Validate(out errors, true);
            Assert.False(valid, string.Join(";", errors));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(200)]
        private void Check_ArticleText_Valid(int textLength)
        {
            string text = GetStringWithDesiredLength(textLength);
            var article = new ArticleDto {Text = text};
            string[] errors;
            bool valid = article.Validate(out errors, true);
            Assert.True(valid);
        }

        [Theory]
        [InlineData(201)]
        private void Check_ArticleText_Invalid(int textLength)
        {
            string text = GetStringWithDesiredLength(textLength);
            var article = new ArticleDto {Text = text};
            string[] errors;
            bool valid = article.Validate(out errors, true);
            Assert.False(valid, string.Join(";", errors));
        }


        [Theory]
        [InlineData(0)]
        [InlineData(50)]
        private void Check_ArticleCommentText_Valid(int textLength)
        {
            string text = GetStringWithDesiredLength(textLength);
            var article = new ArticleDto {Comments = new List<CommentDto> {new CommentDto {Text = text}}};
            string[] errors;
            bool valid = article.Validate(out errors, true);
            Assert.True(valid, string.Join(";", errors));
        }

        [Theory]
        [InlineData(51)]
        private void Check_ArticleCommentText_Invalid(int textLength)
        {
            string text = GetStringWithDesiredLength(textLength);
            var article = new ArticleDto {Comments = new List<CommentDto> {new CommentDto {Text = text}}};
            string[] errors;
            bool valid = article.Validate(out errors, true);
            Assert.False(valid, string.Join(";", errors));
        }
    }
}