using System;

namespace Infotecs.GangOfFour.Prototype
{
    internal interface IRequest
    {
        char[] Data { get; }
        IRequest Clone();
        void Modify(Func<char, char> replace);
    }
}
