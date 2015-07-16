using System;
using System.Collections.Generic;

namespace Infotecs.GangOfFour.Memento
{
    internal class Caretaker
    {
        private static readonly Caretaker _instance = new Caretaker();

        private readonly Dictionary<WeakReference<User>, UserMemento> _store = new Dictionary<WeakReference<User>, UserMemento>();

        static Caretaker()
        {
        }

        private Caretaker()
        {
        }

        public static Caretaker Instance
        {
            get { return _instance; }
        }

        public UserMemento Get(WeakReference<User> userRef)
        {
            return _store[userRef];
        }

        public void Store(WeakReference<User> userRef, UserMemento memento)
        {
            _store[userRef] = memento;
        }
    }
}
