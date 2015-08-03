using System;
using System.Collections.Generic;
using Infotecs.Attika.AttikaDomain.Aggregates;
using Infotecs.Attika.AttikaDomain.Factories;
using Infotecs.Attika.AttikaDomain.Services.RequestProcessors;
using Infotecs.Attika.AttikaDomain.Validators;
using Infotecs.Attika.AttikaInfrastructure.Data.DataTransferObjects;
using Infotecs.Attika.AttikaInfrastructure.Data.Models;
using Infotecs.Attika.AttikaInfrastructure.Data.Repositories;
using Infotecs.Attika.AttikaInfrastructure.Messaging.Messages;
using Infotecs.Attika.AttikaInfrastructure.Messaging.Serializers;
using Infotecs.Attika.AttikaInfrastructure.Services;
using Moq;
using Xunit;

namespace Infotecs.Attika.AttikaDomainTests
{
    public class ArticleHandlerTest : IDisposable
    {
        private readonly ArticleHandler _articleHandler;
        private readonly Mock<ICommandRepository> _commandRepository;
        private readonly Mock<IQueryRepository> _queryRepository;

        public ArticleHandlerTest()
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
            var commentFactory = new CommentFactory(new CommentValidator(), standardTinyMappingService);
            var articleFactory = new ArticleFactory(_queryRepository.Object, new ArticleValidator(),
                                                    new CommentValidator(),
                                                    standardTinyMappingService);

            _articleHandler = new ArticleHandler(_queryRepository.Object, _commandRepository.Object,
                                                 standardTinyMappingService, null, articleFactory, commentFactory,
                                                 new MessageSerializationService());
        }

        public void Dispose()
        {
        }

        [Fact]
        private void TestCreateArticleCalled()
        {
            _articleHandler.Handle(new NewArticleRequest
                {
                    Article = new ArticleDto(),
                    Request = "Article.NewArticleRequest"
                });
            try
            {
                _commandRepository.Verify(cr => cr.CreateArticle(It.IsAny<ArticleState>()), Times.AtLeastOnce());
                Assert.True(true);
            }
            catch (MockException)
            {
                Assert.False(true);
            }
        }

        [Fact]
        private void TestDeleteArticleCalled()
        {
            try
            {
                _commandRepository.Verify(cr => cr.DeleteArticle(It.IsAny<string>()), Times.AtLeastOnce());
                Assert.True(true);
            }
            catch (MockException)
            {
                Assert.False(true);
            }
        }

        [Fact]
        private void TestGetArticleHeadersCalled()
        {
            _articleHandler.Handle(new GetArticleHeadersRequest
                {
                    Request = "Article.GetArticleHeadersRequest"
                });
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
        private void TestGetArticleCalled()
        {
            _articleHandler.Handle(new GetArticleRequest
                {
                    Request = "Article.GetArticleRequest",
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

        [Fact]
        private void TestUpdateArticleCalledAfterAddCommentHandled()
        {
            _articleHandler.Handle(new AddArticleCommentRequest
                {
                    Request = "Article.AddArticleCommentRequest",
                    ArticleId = Guid.NewGuid().ToString(),
                    Comment = new CommentDto {ArticleId = Guid.NewGuid(), Id = Guid.NewGuid()}
                });
            try
            {
                _commandRepository.Verify(cr => cr.UpdateArticle(It.IsAny<ArticleState>()), Times.AtLeastOnce());
                Assert.True(true);
            }
            catch (MockException)
            {
                Assert.False(true);
            }
        }
    }
}