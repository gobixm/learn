using Infotecs.Attika.AtticaDataModel;
using Infotecs.Attika.AtticaDataModel.Repositories;
using Infotecs.Attika.AttikaSharedDataObjects.Mappings;
using Infotecs.Attika.AttikaSharedDataObjects.Messages;

namespace Infotecs.Attika.AttikaQueueProcessor.Processors
{
    public sealed class ArticleProcessor : BaseProcessor
    {
        private readonly ICommandRepository _commandRepository;
        private readonly IMapper _mapper;

        public ArticleProcessor(ICommandRepository commandRepository, IMapper mapper)
        {
            _commandRepository = commandRepository;
            _mapper = mapper;
        }

        public void Process(NewArticleRequest newArticleRequest)
        {
            var articleToSave = _mapper.Map<Article>(newArticleRequest.Article);
            _commandRepository.CreateArticle(articleToSave);
        }
    }
}