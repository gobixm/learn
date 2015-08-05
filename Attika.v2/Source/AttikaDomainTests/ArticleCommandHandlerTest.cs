using System;
using System.Collections.Generic;
using AttikaContracts.DataTransferObjects;
using AttikaContracts.Messages;
using Infotecs.Attika.AttikaDomain.Aggregates;
using Infotecs.Attika.AttikaDomain.Factories;
using Infotecs.Attika.AttikaDomain.Services.Queuing;
using Infotecs.Attika.AttikaDomain.Services.RequestProcessors;
using Infotecs.Attika.AttikaDomain.Validators;
using Infotecs.Attika.AttikaInfrastructure.Data.Models;
using Infotecs.Attika.AttikaInfrastructure.Data.Repositories;
using Infotecs.Attika.AttikaInfrastructure.Services;
using Moq;
using Xunit;

namespace Infotecs.Attika.AttikaDomainTests
{
    public class ArticleCommandHandlerTest
    {
        private readonly ArticleCommandHandler _articleCommandHandler;
        private readonly Mock<ICommandRepository> _commandRepository;
        private readonly Mock<IQueryRepository> _queryRepository;
        private readonly Mock<IQueueService> _queueService;

        public ArticleCommandHandlerTest()
        {
            _queryRepository = new Mock<IQueryRepository>();
            _queryRepository.Setup(qr => qr.GetHeaders()).Returns(new List<ArticleHeaderState>());
            _queryRepository.Setup(qr => qr.GetArticle(It.IsAny<Guid>())).Returns(new ArticleState());

            _commandRepository = new Mock<ICommandRepository>();
            _commandRepository.Setup(cr => cr.CreateArticle(It.IsAny<ArticleState>()));
            _commandRepository.Setup(cr => cr.DeleteArticle(It.IsAny<string>()));

            _queueService = new Mock<IQueueService>();
            _queueService.Setup(qs => qs.PushMessage(It.IsAny<object>()));

            var standardTinyMappingService = new StandardTinyMappingService();
            standardTinyMappingService.Bind<ArticleDto, ArticleState>();
            standardTinyMappingService.Bind<CommentState, CommentDto>();
            standardTinyMappingService.Bind<Article, ArticleDto>();
            var commentFactory = new CommentFactory(new CommentValidator(), standardTinyMappingService);
            var articleFactory = new ArticleFactory(_queryRepository.Object, new ArticleValidator(),
                                                    new CommentValidator(),
                                                    standardTinyMappingService);
            _articleCommandHandler = new ArticleCommandHandler(_queueService.Object,
                                                               articleFactory, commentFactory);
        }

        [Fact]
        private void Check_CreateArticle_QueuedAfter_NewArtilceHandled()
        {
            var request = new NewArticleRequest
                {
                    Article = new ArticleDto()
                };
            _articleCommandHandler.PostOneWay(request);
            try
            {
                _queueService.Verify(qs => qs.PushMessage(It.Is((object r) => r == request)), Times.AtLeastOnce());
                Assert.True(true);
            }
            catch (MockException)
            {
                Assert.False(true);
            }
        }

        [Fact]
        private void Check_DeleteArticle_QueuedAfter_DeleteArticleHandled()
        {
            var request = new DeleteArticleRequest {ArticleId = Guid.NewGuid().ToString()};
            _articleCommandHandler.DeleteOneWay(request);
            try
            {
                _queueService.Verify(qs => qs.PushMessage(It.Is((object r) => r == request)), Times.AtLeastOnce());
                Assert.True(true);
            }
            catch (MockException)
            {
                Assert.False(true);
            }
        }

        [Fact]
        private void Check_UpdateArticle_Queued_AddArticleCommentHandled()
        {
            var request = new AddArticleCommentRequest
                {
                    ArticleId = Guid.NewGuid().ToString(),
                    Comment = new CommentDto {ArticleId = Guid.NewGuid(), Id = Guid.NewGuid()}
                };
            _articleCommandHandler.PostOneWay(request);
            try
            {
                _queueService.Verify(qs => qs.PushMessage(It.Is((object r) => r == request)), Times.AtLeastOnce());
                Assert.True(true);
            }
            catch (MockException)
            {
                Assert.False(true);
            }
        }
    }
}