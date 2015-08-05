using System;
using System.Collections.Generic;
using Infotecs.Attika.AttikaDomain.Entities;

namespace Infotecs.Attika.AttikaDomain.Validators
{
    public class CommentValidator : Validator<Comment>
    {
        public const string TextOverflowError = "Текст комментария не может превышать 50 символов.";
        public const int TextOverflowRange = 50;

        protected override IEnumerable<string> GetErrors(Comment comment)
        {
            if ((comment.Text != null) && (comment.Text.Length > TextOverflowRange))
            {
                yield return TextOverflowError;
            }
        }
    }
}
