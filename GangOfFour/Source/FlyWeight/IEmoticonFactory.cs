using System;

namespace Infotecs.GangOfFour.FlyWeight
{
    internal interface IEmoticonFactory
    {
        IEmoticon GetEmoticon(string name);
    }
}
