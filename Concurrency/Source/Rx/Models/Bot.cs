using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Rx.Extensions;
using System.Threading;
using System.Collections.Concurrent;
using System.Reactive;
using NLog;

namespace Rx.Models
{
    public class Bot : IDisposable, IObservable<string>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private Func<IPEndPoint> _endPoint;
        private Socket _listenSocket;
        private CancellationTokenSource _cancellationSource;
        private ConcurrentBag<IObserver<string>> _dependentBots = new ConcurrentBag<IObserver<string>>();
        private string _name;
        public Bot(Func<IPEndPoint> endPoint, string name)
        {
            _endPoint = endPoint;
            _name = name;
            _cancellationSource = new CancellationTokenSource();
        }
        public async Task StartRecievingCommands()
        {
            List<Task> connections = new List<Task>();
            var endpoint = _endPoint();
            while (endpoint != null)
            {
                var endpointCopy = endpoint;
                connections.Add(
                    Task.Factory.StartNew(async () =>
                        {
                            await RecieveCommandsFromLeader(endpointCopy);
                        },
                        _cancellationSource.Token,
                        TaskCreationOptions.LongRunning,
                        TaskScheduler.Current)
                    );
                endpoint = _endPoint();
            }
            await Task.WhenAll(connections);
        }

        public Task StartAttack(string address)
        {
            return Task.Factory.StartNew(() =>
                {                    
                    logger.Info("{0} attacks {1}",
                                        _name,
                                        address);
                    Parallel.ForEach<IObserver<string>>(_dependentBots, (x) => x.OnNext(address));
                });
        }

        private async Task RecieveCommandsFromLeader(IPEndPoint endpoint)
        {
            Socket leader = new Socket(SocketType.Stream, ProtocolType.Tcp);
            while (true)
            {
                try
                {
                    await leader.ConnectAsyncTask(endpoint);
                    while (true)
                    {
                        try
                        {
                            string address = Encoding.UTF8.GetString(await leader.RecieveAsyncTask());
                            await StartAttack(address);
                        }
                        catch (OperationCanceledException)
                        {
                            throw;
                        }
                        catch
                        {
                            break;
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }
        }

        public async Task Listen(IPEndPoint hostEndpoint)
        {
            _listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            _listenSocket.Bind(hostEndpoint);
            _listenSocket.Listen(int.MaxValue);

            await Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    try
                    {
                        var socket = await _listenSocket.AcceptAsyncTask();
                        Subscribe(
                            Observer.Create<string>(
                            onNext: x =>
                                {
                                    var address = Encoding.UTF8.GetBytes(x);
                                    logger.Info("{0} commands {1} to attack {2}",
                                        _name,
                                        socket.RemoteEndPoint.GetAddress(),
                                        x);
                                    socket.SendAsyncTask(address);
                                },
                            onCompleted: () =>
                                {
                                    socket.Close();
                                },
                            onError: e =>
                                {
                                    socket.Close();
                                }
                            ));
                    }
                    catch (OperationCanceledException)
                    {
                        break;
                    }
                }

            },
            _cancellationSource.Token,
            TaskCreationOptions.LongRunning,
            TaskScheduler.Current
            );
        }

        public void Dispose()
        {
            Parallel.ForEach<IObserver<string>>(_dependentBots, (x) => x.OnCompleted());
            _cancellationSource.Cancel();
        }

        public IDisposable Subscribe(IObserver<string> observer)
        {
            _dependentBots.Add(observer);
            return null;
        }
    }
}
