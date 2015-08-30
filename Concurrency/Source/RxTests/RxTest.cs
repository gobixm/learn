using Microsoft.Reactive.Testing;
using Rx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RxTests
{
    public class RxTest
    {
        [Fact]
        public async Task ShouldRecieveOnNextNotification()
        {
            var leaderEndpoint = new IPEndPoint(IPAddress.Loopback, 40000);
            Bot leader = new Bot(
                null,
                "leader",
                leaderEndpoint);
            EventWaitHandle fired = new EventWaitHandle(false, EventResetMode.ManualReset);
            leader.Subscribe(Observer.Create<string>(x => fired.Set()));
            
            await leader.StartAttack("address");
            if(!fired.WaitOne(1000))
            {
                Assert.False(true);
            }
        }

        [Fact]
        public async Task ShouldRecieveMultipleNextNotification()
        {
            var leaderEndpoint = new IPEndPoint(IPAddress.Loopback, 40000);
            Bot leader = new Bot(
                null,
                "leader",
                leaderEndpoint);
            
            EventWaitHandle[] fired = new EventWaitHandle[10];
            for(int i=0; i<fired.Length; i++)
            {
                var closure = i;
                fired[i] = new EventWaitHandle(false, EventResetMode.ManualReset);
                leader.Subscribe(Observer.Create<string>(x => fired[closure].Set()));
            }            
            
            await leader.StartAttack("address");
            if (!WaitHandle.WaitAll(fired, 1000))
            {
                Assert.False(true);
            }
        }

        [Fact]
        public void ShouldDelaySubscription()
        {
            var scheduler = new TestScheduler();
            var observable = Observable.Return<string>("test").DelaySubscription(DateTime.Now + TimeSpan.FromMinutes(1), scheduler);
            bool fired = false;
            observable.Subscribe((x) => fired=true);
            Assert.False(fired);
            scheduler.Start();
            Assert.True(fired);
        }

        [Fact]
        public void ShouldCreateRange()
        {
            var urls = new List<string>();
            var range = Observable.Range(10, 10).Subscribe(
                (page) =>
                {
                    urls.Add(string.Format("www.google.ru?page={0}", page));
                },
                ()=>Assert.Equal(10, urls.Count) 
                );            
            Assert.Equal("www.google.ru?page=10", urls[0]);
            Assert.Equal("www.google.ru?page=19", urls[9]);
        }
    }
}
