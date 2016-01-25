using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using RepoFac.Entities;
using RepoFac.Repo;
using RepoFac.UnitOfWork;

namespace RepoFac
{
    class Program
    {
        static void Main(string[] args)
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.Register(x => new RepoFactory(configuration =>
            {
                var context = x.Resolve<IComponentContext>();
                configuration.Bind<IGenericRepo<IUser>>(s => context.Resolve<IGenericRepo<IUser>>(new PositionalParameter(0, s)));
                configuration.Bind<IGenericRepo<IGroup>>(s => context.Resolve<IGenericRepo<IGroup>>(new PositionalParameter(0, s)));
                configuration.Bind<IFancyRepo>(s => context.Resolve<IFancyRepo>(new PositionalParameter(0, s)));
            }))
            .As<IRepoFactory>().SingleInstance();

            builder.RegisterType<GenericRepo<IUser>>().As<IGenericRepo<IUser>>();
            builder.RegisterType<GenericRepo<IGroup>>().As<IGenericRepo<IGroup>>();
            builder.RegisterType<FancyRepo>().As<IFancyRepo>();

            builder.RegisterType<UnitOfMock>().As<IUnitOfMock>();

            var container = builder.Build();
            var factory = container.Resolve<IRepoFactory>();

            ISession session = null;

            Console.WriteLine(factory.Get<IGenericRepo<IUser>>(session).WhoAmI());
            Console.WriteLine(factory.Get<IGenericRepo<IGroup>>(session).WhoAmI());
            Console.WriteLine(factory.Get<IFancyRepo>(session).WhoAmI());
            
            Console.WriteLine("Let IGenericRepo<IUser> becomes FancyRepo");
            builder = new ContainerBuilder();
            builder.RegisterType<FancyRepo>().As<IGenericRepo<IUser>>();
            builder.Update(container);
            
            Console.WriteLine(factory.Get<IGenericRepo<IUser>>(session).WhoAmI());
            Console.WriteLine(factory.Get<IGenericRepo<IGroup>>(session).WhoAmI());
            Console.WriteLine(factory.Get<IFancyRepo>(session).WhoAmI());
            Console.WriteLine("Ta-Da!!!");

            Console.WriteLine("Measure performance 1 000 000 resolve/allocations");
            Console.WriteLine("Pure Allocations...");
            var stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < 1000000; i++)
            {
                new FancyRepo(session);
            }
            Console.WriteLine($"...takes {stopwatch.ElapsedMilliseconds} milliseconds");

            stopwatch.Restart();
            Console.WriteLine("Resolves...");
            for (int i = 0; i < 1000000; i++)
            {
                //factory.Get<IFancyRepo>(session);
                container.Resolve<IFancyRepo>(new PositionalParameter(0, session));
            }
            Console.WriteLine($"...takes {stopwatch.ElapsedMilliseconds} milliseconds");

            Console.ReadLine();
        }
    }
}
