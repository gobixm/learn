using Infotecs.Attika.AttikaService.Validators;
using Infotecs.Attika.AttikaSharedDataObjects.DataTransferObjects;
using Xunit;

namespace AtticaServiceTest
{
    public class CommentValidatorTest
    {
        [Theory]
        [InlineData(51)]
        public void TestCommentValidationRules(int commentLength)
        {
            var text = new string('x', commentLength);
            var comment = new CommentDto {Text = text};
            string[] errors;
            bool valid = comment.Validate(out errors);
            Assert.False(valid, string.Join(";", errors));
        }
    }
}