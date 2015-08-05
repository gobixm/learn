using System;
using AttikaContracts.Messages;
using Infotecs.Attika.AttikaConsoleHost.Configurations.ApplicationRoots;
using Infotecs.Attika.AttikaDomain.Services.RequestProcessors;
using NUnit.Framework;
using Ninject;
using TechTalk.SpecFlow;

namespace Infotecs.Attika.AttikaSpecs
{
    [Binding]
    public class GetArticleFeatureSteps
    {
        private GetArticleRequest _getArticleRequest;
        private GetArticleResponse _response;

        [Then(@"i can see article text and title")]
        public void ThenICanSeeArticleTextAndTitle()
        {
            Assert.IsNotNull(_response);
            Assert.IsNotNull(_response.Article);
            Assert.IsNotNullOrEmpty(_response.Article.Text);
            Assert.IsNotNullOrEmpty(_response.Article.Title);
            Assert.Greater(_response.Article.Comments.Count, 0);
        }

        [When(@"service handles ""(get article request)""")]
        public void WhenServiceHandles(string request)
        {
            _response = (GetArticleResponse)NinjectServiceLocator.Kernel.Get<ArticleQueryHandler>().Get(_getArticleRequest);
        }

        [When(@"this service recieved ""(get article request)""")]
        public void WhenThisServiceRecieved(string request)
        {
            _getArticleRequest = new GetArticleRequest
            {
                Id = Metadata.ArticleId
            };
        }
    }
}
