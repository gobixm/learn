using System;
using Infotecs.Attika.AttikaService.Mappings;
using Nelibur.ObjectMapper;

namespace Infotecs.Attika.AttikaConsoleHost.Mappings
{
    public sealed class StandardTinyMapper : IMapper
    {
        private static readonly object LockObject = new object();
        private Action _configuration;

        public T Map<T>(object source)
        {
            lock (LockObject)
            {
                return TinyMapper.Map<T>(source);
            }
        }

        public IMapper Configuration(Action configuration)
        {
            _configuration = configuration;
            return this;
        }

        public void Configure()
        {
            _configuration();
        }
    }
}