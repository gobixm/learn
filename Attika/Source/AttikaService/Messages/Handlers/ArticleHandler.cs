using System;
using Infotecs.Attika.AtticaDataModel;
using Infotecs.Attika.AtticaDataModel.Repositories;
using Infotecs.Attika.AttikaService.DataTransferObjects;
using Infotecs.Attika.AttikaService.Mappings;

namespace Infotecs.Attika.AttikaService.Messages.Handlers
{
    public sealed class ArticleHandler : BaseHandler
    {
        private readonly IMapper _mapper;
        private readonly IQueryRepository _queryRepository;
        private ICommandRepository _commandRepository;

        public ArticleHandler(IQueryRepository queryRepository, ICommandRepository commandRepository, IMapper mapper)
        {
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
            _mapper = mapper;
        }

        public BaseMessage Handle(GetArticleRequest getArticleRequest)
        {
            Article article;
            try
            {
                article = _queryRepository.GetArticle(Guid.Parse(getArticleRequest.Id));
            }
            catch (Exception ex)
            {
                return new FaultMessage("Ошибка при получении статьи из репозитория", ex.Message);
            }
            if (article == null)
            {
                return new FaultMessage("Ошибка при получении статьи",
                                        "Статьи с ID=" + getArticleRequest.Id + " не существует");
            }
            try
            {
                return new GetArticleResponse {Article = _mapper.Map<ArticleDto>(article)};
            }
            catch (Exception ex)
            {
                return new FaultMessage("Ошибка при маппинге статьи", ex.Message);
            }
        }
    }
}