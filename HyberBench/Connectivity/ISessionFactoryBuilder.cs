using NHibernate;

namespace HyberBench.Connectivity
{
    public interface ISessionFactoryBuilder
    {
        ISessionFactory Build();
        void RecreateDb();
    }
}