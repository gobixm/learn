using Infotecs.Attika.AttikaInfrastructure.Services.Contracts;
using Nelibur.ObjectMapper;

namespace Infotecs.Attika.AttikaInfrastructure.Services
{
    public sealed class StandardTinyMappingService : IMappingService
    {
        private static readonly object LockObject = new object();

        public T Map<T>(object source)
        {
            lock (LockObject)
            {
                return TinyMapper.Map<T>(source);
            }
        }

        public void Bind<TSource, TTarget>()
        {
            TinyMapper.Bind<TSource, TTarget>();
        }
    }
}