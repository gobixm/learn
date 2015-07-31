using System.Collections.Generic;
using Infotecs.Attika.AttikaSharedDataObjects.DataTransferObjects;

namespace Infotecs.Attika.AttikaService.Validators
{
    public static class CommentValidator
    {
        private const int MaxCommentTextLength = 50;

        public static bool Validate(this CommentDto comment, out string[] validationErrors)
        {
            var errors = new List<string>();
            if ((comment.Text != null) && (comment.Text.Length > MaxCommentTextLength))
            {
                errors.Add(string.Format("Текст комментария не может превышать {0} символов.", MaxCommentTextLength));
            }
            validationErrors = errors.ToArray();
            return validationErrors.Length == 0;
        }
    }
}