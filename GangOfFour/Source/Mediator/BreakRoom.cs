using System;
using System.Collections.Generic;

namespace Infotecs.GangOfFour.Mediator
{
    internal sealed class BreakRoom : IMediator
    {
        private readonly List<IWorker> _roommates = new List<IWorker>();

        public void AttachWorker(IWorker worker)
        {
            _roommates.Add(worker);
        }

        public void RiotRequest(IWorker sender)
        {
            Console.WriteLine("{0} promotes to riot", sender.GetType());
            if (sender is CharismaticWorker)
            {
                foreach (IWorker roommate in _roommates)
                {
                    roommate.Riot();
                }
            }
        }
    }
}
