using System;
using System.Linq;
using System.Threading;
using AttikaContracts.Messages;
using Infotecs.Attika.AttikaConsoleHost.Configurations.ApplicationRoots;
using Infotecs.Attika.AttikaDomain.Services.RequestProcessors;
using Infotecs.Attika.AttikaInfrastructure.Data.Models;
using Infotecs.Attika.AttikaInfrastructure.Data.Repositories;
using NUnit.Framework;
using Ninject;
using TechTalk.SpecFlow;

namespace Infotecs.Attika.AttikaSpecs.DeleteArticleComment
{
    [Binding]
    public class DeleteArticleCommentSteps
    {
        private DeleteArticleCommentRequest _deleteArticleCommentRequest;

        [Then(@"this comment should dissaper from article scope")]
        public void ThenThisCommentShouldDissaperFromArticleScope()
        {
            Guid commentId = Guid.Parse(Metadata.AvailableCommentId);
            ArticleState article =
                NinjectServiceLocator.Kernel.Get<StandardQueryRepository>().GetArticle(Guid.Parse(Metadata.ArticleId));
            CommentState comment = article.Comments.FirstOrDefault(c => c.Id == commentId);
            Assert.IsNull(comment);
        }

        [When(@"service handles ""(delete article comment request)""")]
        public void WhenServiceHandlesDeleteArticleCommentRequest(string request)
        {
            NinjectServiceLocator.Kernel.Get<ArticleCommandHandler>().DeleteOneWay(_deleteArticleCommentRequest);
            Thread.Sleep(1000);
        }

        [When(@"this service recieved ""(delete article comment request)""")]
        public void WhenThisServiceRecievedDeleteArticleCommentRequest(string request)
        {
            _deleteArticleCommentRequest = new DeleteArticleCommentRequest
            {
                ArticleId = Metadata.ArticleId,
                CommentId = Metadata.AvailableCommentId
            };
        }
    }
}
