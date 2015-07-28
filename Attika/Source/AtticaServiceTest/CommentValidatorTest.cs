using System;
using Infotecs.Attika.AttikaService.DTO;
using Infotecs.Attika.AttikaService.Validators;
using Xunit;

namespace AtticaServiceTest
{
    public class CommentValidatorTest
    {
        private CommentDto CreateValidComment()
        {
            return new CommentDto
                {
                    ArticleId = Guid.NewGuid(),
                    Created = DateTime.Today,
                    Id = Guid.Parse("FA9A3291-E597-4E4F-BBC9-C7E8D347229B"),
                    Text = "some text"
                };
        }

        [Fact]
        public void AssumeCommentTextMoreThanFifty()
        {
            CommentDto comment = CreateValidComment();
            comment.Text = "".PadLeft(51);
            string[] errors;
            Assert.False(comment.Validate(out errors));
        }

        [Fact]
        public void AssumeCommentTextMoreThanFiftyErrorReturned()
        {
            CommentDto comment = CreateValidComment();
            comment.Text = "".PadLeft(51);
            string[] errors;
            comment.Validate(out errors);
            Assert.Equal("Текст комментария не может превышать 50 символов.", errors[0].Trim());
        }

        [Fact]
        public void AssumeCommentTextLengthEqualFifty()
        {
            CommentDto comment = CreateValidComment();
            comment.Text = "".PadLeft(50);
            string[] errors;
            Assert.True(comment.Validate(out errors));
        }

        [Fact]
        public void AssumeCommentTextLessThanFifty()
        {
            CommentDto comment = CreateValidComment();
            comment.Text = "".PadLeft(49);
            string[] errors;
            Assert.True(comment.Validate(out errors));
        }
    }
}