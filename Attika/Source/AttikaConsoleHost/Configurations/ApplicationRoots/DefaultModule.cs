using Infotecs.Attika.AtticaDataModel;
using Infotecs.Attika.AtticaDataModel.Repositories;
using Infotecs.Attika.AttikaConsoleHost.Mappings;
using Infotecs.Attika.AttikaService.DataTransferObjects;
using Infotecs.Attika.AttikaService.Mappings;
using Nelibur.ObjectMapper;
using Ninject.Modules;
using Ninject;

namespace Infotecs.Attika.AttikaConsoleHost.Configurations.ApplicationRoots
{
    public sealed class DefaultModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICommandRepository>().To<StandardCommandRepository>();
            Bind<IQueryRepository>().To<StandardQueryRepository>();
            Bind<IMapper>().To<StandardTinyMapper>();
            Kernel.Get<IMapper>().Configuration(() =>
                {
                    TinyMapper.Bind<ArticleDto, Article>();
                    TinyMapper.Bind<CommentDto, Comment>();
                })
                .Configure();
        }
    }
}