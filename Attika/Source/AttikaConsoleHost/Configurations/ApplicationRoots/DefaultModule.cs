using Infotecs.Attika.AtticaDataModel;
using Infotecs.Attika.AtticaDataModel.Repositories;
using Infotecs.Attika.AttikaService.Messages.Handlers;
using Infotecs.Attika.AttikaService.Messages.Queues;
using Infotecs.Attika.AttikaSharedDataObjects.DataTransferObjects;
using Infotecs.Attika.AttikaSharedDataObjects.Mappings;
using Nelibur.ObjectMapper;
using Ninject;
using Ninject.Modules;

namespace Infotecs.Attika.AttikaConsoleHost.Configurations.ApplicationRoots
{
    public sealed class DefaultModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICommandRepository>().To<StandardCommandRepository>();
            Bind<IQueryRepository>().To<StandardQueryRepository>();
            Bind<IMapper>().To<StandardTinyMapper>().InSingletonScope();
            Bind<IQueuePusher>().To<QueuePusher>();
            Bind<ArticleHandler>().ToSelf();
            Bind<IMessageProcessorConfiguration>().To<MessageProcessorConfiguration>()
                                                  .InSingletonScope()
                                                  .WithConstructorArgument("handlers",
                                                                           new BaseHandler[]
                                                                               {Kernel.Get<ArticleHandler>()});
            Bind<IMessageProcessor>().To<MessageProcessor>();

            Kernel.Get<IMapper>().Configuration(() =>
                {
                    TinyMapper.Bind<ArticleDto, Article>();
                    TinyMapper.Bind<CommentDto, Comment>();
                })
                  .Configure();
        }
    }
}