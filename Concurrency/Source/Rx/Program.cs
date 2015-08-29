using Rx.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rx
{
    class Program
    {
        static ConcurrentBag<Bot> nodes = new ConcurrentBag<Bot>();
        static List<Task> listenTasks = new List<Task>();
        static List<Task> connectionTasks = new List<Task>();
        static void Main(string[] args)
        {
            const int BasePort = 40000;
            const int LvlOneMultiplier = 1000;
            const int LvlTwoMultiplier = 100;
            const int LvlThreeMultiplier = 10;
            //configure botnet
            
            //leader
            Bot leader =  AddBotNode(new List<IPEndPoint>(), new IPEndPoint(IPAddress.Loopback, BasePort), "gray cardinal");            

            //first level bots
            for(int i=1; i<5; i++)
            {
                int lvlOnePort = BasePort+LvlOneMultiplier*i;
                AddBotNode(new List<IPEndPoint>{
                    new IPEndPoint(IPAddress.Loopback, BasePort)
                },
                new IPEndPoint(IPAddress.Loopback, lvlOnePort),
                string.Format("First Level Zombie-{0}", i));

                //second level bots                
                for(int j=1; j<10; j++)
                {
                    int lvlTwoPort = lvlOnePort+LvlTwoMultiplier*j;
                    AddBotNode(new List<IPEndPoint>{
                        new IPEndPoint(IPAddress.Loopback, lvlOnePort)
                    },
                    new IPEndPoint(IPAddress.Loopback, lvlTwoPort),
                    string.Format("Second Level Zombie-{0},{1}", i, j));

                    //third level bots                
                    for (int k = 1; k < 10; k++)
                    {
                        int lvlThreePort = lvlTwoPort + LvlThreeMultiplier * k;
                        AddBotNode(new List<IPEndPoint>{
                        new IPEndPoint(IPAddress.Loopback, lvlTwoPort)
                        },
                        new IPEndPoint(IPAddress.Loopback, lvlThreePort),
                        string.Format("Third Level Zombie-{0},{1},{2}", i, j, k));
                    }   
                }                
            }

            Parallel.ForEach(nodes, x => x.CreateListener());
            Parallel.ForEach(nodes, async x => await x.Listen());
            Parallel.ForEach(nodes, async x => await x.ConnectToLeaders());
            Parallel.ForEach(nodes, async x => await x.RecieveCommandsFromLeader());
            
            Task.WaitAll(
                leader.StartAttack("www.google.com"),
                leader.StartAttack("www.yandex.com"),
                leader.StartAttack("www.yahoo.com")
                );                        
            Console.ReadKey();
            Console.WriteLine("Stopping...");
            Parallel.ForEach<Bot>(nodes, x => x.Dispose());            
            Console.WriteLine("Stopped");

            
        }

        static private Bot AddBotNode(IEnumerable<IPEndPoint> leaderEndpoints, IPEndPoint selfEndpoint, string name)
        {
            var iterator = leaderEndpoints.GetEnumerator();
            var bot = new Bot(() =>
                {
                    iterator.MoveNext();
                    return iterator.Current;
                },
                name,
                selfEndpoint);
            nodes.Add(bot);                   
            return bot;
        }
    }
}
