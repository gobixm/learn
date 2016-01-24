using System;
using Autofac;
using HyberBench.Connectivity;
using NHibernate;

namespace HyberBench
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.Register(x => x.Resolve<ISessionFactoryBuilder>().Build()).SingleInstance().As<ISessionFactory>();
            builder.Register(x => x.Resolve<ISessionFactory>().OpenSession());
            builder.Register(x => x.Resolve<ISessionFactory>().OpenStatelessSession());
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<BenchSuite>().AsSelf();
            builder.RegisterType<SessionHelper>().As<ISessionHelper>();
            builder.RegisterType<SqliteSessionFactoryBuilder>().As<ISessionFactoryBuilder>().SingleInstance();

            var container = builder.Build();
            var suite = container.Resolve<BenchSuite>();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Sqlite");
            Console.ResetColor();
            suite.Run();

            builder = new ContainerBuilder();
            builder.RegisterType<MsSqlSessionFactoryBuilder>().As<ISessionFactoryBuilder>().SingleInstance();
            builder.Register(x => x.Resolve<ISessionFactoryBuilder>().Build()).SingleInstance().As<ISessionFactory>();
            builder.Update(container);
            suite = container.Resolve<BenchSuite>();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Mssql");
            Console.ResetColor();
            suite.Run();

            Console.ReadKey();
        }
    }
}