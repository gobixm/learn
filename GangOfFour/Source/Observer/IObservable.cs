using System;

namespace Infotecs.GangOfFour.Observer
{
    internal interface IObservable<out T>
        where T : IMessage
    {
        void Attach(IObserver<T> observer);
        void Detach(IObserver<T> observer);
        void Notify();
    }
}
