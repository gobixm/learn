using System;

namespace Infotecs.GangOfFour.State
{
    internal interface IOrderState
    {
        void ChangeState(Order order, IOrderState newState);
        void Pay(Order order);
        void Place(Order order);
        void Ship(Order order);
    }
}
