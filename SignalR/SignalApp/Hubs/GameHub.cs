using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using SignalApp.Models;

namespace SignalApp.Hubs
{
    [HubName("gameHub")]
    public class GameHub : Hub
    {
        private IGameService _gameService;
        public GameHub(IGameService gameService)
        {
            _gameService = gameService;            
        }

        public Player Connect()
        {           
           return _gameService.SpawnPlayer(Context.ConnectionId);
        }

        public void Disconnect()
        {            
        }

        public void Move(double x, double y)
        {
            _gameService.Move(Context.ConnectionId, x, y);
        }

        public void OnEvent(GameEvent gameEvent)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<GameHub>();
            context.Clients.All.notify(gameEvent);
        }

        public void GetPlayers()
        {
            _gameService.Players
                .Values
                .Where(x=>x.Name != Context.ConnectionId).ToList()
                .ForEach(x =>
                {
                    Clients.Client(Context.ConnectionId).notify(new NewPlayerEvent(x));
                });            
        }

        public override Task OnConnected()
        {
            _gameService.Subscribe(OnEvent);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {            
            _gameService.DestroyPlayer(Context.ConnectionId);
            _gameService.Unsubscribe(OnEvent);
            return base.OnDisconnected(stopCalled);
        }
    }
}