using System;

namespace Infotecs.Attika.AttikaInfrastructure.Services.Contracts
{
    public interface IMappingService
    {
        void Bind<TSource, TTarget>();
        T Map<T>(object source);
    }
}
