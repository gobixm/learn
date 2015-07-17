using System;

namespace Infotecs.GangOfFour.State
{
    internal sealed class OrderPlaced : IOrderState
    {
        public void ChangeState(Order order, IOrderState newState)
        {
            order.ChangeState(newState);
        }

        public void Pay(Order order)
        {
            ChangeState(order, new OrderPayed());
        }

        public void Place(Order order)
        {
            throw new ArgumentException("order already placed");
        }

        public void Ship(Order order)
        {
            throw new ArgumentException("you must pay first, order placed but not payed");
        }
    }
}
