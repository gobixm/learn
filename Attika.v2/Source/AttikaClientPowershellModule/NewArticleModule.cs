using System;
using System.Management.Automation;
using AttikaContracts.DataTransferObjects;
using Infotecs.Attika.AttikaClient;

namespace Infotecs.Attika.AttikaClientPowershellModule
{
    [Cmdlet(VerbsCommon.New, "article")]
    public class NewArticleCommand : Cmdlet
    {
        private string connectionString = "http://localhost:7878";

        [Parameter(Position = 3)]
        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }

        [Parameter(Position = 2)]
        [ValidateLength(0, 200)]
        public string Text { get; set; }

        [Parameter(Position = 1)]
        [ValidateLength(0, 100)]
        public string Title { get; set; }

        protected override void ProcessRecord()
        {
            var client = new MessagedClientService(ConnectionString);

            try
            {
                var articleDto = new ArticleDto
                {
                    Id = Guid.NewGuid(),
                    Title = Title,
                    Text = Text
                };
                client.NewArticle(articleDto);
                WriteVerbose("Статья c id " + articleDto.Id + " добавлена");
            }
            catch (DataServiceException ex)
            {
                Console.WriteLine("Не удалось добавить статью:" + ex.Message);
            }
        }
    }
}
