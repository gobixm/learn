using System;
using System.Collections.Generic;
using Infotecs.Attika.AttikaDomain.Entities;
using Infotecs.Attika.AttikaInfrastructure.Data.Models;
using Xunit;

namespace Infotecs.Attika.AttikaDomainTests
{
    public class CommentValidatorTest
    {
        public static IEnumerable<object[]> CommentValidationData
        {
            get
            {
                yield return new object[]
                {
                    new CommentState {Text = "comment"}, true, ""
                };
                yield return new object[]
                {
                    new CommentState {Text = new string('x', 50)}, true, ""
                };
                yield return new object[]
                {
                    new CommentState {Text = new string('x', 51)}, false,
                    "Текст комментария не может превышать 50 символов."
                };
            }
        }

        [Theory]
        [MemberData("CommentValidationData")]
        public void TestCommentValidationRules(CommentState comment, bool valid, string message)
        {
            try
            {
                Comment.Create(comment);
            }
            catch (ArgumentException ex)
            {
                if (valid)
                    Assert.False(true);
                else
                {
                    Assert.Contains(message, ex.Message.Split('\n'));
                }
            }
        }
    }
}