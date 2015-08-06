using System;
using System.Management.Automation;
using Infotecs.Attika.AttikaClient;

namespace Infotecs.Attika.AttikaClientPowershellModule
{
    [Cmdlet(
        VerbsCommon.Remove,
        "article",
        DefaultParameterSetName = "default")]
    public class DeleteArticleCommand : Cmdlet
    {
        private MessagedClientService _client;
        private string connectionString = "http://localhost:7878";

        [Parameter(
            Position = 0,
            Mandatory = true,
            HelpMessage = "identifier of the article you want to delete",
            ParameterSetName = "default",
            ValueFromPipelineByPropertyName = true)]
        public Guid Article { get; set; }

        public MessagedClientService Client
        {
            get { return _client ?? (_client = new MessagedClientService(ConnectionString)); }
        }

        [Parameter(Position = 1,
            HelpMessage = "address of your article publishing service",
            ParameterSetName = "default",
            ValueFromPipelineByPropertyName = true)]
        [Alias("h", "connection", "conn", "svc")]
        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }

        protected override void ProcessRecord()
        {
            try
            {
                Client.DeleteArticle(Article.ToString());
            }
            catch (DataServiceException ex)
            {
                Console.WriteLine("Не удалось добавить статью:" + ex.Message);
            }
        }
    }
}
