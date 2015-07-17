using System;
using System.Collections.Generic;

namespace Infotecs.GangOfFour.Observer
{
    internal sealed class EarthquakeSensor<T> : IObservable<T>
        where T : IMessage, new()
    {
        private readonly HashSet<IObserver<T>> _recipients = new HashSet<IObserver<T>>();

        public void Attach(IObserver<T> observer)
        {
            _recipients.Add(observer);
        }

        public void Detach(IObserver<T> observer)
        {
            _recipients.Remove(observer);
        }

        public void Notify()
        {
            var message = new T();
            foreach (IObserver<T> recipient in _recipients)
            {
                recipient.OnNext(message);
                recipient.OnCompleted();
            }
        }
    }
}
