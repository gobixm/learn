using System;
using System.Configuration;
using Infotecs.Attika.AttikaClient;
using fit;

namespace Infotecs.Attika.AttikaFitnesse
{
    public class DeleteCommentFixture : ColumnFixture
    {
        public string ArticleId { get; set; }
        public string Id { get; set; }

        private bool DeleteArticleComment()
        {
            using (var service = new MessagedClientService(ConfigurationManager.ConnectionStrings["host"].ConnectionString))
            {
                try
                {
                    service.DeleteComment(ArticleId, Id);
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
