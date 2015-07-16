using System;

namespace Infotecs.GangOfFour.Observer
{
    internal class StandardObserver<T> : IObserver<T>
        where T : IMessage
    {
        public StandardObserver(string observerName)
        {
            ObserverName = observerName;
        }

        private string ObserverName { get; set; }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(T value)
        {
            Console.WriteLine("{0} notified with message \"{1}\" occured at {2}", ObserverName, value.Body, value.TimeStamp);
        }
    }
}
