using System;

namespace Infotecs.GangOfFour.Memento
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var user = new User("Kirill", "N.", "Ivanov");
            var userRef = new WeakReference<User>(user);
            Caretaker.Instance.Store(userRef, user.CreateMemento());
            Caretaker.Instance.Store(userRef, user.CreateMemento());
            Console.WriteLine("The user is: {0}", user);
            user.Clear();
            Console.WriteLine("The user after clear() is: {0}", user);
            user.LoadFromMemento(Caretaker.Instance.Get(userRef));
            Console.WriteLine("The user after restore is: {0}", user);
            Console.ReadLine();
        }
    }
}
