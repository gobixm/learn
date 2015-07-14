using System;

namespace Prototype
{
    internal interface IRequest
    {
        IRequest Clone();
        void Modify(Func<char, char> replace);
        char[] Data { get; }
    }
}