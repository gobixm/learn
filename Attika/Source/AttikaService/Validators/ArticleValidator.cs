using System.Collections.Generic;
using Infotecs.Attika.AttikaService.DTO;

namespace Infotecs.Attika.AttikaService.Validators
{
    public static class ArticleValidator
    {
        private const int MaxArticleTitleLength = 100;
        private const int MaxArticleTextLength = 200;

        public static bool Validate(this ArticleDto article, out string[] validationErrors,
                                    bool validateComments = false)
        {
            var errors = new List<string>();
            if ((article.Title != null) && (article.Title.Length > MaxArticleTitleLength))
            {
                errors.Add(string.Format("Заголовок статьи не может превышать {0} символов.", MaxArticleTitleLength));
            }
            if ((article.Text != null) && (article.Text.Length > MaxArticleTextLength))
            {
                errors.Add(string.Format("Текст статьи не может превышать {0} символов.", MaxArticleTextLength));
            }
            if (validateComments && (article.Comments != null))
            {
                foreach (CommentDto comment in article.Comments)
                {
                    string[] commentErrors;
                    if (!comment.Validate(out commentErrors))
                    {
                        errors.AddRange(commentErrors);
                    }
                }
            }

            validationErrors = errors.ToArray();
            return validationErrors.Length == 0;
        }
    }
}