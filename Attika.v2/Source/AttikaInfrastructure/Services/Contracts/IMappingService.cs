namespace Infotecs.Attika.AttikaInfrastructure.Services.Contracts
{
    public interface IMappingService
    {
        T Map<T>(object source);
        void Bind<TSource, TTarget>();
    }
}