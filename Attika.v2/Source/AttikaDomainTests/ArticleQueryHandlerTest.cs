using System;
using System.Collections.Generic;
using AttikaContracts.DataTransferObjects;
using AttikaContracts.Messages;
using Infotecs.Attika.AttikaDomain.Aggregates;
using Infotecs.Attika.AttikaDomain.Factories;
using Infotecs.Attika.AttikaDomain.Services.RequestProcessors;
using Infotecs.Attika.AttikaDomain.Validators;
using Infotecs.Attika.AttikaInfrastructure.Data.Models;
using Infotecs.Attika.AttikaInfrastructure.Data.Repositories;
using Infotecs.Attika.AttikaInfrastructure.Services;
using Moq;
using Xunit;

namespace Infotecs.Attika.AttikaDomainTests
{
    public class ArticleQueryHandlerTest
    {
        private readonly ArticleQueryHandler _articleQueryHandler;
        private readonly Mock<ICommandRepository> _commandRepository;
        private readonly Mock<IQueryRepository> _queryRepository;

        public ArticleQueryHandlerTest()
        {
            _queryRepository = new Mock<IQueryRepository>();
            _queryRepository.Setup(qr => qr.GetHeaders()).Returns(new List<ArticleHeaderState>());
            _queryRepository.Setup(qr => qr.GetArticle(It.IsAny<Guid>())).Returns(new ArticleState());

            _commandRepository = new Mock<ICommandRepository>();
            _commandRepository.Setup(cr => cr.CreateArticle(It.IsAny<ArticleState>()));
            _commandRepository.Setup(cr => cr.DeleteArticle(It.IsAny<string>()));

            var standardTinyMappingService = new StandardTinyMappingService();
            standardTinyMappingService.Bind<ArticleDto, ArticleState>();
            standardTinyMappingService.Bind<CommentState, CommentDto>();
            standardTinyMappingService.Bind<Article, ArticleDto>();
            var articleFactory = new ArticleFactory(_queryRepository.Object, new ArticleValidator(),
                                                    new CommentValidator(),
                                                    standardTinyMappingService);

            _articleQueryHandler = new ArticleQueryHandler(_queryRepository.Object, standardTinyMappingService,
                                                           articleFactory);
        }

        [Fact]
        private void Check_GetHeaders_CalledAfter_GetArticleHeadersHandled()
        {
            _articleQueryHandler.Get(new GetArticleHeadersRequest());
            try
            {
                _queryRepository.Verify(cr => cr.GetHeaders(), Times.AtLeastOnce());
                Assert.True(true);
            }
            catch (MockException)
            {
                Assert.False(true);
            }
        }

        [Fact]
        private void Check_GetArticle_CalledAfter_GetArticleHandled()
        {
            _articleQueryHandler.Get(new GetArticleRequest
                {
                    Id = Guid.NewGuid().ToString()
                });
            try
            {
                _queryRepository.Verify(cr => cr.GetArticle(It.IsAny<Guid>()), Times.AtLeastOnce());
                Assert.True(true);
            }
            catch (MockException)
            {
                Assert.False(true);
            }
        }
    }
}