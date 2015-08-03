using System;
using System.Collections.Generic;
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
        public void FailValidateCommentWithLongText(int textLength)
        {
            var text = new string('x', textLength);
            var validator = new CommentValidator();
            string[] errors;
            Assert.False(validator.Validate(Comment.Create(new CommentState { Text = text }), out errors));
        }
    }
}