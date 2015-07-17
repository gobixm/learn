using System;

namespace Infotecs.GangOfFour.State
{
    internal sealed class Order
    {
        public Order()
        {
            State = new OrderRequested();
        }

        private IOrderState State { get; set; }

        public void ChangeState(IOrderState newState)
        {
            State = newState;
            Console.WriteLine("order state changed to {0}", State.GetType().Name);
        }

        public void Pay()
        {
            State.Pay(this);
        }

        public void Place()
        {
            State.Place(this);
        }

        public void Ship()
        {
            State.Ship(this);
        }
    }
}
