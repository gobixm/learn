using System;
using System.Management.Automation;
using AttikaContracts.DataTransferObjects;
using Infotecs.Attika.AttikaClient;

namespace Infotecs.Attika.AttikaClientPowershellModule
{
    [Cmdlet(VerbsCommon.Add, "comment")]
    public class AddArticleComment : Cmdlet
    {
        private string connectionString = "http://localhost:7878";

        [Parameter(Position = 1, Mandatory = true)]
        public Guid Article { get; set; }

        [Parameter(Position = 3)]
        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }

        [Parameter(Position = 2)]
        public string Text { get; set; }

        protected override void ProcessRecord()
        {
            var client = new MessagedClientService(ConnectionString);

            try
            {
                var comment = new CommentDto
                {
                    Id = Guid.NewGuid(),
                    ArticleId = Article,
                    Text = Text
                };
                client.NewComment(Article.ToString(), comment);
                WriteVerbose("Комментарий c id " + comment.Id + " добавлен");
            }
            catch (DataServiceException ex)
            {
                Console.WriteLine("Не удалось добавить комментарий:" + ex.Message);
            }
        }
    }
}
