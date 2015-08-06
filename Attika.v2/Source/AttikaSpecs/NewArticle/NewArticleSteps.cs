using System;
using System.ServiceModel.Web;
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

namespace Infotecs.Attika.AttikaSpecs.NewArticle
{
    [Binding]
    public class NewArticleSteps
    {
        private ArticleCommandHandler _articleCommandHandler;
        private NewArticleRequest _newArticleRequest;

        [Given(@"I got working article service")]
        public void GivenIGotWorkingArticleService()
        {
            _articleCommandHandler = NinjectServiceLocator.Kernel.Get<ArticleCommandHandler>();
        }

        [Then(@"desired article appeared in store")]
        public void ThenNewArticleAppearedInStore()
        {
            var standardQueryRepository = NinjectServiceLocator.Kernel.Get<StandardQueryRepository>();
            ArticleState article = standardQueryRepository.GetArticle(_newArticleRequest.Article.Id);
            Assert.AreEqual(article.Id, _newArticleRequest.Article.Id);
            Assert.AreEqual(article.Title, _newArticleRequest.Article.Title);
            Assert.AreEqual(article.Text, _newArticleRequest.Article.Text);
        }

        [Then(@"service fails to handle ""(invalid new article request)""")]
        public void ThenServiceFailsToHandleInvalidNewArticleRequest(string request)
        {
            Assert.Throws<WebFaultException>(() => _articleCommandHandler.PostOneWay(_newArticleRequest));
        }

        [When(@"service handles ""(valid new article request)""")]
        public void WhenServiceHandlesValidNewArticleRequest(string request)
        {
            _articleCommandHandler.PostOneWay(_newArticleRequest);

            Thread.Sleep(1000);
        }

        [When(@"this service recieved ""(invalid new article request)""")]
        public void WhenThisServiceRecievedIvalidNewArticleRequest(string request)
        {
            _newArticleRequest = new NewArticleRequest();
        }

        [When(@"this service recieved ""(long text request)""")]
        public void WhenThisServiceRecievedLongTextRequest(string request)
        {
            _newArticleRequest = new NewArticleRequest
            {
                Article = new ArticleDto
                {
                    Created = DateTime.Now,
                    Id = Guid.NewGuid(),
                    Text = new string('x', 201)
                }
            };
        }

        [When(@"this service recieved ""(long title request)""")]
        public void WhenThisServiceRecievedLongTitleRequest(string request)
        {
            _newArticleRequest = new NewArticleRequest
            {
                Article = new ArticleDto
                {
                    Created = DateTime.Now,
                    Id = Guid.NewGuid(),
                    Title = new string('x', 101)
                }
            };
        }

        [When(@"this service recieved ""(valid new article request)""")]
        public void WhenThisServiceRecievedValidNewArticleRequest(string request)
        {
            _newArticleRequest = new NewArticleRequest
            {
                Article = new ArticleDto
                {
                    Created = DateTime.Now,
                    Id = Guid.NewGuid(),
                    Text = "article text",
                    Title = "article title"
                }
            };
        }
    }
}
