using System.Collections.Generic;
using Infotecs.Attika.AttikaDomain.Aggregates;

namespace Infotecs.Attika.AttikaDomain.Validators
{
    public sealed class ArticleValidator : Validator<Article>
    {
        private const string TitleOverflowError = "Заголовок статьи не может превышать 100 символов.";
        private const int TitelOverflowRange = 100;
        private const string TextOverflowError = "Текст статьи не может превышать 200 символов.";
        private const int TextOverflowRange = 200;

        protected override IEnumerable<string> GetErrors(Article article)
        {
            if ((article.Title != null) && (article.Title.Length > TitelOverflowRange))
            {
                yield return TitleOverflowError;
            }
            if ((article.Text != null) && (article.Text.Length > TextOverflowRange))
            {
                yield return TextOverflowError;
            }
        }
    }
}