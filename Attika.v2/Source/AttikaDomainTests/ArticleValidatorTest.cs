using Infotecs.Attika.AttikaDomain.Aggregates;
using Infotecs.Attika.AttikaDomain.Validators;
using Infotecs.Attika.AttikaInfrastructure.Data.Models;
using Xunit;

namespace Infotecs.Attika.AttikaDomainTests
{
    public class ArticleValidatorTest
    {
        [Theory]
        [InlineData(101)]
        private void FailValidateArticleWithLongTitle(int titleLength)
        {
            var title = new string('x', titleLength);
            var validator = new ArticleValidator();
            string[] errors;
            Assert.False(validator.Validate(Article.Create(new ArticleState {Title = title}), out errors));
        }

        [Theory]
        [InlineData(201)]
        private void FailValidateArticleWithLongText(int textLength)
        {
            var text = new string('x', textLength);
            var validator = new ArticleValidator();
            string[] errors;
            Assert.False(validator.Validate(Article.Create(new ArticleState {Text = text}), out errors));
        }
    }
}