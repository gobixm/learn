using System;

namespace Infotecs.GangOfFour.Memento
{
    internal class User
    {
        public User(string firstName, string middleName, string surname)
        {
            FirstName = firstName;
            MiddleName = middleName;
            Surname = surname;
        }

        public string FirstName { get; private set; }
        public string MiddleName { get; private set; }
        public string Surname { get; private set; }

        public void Clear()
        {
            FirstName = MiddleName = Surname = "";
        }

        public UserMemento CreateMemento()
        {
            return new UserMemento { FirstName = FirstName, MiddleName = MiddleName, Surname = Surname };
        }

        public void LoadFromMemento(UserMemento memento)
        {
            FirstName = memento.FirstName;
            MiddleName = memento.MiddleName;
            Surname = memento.Surname;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", FirstName, MiddleName, Surname);
        }
    }
}
