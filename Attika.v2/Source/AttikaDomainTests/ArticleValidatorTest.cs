using System.Collections.Generic;
using System.Collections.ObjectModel;
using Infotecs.Attika.AttikaDomain.Aggregates;
using Infotecs.Attika.AttikaDomain.Entities;
using Infotecs.Attika.AttikaDomain.Validators;
using Infotecs.Attika.AttikaInfrastructure.Data.Models;
using Xunit;

namespace Infotecs.Attika.AttikaDomainTests
{
    public class ArticleValidatorTest
    {
        public static IEnumerable<object[]> ArticleValidationData
        {
            get
            {
                yield return new object[]
                    {
                        new ArticleState {Title = "article 1"}, true, ""
                    };
                yield return new object[]
                    {
                        new ArticleState {Title = new string('x', 100)}, true, ""
                    };
                yield return new object[]
                    {
                        new ArticleState {Title = new string('x', 101)}, false,
                        "Заголовок статьи не может превышать 100 символов."
                    };
                yield return new object[]
                    {
                        new ArticleState {Text = "simple text"}, true, ""
                    };
                yield return new object[]
                    {
                        new ArticleState {Text = new string('x', 200)}, true, ""
                    };
                yield return new object[]
                    {
                        new ArticleState {Text = new string('x', 201)}, false,
                        "Текст статьи не может превышать 200 символов."
                    };
                yield return new object[]
                    {
                        new ArticleState
                            {
                                Comments = new List<CommentState> {new CommentState {Text = new string('x', 500)}}
                            },
                        false, "Текст комментария не может превышать 50 символов."
                    };
            }
        }

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