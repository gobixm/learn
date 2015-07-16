using System;

namespace Infotecs.GangOfFour.Observer
{
    internal class Evented
    {
        public event EventHandler<EventArgs> SimpleEvent = (sender, args) => { };

        public void Fire()
        {
            SimpleEvent(this, EventArgs.Empty);
        }
    }
}
