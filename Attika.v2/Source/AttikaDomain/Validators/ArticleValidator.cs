using System.Collections.Generic;
using System.Linq;
using Infotecs.Attika.AttikaDomain.Aggregates;
using Infotecs.Attika.AttikaDomain.Validators.Contracts;

namespace Infotecs.Attika.AttikaDomain.Validators
{
    public class ArticleValidator : Validator<Article>
    {
        public const string TitleOverflowError = "Заголовок статьи не может превышать 100 символов.";
        public const int TitelOverflowRange = 100;
        public const string TextOverflowError = "Текст статьи не может превышать 200 символов.";
        public const int TextOverflowRange = 200;
        protected override IEnumerable<string> GetErrors(Article article)
        {
            if (article.Title.Length > TitelOverflowRange)
            {
                yield return TitleOverflowError;
            }
            if (article.Text.Length > TextOverflowRange)
            {
                yield return TextOverflowError;
            }
        }
    }
}