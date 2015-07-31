using System.Collections.Generic;
using Infotecs.Attika.AttikaService.Validators;
using Infotecs.Attika.AttikaSharedDataObjects.DataTransferObjects;
using Xunit;

namespace AtticaServiceTest
{
    public class CommentValidatorTest
    {
        public static IEnumerable<object[]> CommentValidationData
        {
            get
            {
                yield return new object[]
                    {
                        new CommentDto {Text = "comment"}, true, ""
                    };
                yield return new object[]
                    {
                        new CommentDto {Text = new string('x', 50)}, true, ""
                    };
                yield return new object[]
                    {
                        new CommentDto {Text = new string('x', 51)}, false,
                        "Текст комментария не может превышать 50 символов."
                    };
            }
        }

        [Theory]
        [MemberData("CommentValidationData")]
        public void TestCommentValidationRules(CommentDto comment, bool valid, string message)
        {
            string[] errors;
            bool validated = comment.Validate(out errors);
            Assert.Equal(valid, validated);
            if (!validated)
            {
                Assert.Equal(message, errors[0]);
            }
        }
    }
}