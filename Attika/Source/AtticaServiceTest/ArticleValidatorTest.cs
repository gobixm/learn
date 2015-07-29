using System.Collections.Generic;
using Infotecs.Attika.AttikaService.DataTransferObjects;
using Infotecs.Attika.AttikaService.Validators;
using Xunit;

namespace AtticaServiceTest
{
    public class ArticleValidatorTest
    {
        public static IEnumerable<object[]> ArticleValidationData
        {
            get
            {
                yield return new object[]
                    {
                        new ArticleDto {Title = "article 1"}, true, ""
                    };
                yield return new object[]
                    {
                        new ArticleDto {Title = new string('x', 100)}, true, ""
                    };
                yield return new object[]
                    {
                        new ArticleDto {Title = new string('x', 101)}, false,
                        "Заголовок статьи не может превышать 100 символов."
                    };
                yield return new object[]
                    {
                        new ArticleDto {Text = "simple text"}, true, ""
                    };
                yield return new object[]
                    {
                        new ArticleDto {Text = new string('x', 200)}, true, ""
                    };
                yield return new object[]
                    {
                        new ArticleDto {Text = new string('x', 201)}, false,
                        "Текст статьи не может превышать 200 символов."
                    };
                yield return new object[]
                    {
                        new ArticleDto {Comments = new List<CommentDto> {new CommentDto {Text = new string('x', 500)}}},
                        false, "Текст комментария не может превышать 50 символов."
                    };
            }
        }

        [Theory]
        [MemberData("ArticleValidationData", null)]
        private void TestArticleValidationRules(ArticleDto article, bool valid, string message)
        {
            string[] errors;
            bool validated = article.Validate(out errors, true);
            Assert.Equal(valid, validated);
            if (!validated)
            {
                Assert.Equal(errors[0], message);
            }
        }
    }
}