using System;

namespace Infotecs.GangOfFour.Observer
{
    internal sealed class Evented
    {
        public event EventHandler<EventArgs> SimpleEvent = (sender, args) => { };

        public void Fire()
        {
            SimpleEvent(this, EventArgs.Empty);
        }
    }
}
