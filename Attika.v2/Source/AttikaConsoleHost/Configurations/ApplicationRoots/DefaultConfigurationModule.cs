using System;
using AttikaContracts.DataTransferObjects;
using AttikaContracts.Messages;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using Infotecs.Attika.AttikaDomain.Aggregates;
using Infotecs.Attika.AttikaDomain.Entities;
using Infotecs.Attika.AttikaDomain.Factories;
using Infotecs.Attika.AttikaDomain.Factories.Contracts;
using Infotecs.Attika.AttikaDomain.Services.Queuing;
using Infotecs.Attika.AttikaDomain.Services.RequestProcessors;
using Infotecs.Attika.AttikaDomain.Validators;
using Infotecs.Attika.AttikaDomain.Validators.Contracts;
using Infotecs.Attika.AttikaInfrastructure.Data;
using Infotecs.Attika.AttikaInfrastructure.Data.Models;
using Infotecs.Attika.AttikaInfrastructure.Data.Repositories;
using Infotecs.Attika.AttikaInfrastructure.Services;
using Infotecs.Attika.AttikaInfrastructure.Services.Contracts;
using Nelibur.ServiceModel.Services;
using Ninject;
using Ninject.Modules;

namespace Infotecs.Attika.AttikaConsoleHost.Configurations.ApplicationRoots
{
    public sealed class DefaultConfigurationModule : NinjectModule
    {
        public override void Load()
        {
#if DEBUG
            NHibernateProfiler.Initialize();
#endif
            SessionHelper.PrepareDatabase();

            BindRepositories();
            BindFactories();
            BindValidators();
            BindMappings();

            Bind<IMessageSerializationService>().To<MessageSerializationService>().InSingletonScope();
            Bind<IQueueService>().To<QueueService>().InSingletonScope();

            BindHandlers();
        }

        private void BindFactories()
        {
            Bind<IArticleFactory>().To<ArticleFactory>().InSingletonScope();
            Bind<ICommentFactory>().To<CommentFactory>().InSingletonScope();
        }

        private void BindHandlers()
        {
            Bind<ArticleQueryHandler>().ToSelf();
            Bind<ArticleCommandHandler>().ToSelf();
            Bind<ArticleQueueProcessor>().ToSelf();

            NeliburRestService.Configure(configuration =>
            {
                configuration.Bind<GetArticleRequest, ArticleQueryHandler>(() => Kernel.Get<ArticleQueryHandler>());
                configuration.Bind<GetArticleHeadersRequest, ArticleQueryHandler>(
                    () => Kernel.Get<ArticleQueryHandler>());
                configuration.Bind<NewArticleRequest, ArticleCommandHandler>(
                    () => Kernel.Get<ArticleCommandHandler>());
                configuration.Bind<AddArticleCommentRequest, ArticleCommandHandler>(
                    () => Kernel.Get<ArticleCommandHandler>());
                configuration.Bind<DeleteArticleCommentRequest, ArticleCommandHandler>(
                    () => Kernel.Get<ArticleCommandHandler>());
                configuration.Bind<DeleteArticleRequest, ArticleCommandHandler>(
                    () => Kernel.Get<ArticleCommandHandler>());
            });
            QueueService.Configure(configuration =>
            {
                configuration.Bind<NewArticleRequest, ArticleQueueProcessor>(
                    () => Kernel.Get<ArticleQueueProcessor>());
                configuration.Bind<AddArticleCommentRequest, ArticleQueueProcessor>(
                    () => Kernel.Get<ArticleQueueProcessor>());
                configuration.Bind<DeleteArticleCommentRequest, ArticleQueueProcessor>(
                    () => Kernel.Get<ArticleQueueProcessor>());
                configuration.Bind<DeleteArticleRequest, ArticleQueueProcessor>(
                    () => Kernel.Get<ArticleQueueProcessor>());
            });
        }

        private void BindMappings()
        {
            Bind<IMappingService>().To<StandardTinyMappingService>().InSingletonScope().OnActivation(
                (ctx, standardTynyMapper) =>
                {
                    standardTynyMapper.Bind<ArticleDto, ArticleState>();
                    standardTynyMapper.Bind<CommentDto, CommentState>();
                    standardTynyMapper.Bind<Article, ArticleDto>();
                });
        }

        private void BindRepositories()
        {
            Bind<ICommandRepository>().To<StandardCommandRepository>();
            Bind<IQueryRepository>().To<StandardQueryRepository>();
        }

        private void BindValidators()
        {
            Bind<IValidator<Article>>().To<ArticleValidator>().InSingletonScope();
            Bind<IValidator<Comment>>().To<CommentValidator>().InSingletonScope();
        }
    }
}
