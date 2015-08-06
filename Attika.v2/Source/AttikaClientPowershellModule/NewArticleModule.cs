using System;
using System.Management.Automation;
using AttikaContracts.DataTransferObjects;
using Infotecs.Attika.AttikaClient;

namespace Infotecs.Attika.AttikaClientPowershellModule
{
    [Cmdlet(
        VerbsCommon.New,
        "article",
        DefaultParameterSetName = "default")]
    public class NewArticleCommand : Cmdlet
    {
        private MessagedClientService _client;
        private string connectionString = "http://localhost:7878";

        public MessagedClientService Client
        {
            get { return _client ?? (_client = new MessagedClientService(ConnectionString)); }
        }

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

        [Parameter(Position = 1,
            HelpMessage = "content of your article",
            ParameterSetName = "default",
            ValueFromPipelineByPropertyName = true)]
        [ValidateLength(0, 200)]
        [Alias("content")]
        public string Text { get; set; }

        [Parameter(
            Position = 0,
            HelpMessage = "title of your article",
            ParameterSetName = "default",
            ValueFromPipelineByPropertyName = true)]
        [ValidateLength(0, 100)]
        [Alias("t")]
        public string Title { get; set; }

        protected override void ProcessRecord()
        {
            try
            {
                var articleDto = new ArticleDto
                {
                    Id = Guid.NewGuid(),
                    Title = Title,
                    Text = Text
                };
                Client.NewArticle(articleDto);
                WriteVerbose("Статья c id " + articleDto.Id + " добавлена");
                WriteObject(new { Article = articleDto.Id });
            }
            catch (DataServiceException ex)
            {
                Console.WriteLine("Не удалось добавить статью:" + ex.Message);
            }
        }
    }
}
