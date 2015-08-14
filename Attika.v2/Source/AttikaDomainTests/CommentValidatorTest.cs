using System;
using Infotecs.Attika.AttikaDomain.Entities;
using Infotecs.Attika.AttikaDomain.Validators;
using Infotecs.Attika.AttikaInfrastructure.Data.Models;
using Xunit;

namespace Infotecs.Attika.AttikaDomainTests
{
    public class CommentValidatorTest
    {
        [Theory]
        [InlineData(51)]
        public void Check_CommentTitle_Invalid(int textLength)
        {
            string text = GetStringWithDesiredLength(textLength);
            var validator = new CommentValidator();
            string[] errors;
            Assert.False(validator.Validate(Comment.Create(new CommentState { Text = text }), out errors));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(51)]
        public void Check_CommentTitle_Valid(int textLength)
        {
            string text = GetStringWithDesiredLength(textLength);
            var validator = new CommentValidator();
            string[] errors;
            Assert.True(validator.Validate(Comment.Create(new CommentState { Text = text }), out errors));
        }

        private string GetStringWithDesiredLength(int length)
        {
            return new string('x', length);
        }
    }
}
