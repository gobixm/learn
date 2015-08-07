using System;
using System.Configuration;
using Infotecs.Attika.AttikaClient;
using fit;

namespace Infotecs.Attika.AttikaFitnesse
{
    public class DeleteArticleFixture : ColumnFixture
    {
        public string Id { get; set; }

        public bool DeleteArticle()
        {
            var service = new MessagedClientService(ConfigurationManager.ConnectionStrings["host"].ConnectionString);
            try
            {
                service.DeleteArticle(Id);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
