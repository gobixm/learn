using System;
using System.Management.Automation;
using Infotecs.Attika.AttikaClient;

namespace Infotecs.Attika.AttikaClientPowershellModule
{
    [Cmdlet(
        VerbsCommon.Remove,
        "comment",
        DefaultParameterSetName = "default")]
    public class DeleteArticleCommentCommand : Cmdlet
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

        [Parameter(
            Position = 1,
            Mandatory = true,
            HelpMessage = "identifier of the comment you want to delete",
            ParameterSetName = "default",
            ValueFromPipelineByPropertyName = true)]
        public Guid Comment { get; set; }

        [Parameter(Position = 2,
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
                Client.DeleteComment(Article.ToString(), Comment.ToString());
            }
            catch (DataServiceException ex)
            {
                Console.WriteLine("Не удалось добавить статью:" + ex.Message);
            }
        }
    }
}
