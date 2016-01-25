using System;

namespace HyberBench.Connectivity
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}