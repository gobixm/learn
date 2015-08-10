using System;
using System.Configuration;
using AttikaContracts.DataTransferObjects;
using Infotecs.Attika.AttikaClient;
using fit;

namespace Infotecs.Attika.AttikaFitnesse
{
    public class AddCommentFixture : ColumnFixture
    {
        public string ArticleId { get; set; }
        public string Id { get; set; }
        public string Text { get; set; }

        private bool AddArticleComment()
        {
            if (string.IsNullOrEmpty(ArticleId))
            {
                ArticleId = Args[0];
            }
            using (var service = new MessagedClientService(ConfigurationManager.ConnectionStrings["host"].ConnectionString))
            {
                try
                {
                    service.NewComment(ArticleId, new CommentDto
                    {
                        ArticleId = Guid.Parse(ArticleId),
                        Id = Guid.Parse(Id),
                        Text = Text
                    });
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
