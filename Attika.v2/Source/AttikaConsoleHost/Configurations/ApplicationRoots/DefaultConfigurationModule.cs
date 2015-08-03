using Infotecs.Attika.AttikaDomain.Aggregates;
using Infotecs.Attika.AttikaDomain.Entities;
using Infotecs.Attika.AttikaDomain.Factories;
using Infotecs.Attika.AttikaDomain.Factories.Contracts;
using Infotecs.Attika.AttikaDomain.Services.Queuing;
using Infotecs.Attika.AttikaDomain.Services.RequestProcessors;
using Infotecs.Attika.AttikaDomain.Validators;
using Infotecs.Attika.AttikaDomain.Validators.Contracts;
using Infotecs.Attika.AttikaInfrastructure.Data.DataTransferObjects;
using Infotecs.Attika.AttikaInfrastructure.Data.Models;
using Infotecs.Attika.AttikaInfrastructure.Data.Repositories;
using Infotecs.Attika.AttikaInfrastructure.Messaging.Serializers;
using Infotecs.Attika.AttikaInfrastructure.Services;
using Infotecs.Attika.AttikaInfrastructure.Services.Contracts;
using Infotecs.Attika.AttikaService.Messages.Processors;
using Ninject;
using Ninject.Modules;

namespace Infotecs.Attika.AttikaConsoleHost.Configurations.ApplicationRoots
{
    public sealed class DefaultConfigurationModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICommandRepository>().To<StandardCommandRepository>();
            Bind<IQueryRepository>().To<StandardQueryRepository>();
            Bind<IArticleFactory>().To<ArticleFactory>().InSingletonScope();
            Bind<ICommentFactory>().To<CommentFactory>().InSingletonScope();
            Bind<IValidator<Article>>().To<ArticleValidator>().InSingletonScope();
            Bind<IValidator<Comment>>().To<CommentValidator>().InSingletonScope();
            Bind<IMappingService>().To<StandardTinyMappingService>().InSingletonScope().OnActivation(
                (ctx, standardTynyMapper) =>
                {
                    standardTynyMapper.Bind<ArticleDto, ArticleState>();
                    standardTynyMapper.Bind<CommentDto, CommentState>();
                    standardTynyMapper.Bind<Article, ArticleDto>();
                });

            Bind<IMessageSerializationService>().To<MessageSerializationService>().InSingletonScope();
            Bind<ArticleHandler>().ToSelf();
            Bind<IQueueService>().To<QueueService>().InSingletonScope();
            Bind<IMessageProcessorConfiguration>().To<MessageProcessorConfiguration>()
                .InSingletonScope()
                .WithConstructorArgument("handlers",
                    new BaseHandler[]
                    {Kernel.Get<ArticleHandler>()});
            Bind<IMessageProcessor>().To<MessageProcessor>();
        }
    }
}