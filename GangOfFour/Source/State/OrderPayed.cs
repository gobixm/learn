using System;

namespace Infotecs.GangOfFour.State
{
    internal class OrderPayed : IOrderState
    {
        public void ChangeState(Order order, IOrderState newState)
        {
            order.ChangeState(newState);
        }

        public void Pay(Order order)
        {
            throw new ArgumentException("Order cant be payed. It's already payed");
        }

        public void Place(Order order)
        {
            throw new ArgumentException("Order cant be placed. It's already payed");
        }

        public void Ship(Order order)
        {
            ChangeState(order, new OrderShipped());
        }
    }
}
