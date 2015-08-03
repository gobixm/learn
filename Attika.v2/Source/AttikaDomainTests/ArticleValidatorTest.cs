using Infotecs.Attika.AttikaDomain.Aggregates;
using Infotecs.Attika.AttikaDomain.Validators;
using Infotecs.Attika.AttikaInfrastructure.Data.Models;
using Xunit;

namespace Infotecs.Attika.AttikaDomainTests
{
    public class ArticleValidatorTest
    {
        private string GetStringWithDesiredLength(int length)
        {
            return new string('x', length);
        }

        [Theory]
        [InlineData(101)]
        private void Check_ArticleTitle_Invalid(int titleLength)
        {
            string title = GetStringWithDesiredLength(titleLength);
            var validator = new ArticleValidator();
            string[] errors;
            Assert.False(validator.Validate(Article.Create(new ArticleState {Title = title}), out errors));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(100)]
        private void Check_ArticleTitle_Valid(int titleLength)
        {
            string title = GetStringWithDesiredLength(titleLength);
            var validator = new ArticleValidator();
            string[] errors;
            Assert.True(validator.Validate(Article.Create(new ArticleState {Title = title}), out errors));
        }

        [Theory]
        [InlineData(201)]
        private void Check_ArticleText_Invalid(int textLength)
        {
            string text = GetStringWithDesiredLength(textLength);
            var validator = new ArticleValidator();
            string[] errors;
            Assert.False(validator.Validate(Article.Create(new ArticleState {Text = text}), out errors));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(200)]
        private void Check_ArticleText_Valid(int textLength)
        {
            string text = GetStringWithDesiredLength(textLength);
            var validator = new ArticleValidator();
            string[] errors;
            Assert.True(validator.Validate(Article.Create(new ArticleState {Text = text}), out errors));
        }
    }
}