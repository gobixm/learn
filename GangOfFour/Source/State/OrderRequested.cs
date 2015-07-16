using System;

namespace Infotecs.GangOfFour.State
{
    internal class OrderRequested : IOrderState
    {
        public void ChangeState(Order order, IOrderState newState)
        {
            order.ChangeState(newState);
        }

        public void Pay(Order order)
        {
            throw new ArgumentException("Order must be Placed before Payment");
        }

        public void Place(Order order)
        {
            ChangeState(order, new OrderPlaced());
        }

        public void Ship(Order order)
        {
            throw new ArgumentException("Order must be Placed before Shipment");
        }
    }
}
