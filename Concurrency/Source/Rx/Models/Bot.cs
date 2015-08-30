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

        private Func<IPEndPoint> _endPointGetter;
        IPEndPoint _listenEndpoint;
        private Socket _listenSocket;
        private List<Socket> _leaders = new List<Socket>();

        private CancellationTokenSource _cancellationSource;

        private ConcurrentBag<IObserver<string>> _dependentBots = new ConcurrentBag<IObserver<string>>();        
        private string _name;
        
        public Bot(Func<IPEndPoint> endPointGetter, string name, IPEndPoint listenEndpoint)
        {
            _endPointGetter = endPointGetter;
            _listenEndpoint = listenEndpoint;
            _name = name;
            _cancellationSource = new CancellationTokenSource();
        }

        public async Task StartAttack(string address)
        {
            await Task.Factory.StartNew(() =>
                {
                    logger.Info("{0} attacks {1}",
                                        _name,
                                        address);
                    Parallel.ForEach<IObserver<string>>(_dependentBots, x =>
                        {
                            x.OnNext(address);
                        });
                },
                _cancellationSource.Token);
        }

        public async Task ConnectToLeaders()
        {
            var endpoint = _endPointGetter();
            var tasks = new List<Task>();
            while (endpoint != null)
            {
                Socket leader = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _leaders.Add(leader);
                tasks.Add(leader.ConnectAsyncTask(endpoint, _cancellationSource));
                endpoint = _endPointGetter();
            }
            await Task.WhenAll(tasks.ToArray());
        }

        public async Task RecieveCommandsFromLeader()
        {
            await Task.Run(() =>
                Parallel.ForEach(_leaders, async socket =>
                {
                    while (true)
                    {
                        try
                        {
                            _cancellationSource.Token.ThrowIfCancellationRequested();
                            if (!socket.Connected)
                            {
                                await Task.Delay(1000);
                                continue;
                            }
                            if (socket.Connected)
                            {
                                string address = Encoding.UTF8.GetString(await socket.RecieveAsyncTask(_cancellationSource));
                                await StartAttack(address);
                            }
                        }
                        catch (OperationCanceledException)
                        {
                            break;
                        }
                        catch
                        {
                            break;
                        }
                    }
                }));
        }

        public void CreateListener()
        {
            _listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _listenSocket.Bind(_listenEndpoint);
            _listenSocket.Listen(int.MaxValue);
        }

        public async Task Listen()
        {
            await Task.Run(async () =>
                {
                    while (true)
                    {
                        try
                        {
                            var socket = await _listenSocket.AcceptAsyncTask(_cancellationSource);
                            var disposed = false;
                            _cancellationSource.Token.ThrowIfCancellationRequested();
                            var observer = Observer.Create<string>(
                                onNext: x =>
                                    {
                                        var address = Encoding.UTF8.GetBytes(x);
                                        logger.Info("{0} commands {1} to attack {2}",
                                            _name,
                                            socket.RemoteEndPoint.GetAddress(),
                                            x);
                                        try
                                        {
                                            if (!disposed)
                                            {
                                                socket.SendAsyncTask(address, _cancellationSource);
                                            }
                                        }
                                        catch
                                        {

                                        }
                                    },
                                onCompleted: () =>
                                    {
                                        disposed = true;
                                        socket.Close();
                                    },
                                onError: e =>
                                    {
                                        disposed = true;
                                        socket.Close();
                                    }
                                );
                            Subscribe(observer);
                        }
                        catch (OperationCanceledException)
                        {
                            break;
                        }

                    }
                });
        }

        public void Dispose()
        {
            _cancellationSource.Cancel();
            if (_listenSocket != null)
            {
                _listenSocket.Close();
            }
            Parallel.ForEach<IObserver<string>>(_dependentBots, (x) => x.OnCompleted());
        }

        public IDisposable Subscribe(IObserver<string> observer)
        {
            _dependentBots.Add(observer);
            return null;
        }
    }
}
