using System;
using System.Collections.Generic;
using Infotecs.Attika.AtticaDataModel;
using Infotecs.Attika.AtticaDataModel.Repositories;
using Infotecs.Attika.AttikaService;
using Infotecs.Attika.AttikaSharedDataObjects.DataTransferObjects;
using Infotecs.Attika.AttikaSharedDataObjects.Mappings;
using Moq;
using Xunit;

namespace AtticaServiceTest
{
    public class ArticleServiceTest : IDisposable
    {
        private readonly IArticleService _service;
        private Mock<ICommandRepository> _commandRepository;
        private Mock<IMapper> _mapper;
        private Mock<IQueryRepository> _queryRepository;

        public ArticleServiceTest()
        {
            _mapper = new Mock<IMapper>();
            _mapper.Setup(m => m.Map<ArticleDto>(It.IsAny<Article>())).Returns(() => new ArticleDto());
            _mapper.Setup(m => m.Map<CommentDto>(It.IsAny<Comment>())).Returns(() => new CommentDto());
            _mapper.Setup(m => m.Map<ArticleHeaderDto>(It.IsAny<ArticleHeader>())).Returns(() => new ArticleHeaderDto());
            _mapper.Setup(m => m.Map<Article>(It.IsAny<ArticleDto>())).Returns(() => new Article());
            _mapper.Setup(m => m.Map<Comment>(It.IsAny<CommentDto>())).Returns(() => new Comment());
            _mapper.Setup(m => m.Map<ArticleHeader>(It.IsAny<ArticleHeaderDto>())).Returns(() => new ArticleHeader());

            _queryRepository = new Mock<IQueryRepository>();
            _queryRepository.Setup(qr => qr.GetHeaders()).Returns(new List<ArticleHeader>());
            _queryRepository.Setup(qr => qr.GetArticle(It.IsAny<Guid>())).Returns(new Article());

            _commandRepository = new Mock<ICommandRepository>();
            _commandRepository.Setup(cr => cr.CreateArticle(It.IsAny<Article>()));
            _commandRepository.Setup(cr => cr.CreateComment(It.IsAny<Guid>(), It.IsAny<Comment>()));
            _commandRepository.Setup(cr => cr.DeleteArticle(It.IsAny<string>()));
            _commandRepository.Setup(cr => cr.DeleteComment(It.IsAny<string>()));

            _service = new ArticleService(_commandRepository.Object, _queryRepository.Object, _mapper.Object);
        }

        public void Dispose()
        {
            _queryRepository = null;
            _commandRepository = null;
            _mapper = null;
        }

        [Fact]
        private void TestCreateArticleCalled()
        {
            _service.NewArticle(new ArticleDto());
            try
            {
                _commandRepository.Verify(cr => cr.CreateArticle(It.IsAny<Article>()), Times.AtLeastOnce());
                Assert.True(true);
            }
            catch (MockException)
            {
                Assert.False(true);
            }
        }

        [Fact]
        private void TestCreateCommentCalled()
        {
            _service.AddComment(Guid.NewGuid().ToString(), new CommentDto());
            try
            {
                _commandRepository.Verify(cr => cr.CreateComment(It.IsAny<Guid>(), It.IsAny<Comment>()),
                                          Times.AtLeastOnce());
                Assert.True(true);
            }
            catch (MockException)
            {
                Assert.False(true);
            }
        }

        [Fact]
        private void TestDeleteCommentCalled()
        {
            _service.DeleteComment("");
            try
            {
                _commandRepository.Verify(cr => cr.DeleteComment(It.IsAny<string>()), Times.AtLeastOnce());
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
            _service.DeleteArticle("");
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
            _service.GetArticleHeaders();
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
            _service.GetArticle(Guid.NewGuid().ToString());
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