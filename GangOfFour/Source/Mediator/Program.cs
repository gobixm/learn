using System;
using System.Collections.Generic;

namespace Infotecs.GangOfFour.Mediator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IMediator breakRoom = new BreakRoom();
            List<IWorker> workers = new List<IWorker>
            {
                new SimpleWorker(breakRoom),
                new SimpleWorker(breakRoom),
                new SimpleWorker(breakRoom)
            };
            workers[0].PromoteToRiot();
            Console.ReadKey();
        }
    }
}
