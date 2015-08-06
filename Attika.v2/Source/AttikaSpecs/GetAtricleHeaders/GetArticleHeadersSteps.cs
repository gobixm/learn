using System;
using System.Linq;
using AttikaContracts.DataTransferObjects;
using AttikaContracts.Messages;
using Infotecs.Attika.AttikaConsoleHost.Configurations.ApplicationRoots;
using Infotecs.Attika.AttikaDomain.Services.RequestProcessors;
using NUnit.Framework;
using Ninject;
using TechTalk.SpecFlow;

namespace Infotecs.Attika.AttikaSpecs.GetAtricleHeaders
{
    [Binding]
    public class GetArticleHeadersSteps
    {
        private GetArticleHeadersRequest _getArticleHeadersRequest;
        private GetArticleHeadersResponse _getArticleHeadersResponse;

        [Then(@"i can see article header in ""(get article response)""")]
        public void ThenICanSeeArticleHeaderInGetArticleResponse(string response)
        {
            Guid articleGuid = Guid.Parse(Metadata.ArticleId);
            ArticleHeaderDto header = _getArticleHeadersResponse.Headers.FirstOrDefault(a => a.ArticleId == articleGuid);
            Assert.IsNotNull(header);
        }

        [When(@"service handles ""(get article headers request)""")]
        public void WhenServiceHandlesGerArticleHeadersRequest(string request)
        {
            _getArticleHeadersResponse = (GetArticleHeadersResponse)NinjectServiceLocator.Kernel.Get<ArticleQueryHandler>().Get(_getArticleHeadersRequest);
        }

        [When(@"this service recieved ""(get article headers request)""")]
        public void WhenThisServiceRecievedGetArticleHeadersRequest(string request)
        {
            _getArticleHeadersRequest = new GetArticleHeadersRequest();
        }
    }
}
