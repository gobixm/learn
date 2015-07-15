using System.Collections.Generic;
namespace Infotecs.GangOfFour.FlyWeight
{
    internal class SharedEmoticonFactory:IEmoticonFactory
    {
        private readonly Dictionary<string, IEmoticon> _pool;

        public SharedEmoticonFactory()
        {
            _pool = new Dictionary<string, IEmoticon>();
        }

        public IEmoticon GetEmoticon(string name)
        {
            if (_pool.ContainsKey(name))
            {
                return _pool[name];
            }
            else
            {
                SharedEmoticon emoticon = new SharedEmoticon();
                emoticon.Load(name);
                _pool.Add(name, emoticon);
                return emoticon;
            }

        }
    }
}