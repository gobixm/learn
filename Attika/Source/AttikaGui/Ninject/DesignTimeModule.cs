using System.Collections.Generic;
using System.Linq;
using Infotecs.Attika.AttikaGui.DTO;
using Infotecs.Attika.AttikaGui.DesignTime;
using Infotecs.Attika.AttikaGui.Model;
using Infotecs.Attika.AttikaGui.ViewModel;
using Moq;
using Ninject.Modules;

namespace Infotecs.Attika.AttikaGui.Ninject
{
    public class DesignTimeModule : NinjectModule
    {
        public override void Load()
        {
            var dataService = new Mock<IDataService>();
            List<ArticleDto> articles = DesignDataFixture.GetFakeArticles();
            List<ArticleHeaderDto> headers = DesignDataFixture.BuildHeaders(articles);
            dataService
                .Setup(s => s.GetArticle(It.IsAny<string>()))
                .Returns((string id) => (from a in articles where a.Id.ToString() == id select a).FirstOrDefault());
            dataService.
                Setup(s => s.GetArticleHeaders())
                       .Returns(
                           () => headers);
            dataService.Setup(s => s.NewArticle(It.IsAny<ArticleDto>())).Callback<ArticleDto>(a =>
                {
                    articles.Add(a);
                    headers.Add(new ArticleHeaderDto
                        {
                            ArticleId = a.Id,
                            Title = a.Title
                        });
                });

            Bind<IDataService>().ToMethod(ctx => dataService.Object);
            Bind<MainViewModel>().ToSelf();
            Bind<NavigationViewModel>().ToSelf();
            Bind<ArticleViewModel>().ToSelf();
            Bind<IDataSerializer>().To<WcfJsonSerializer>();
        }
    }
}