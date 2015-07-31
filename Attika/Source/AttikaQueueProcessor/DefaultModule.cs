using Infotecs.Attika.AtticaDataModel;
using Infotecs.Attika.AtticaDataModel.Repositories;
using Infotecs.Attika.AttikaQueueProcessor.Processors;
using Infotecs.Attika.AttikaSharedDataObjects.DataTransferObjects;
using Infotecs.Attika.AttikaSharedDataObjects.Mappings;
using Nelibur.ObjectMapper;
using Ninject;
using Ninject.Modules;

namespace Infotecs.Attika.AttikaQueueProcessor
{
    public class DefaultModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICommandRepository>().To<StandardCommandRepository>().InSingletonScope();
            Bind<IMapper>().To<StandardTinyMapper>().InSingletonScope();
            Bind<ArticleProcessor>().ToSelf().InSingletonScope();
            Bind<QueueDirector>().ToSelf().InSingletonScope().WithConstructorArgument("processors", new BaseProcessor[]{Kernel.Get<ArticleProcessor>()});

            Kernel.Get<IMapper>().Configuration(() =>
            {
                TinyMapper.Bind<ArticleDto, Article>();
                TinyMapper.Bind<CommentDto, Comment>();
            })
                  .Configure();
        }
    }
}