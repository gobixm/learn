using System;
using Infotecs.Attika.AttikaService.Mappings;
using Nelibur.ObjectMapper;

namespace Infotecs.Attika.AttikaConsoleHost.Mappings
{
    public sealed class StandardTinyMapper : IMapper
    {
        private Action _configuration;
        private static object LockObject = new object();

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

        public IMapper Configure()
        {
            _configuration();
            return this;
        }
    }
}