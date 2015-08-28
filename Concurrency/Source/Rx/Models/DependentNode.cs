using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Rx.Extensions;

namespace Rx.Models
{
    public class DependentNode:IObserver<string>
    {
        private Socket _socket;
        public DependentNode(Socket socket)
        {
            _socket = socket;
        }

        public void OnCompleted()
        {
            _socket.Close();
        }

        public void OnError(Exception error)
        {
            _socket.Close();
        }

        public void OnNext(string value)
        {
            _socket.SendAsyncTask(Encoding.UTF8.GetBytes(value));
        }
    }
}
