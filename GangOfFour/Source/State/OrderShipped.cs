using System;

namespace Infotecs.GangOfFour.State
{
    internal sealed class OrderShipped : IOrderState
    {
        public void ChangeState(Order order, IOrderState newState)
        {
            order.ChangeState(newState);
        }

        public void Pay(Order order)
        {
            throw new ArgumentException("order shipped, what you suppose to pay?");
        }

        public void Place(Order order)
        {
            throw new ArgumentException("order shipped. There no reason to place it again");
        }

        public void Ship(Order order)
        {
            throw new ArgumentException("order already shipped");
        }
    }
}
