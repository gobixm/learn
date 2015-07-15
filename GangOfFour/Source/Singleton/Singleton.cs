using System;

namespace Infotecs.GangOfFour.Singleton
{
    internal sealed class Singleton
    {
        private static readonly Singleton _instance = new Singleton();

        static Singleton()
        {
        }

        private Singleton()
        {
        }

        public static Singleton Instance
        {
            get { return _instance; }
        }

        public string Field
        {
            get { return "Singleton " + GetType().FullName; }
        }
    }
}
