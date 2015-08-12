using System;
using System.Collections.Generic;
using System.Linq;
using AttikaContracts.DataTransferObjects;
using Infotecs.Attika.AttikaClient;
using Infotecs.Attika.AttikaGui.ViewModels;
using Moq;
using Ninject.Modules;
using Infotecs.Attika.AttikaGui.DesignTimes;

namespace Infotecs.Attika.AttikaGui.NinjectModules
{
    public sealed class DesignTimeModule : NinjectModule
    {
        public override void Load()
        {
            var dataService = new Mock<IClientService>();
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

            Bind<IClientService>().ToMethod(ctx => dataService.Object);
            Bind<MainViewModel>().ToSelf();
            Bind<NavigationViewModel>().ToSelf();
            Bind<ArticleViewModel>().ToSelf();
        }
    }
}
