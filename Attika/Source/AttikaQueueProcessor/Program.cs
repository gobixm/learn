using System;
using Infotecs.Attika.AttikaQueueProcessor.Processors;
using Ninject;

namespace Infotecs.Attika.AttikaQueueProcessor
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            IKernel kernel = new StandardKernel(new DefaultModule());
            var director = kernel.Get<QueueDirector>();
            director.StartManagement();
            Console.ReadKey();
        }
    }
}