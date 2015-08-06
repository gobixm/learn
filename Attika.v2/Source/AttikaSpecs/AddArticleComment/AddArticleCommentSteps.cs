using System;
using System.Linq;
using System.Threading;
using AttikaContracts.DataTransferObjects;
using AttikaContracts.Messages;
using Infotecs.Attika.AttikaConsoleHost.Configurations.ApplicationRoots;
using Infotecs.Attika.AttikaDomain.Services.RequestProcessors;
using Infotecs.Attika.AttikaInfrastructure.Data.Models;
using Infotecs.Attika.AttikaInfrastructure.Data.Repositories;
using NUnit.Framework;
using Ninject;
using TechTalk.SpecFlow;

namespace Infotecs.Attika.AttikaSpecs.AddArticleComment
{
    [Binding]
    public class AddArticleCommentSteps
    {
        private const string ValidText = "valid text";
        private AddArticleCommentRequest _addArticleCommentRequest;

        [Then(@"this comment should be seen in valid article")]
        public void ThenThisCommentShouldBeSeenInValidArticle()
        {
            Guid id = Guid.Parse(Metadata.ArticleId);
            ArticleState article =
                NinjectServiceLocator.Kernel.Get<StandardQueryRepository>().GetArticle(id);
            Assert.NotNull(article);
            CommentState comment = article.Comments.FirstOrDefault(c => c.Id == Guid.Parse(Metadata.ValidCommentId));
            Assert.NotNull(comment);
            Assert.AreEqual(ValidText, comment.Text);
        }

        [Then(@"this comment should not be stored")]
        public void ThenThisCommentShouldNotBeStored()
        {
            Guid id = Guid.Parse(Metadata.ArticleId);
            ArticleState article =
                NinjectServiceLocator.Kernel.Get<StandardQueryRepository>().GetArticle(id);
            Assert.NotNull(article);
            CommentState comment = article.Comments.FirstOrDefault(c => c.Id == Guid.Parse(Metadata.InvalidCommentId));
            Assert.Null(comment);
        }

        [When(@"service handles ""(addcomment request with invalid text)""")]
        public void WhenServiceHandlesAddCommentRequestWithInvalidText(string request)
        {
            try
            {
                NinjectServiceLocator.Kernel.Get<ArticleCommandHandler>().PostOneWay(_addArticleCommentRequest);
            }
            catch (Exception)
            {
            }
            Thread.Sleep(1000);
        }

        [When(@"service handles ""(addcomment request with valid id)""")]
        public void WhenServiceHandlesAddcommentRequestWithValidId(string request)
        {
            NinjectServiceLocator.Kernel.Get<ArticleCommandHandler>().PostOneWay(_addArticleCommentRequest);
            Thread.Sleep(1000);
        }

        [When(@"this service recieved ""(addcomment request with invalid text)""")]
        public void WhenThisServiceRecievedAddCommentRequestWithInvalidText(string request)
        {
            _addArticleCommentRequest = new AddArticleCommentRequest
            {
                ArticleId = Metadata.ArticleId,
                Comment = new CommentDto
                {
                    ArticleId = Guid.Parse(Metadata.ArticleId),
                    Created = DateTime.Now,
                    Id = Guid.Parse(Metadata.InvalidCommentId),
                    Text =
                        "innnnnnnnnnnnnnnnnnnvvvvvvvvvvvvvvvvvvvaaaaaaaaaaaaaaaaaaaaaaaalllllllllllllllllliiiiiiiiiiiiiiiiiddddddddddd"
                }
            };
        }

        [When(@"this service recieved ""(addcomment request with valid id)""")]
        public void WhenThisServiceRecievedAddcommentRequestWithValidId(string request)
        {
            _addArticleCommentRequest = new AddArticleCommentRequest
            {
                ArticleId = Metadata.ArticleId,
                Comment = new CommentDto
                {
                    ArticleId = Guid.Parse(Metadata.ArticleId),
                    Created = DateTime.Now,
                    Id = Guid.Parse(Metadata.ValidCommentId),
                    Text = ValidText
                }
            };
        }
    }
}
