using Infotecs.Attika.AttikaService.Validators;
using Infotecs.Attika.AttikaSharedDataObjects.DataTransferObjects;
using Xunit;

namespace AtticaServiceTest
{
    public class CommentValidatorTest
    {
        private string GetStringWithDesiredLength(int length)
        {
            return new string('x', length);
        }

        [Theory]
        [InlineData(51)]
        public void Check_CommentText_Invalid(int commentLength)
        {
            string text = GetStringWithDesiredLength(commentLength);
            var comment = new CommentDto {Text = text};
            string[] errors;
            bool valid = comment.Validate(out errors);
            Assert.False(valid, string.Join(";", errors));
        }

        [Theory]
        [InlineData(50)]
        [InlineData(0)]
        public void Check_CommentText_Valid(int commentLength)
        {
            string text = GetStringWithDesiredLength(commentLength);
            var comment = new CommentDto {Text = text};
            string[] errors;
            bool valid = comment.Validate(out errors);
            Assert.True(valid, string.Join(";", errors));
        }
    }
}