using System;
using System.Configuration;
using AttikaContracts.DataTransferObjects;
using Infotecs.Attika.AttikaClient;
using fit;

namespace Infotecs.Attika.AttikaFitnesse
{
    public class NewArticleFixture : ColumnFixture
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }

        public bool NewArticle()
        {
            using (var service = new MessagedClientService(ConfigurationManager.ConnectionStrings["host"].ConnectionString))
            {
                try
                {
                    service.NewArticle(new ArticleDto
                    {
                        Id = Guid.Parse(Id),
                        Title = Title,
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
