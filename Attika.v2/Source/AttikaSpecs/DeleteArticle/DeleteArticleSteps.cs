using System;
using System.Threading;
using AttikaContracts.Messages;
using Infotecs.Attika.AttikaConsoleHost.Configurations.ApplicationRoots;
using Infotecs.Attika.AttikaDomain.Services.RequestProcessors;
using Infotecs.Attika.AttikaInfrastructure.Data.Models;
using Infotecs.Attika.AttikaInfrastructure.Data.Repositories;
using NUnit.Framework;
using Ninject;
using TechTalk.SpecFlow;

namespace Infotecs.Attika.AttikaSpecs.DeleteArticle
{
    [Binding]
    public class DeleteArticleSteps
    {
        private static Guid _articleId = Guid.Parse(Metadata.ArticleId);
        private DeleteArticleRequest _deleteArticleRequest;

        [Given(@"I have article with valid id in store")]
        public void GivenIHaveArticleWithValidIdInStore()
        {
            ArticleState article = NinjectServiceLocator.Kernel.Get<StandardQueryRepository>().GetArticle(_articleId);
            if (article != null)
            {
                NinjectServiceLocator.Kernel.Get<StandardCommandRepository>().DeleteArticle(_articleId.ToString());
            }
            article = new ArticleState
            {
                Id = _articleId,
                Title = "article title",
                Text = "article text",
                Created = DateTime.Now
            };
            article.Comments.Add(new CommentState
            {
                ArticleState = article,
                Created = DateTime.Now,
                Id = Guid.Parse(Metadata.AvailableCommentId),
                Text = "commentText"
            });
            NinjectServiceLocator.Kernel.Get<StandardCommandRepository>().CreateArticle(article);
        }

        [Then(@"this article should dissapear from store")]
        public void ThenThisArticleShouldDissapearFromStore()
        {
            ArticleState article = NinjectServiceLocator.Kernel.Get<StandardQueryRepository>().GetArticle(_articleId);
            Assert.IsNull(article);
        }

        [Then(@"this service should throw no error when handles ""(delete article request with nonexisting id)""")]
        public void ThenThisServiceShouldThrowNoErrorWhenHandlesDeleteArticleRequestWithNonExistingId(string request)
        {
            NinjectServiceLocator.Kernel.Get<ArticleCommandHandler>().DeleteOneWay(_deleteArticleRequest);
        }

        [When(@"service handles ""(delete article request with valid id)""")]
        public void WhenServiceHandlesDeleteArticleRequestWithValidId(string request)
        {
            NinjectServiceLocator.Kernel.Get<ArticleCommandHandler>().DeleteOneWay(_deleteArticleRequest);
            Thread.Sleep(1000);
        }

        [When(@"this service recieved ""(delete article request with nonexisting id)""")]
        public void WhenThisServiceRecievedDeleteArticleRequestWithNonExistingId(string request)
        {
            _deleteArticleRequest = new DeleteArticleRequest { ArticleId = Guid.NewGuid().ToString() };
        }

        [When(@"this service recieved ""(delete article request with valid id)""")]
        public void WhenThisServiceRecievedDeleteArticleRequestWithValidId(string request)
        {
            _deleteArticleRequest = new DeleteArticleRequest { ArticleId = _articleId.ToString() };
        }
    }
}
