﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Rx.Extensions
{
    public static class SocketExtensions
    {
        public static Task<byte[]> RecieveAsyncTask(this Socket socket)
        {
            var buff = new List<ArraySegment<byte>>();
            buff.Add(new ArraySegment<byte>(new byte[65535]));
            return Task<byte[]>.Factory.FromAsync(
                (ac,o)=>socket.BeginReceive(buff, SocketFlags.None, ac, null),
                ar =>
                {                    
                    var length = socket.EndReceive(ar);
                    var result = new byte[length];
                    Array.Copy(buff[0].Array, result, length);
                    return result;
                },
                null);            
        }

        public static Task SendAsyncTask(this Socket socket, byte[] dataToSend)
        {            
            return Task.Factory.FromAsync(
                (ac, o) => socket.BeginSend(dataToSend, 0, dataToSend.Length, SocketFlags.None, ac, null),
                ar =>
                {
                    socket.EndSend(ar);                    
                },
                null);
        }

        public static Task<Socket> AcceptAsyncTask(this Socket socket)
        {            
            return Task<Socket>.Factory.FromAsync(
                (ac, o) => socket.BeginAccept(ac, o),
                ar =>
                {
                    return socket.EndAccept(ar);
                },
                null);
        }

        public static Task ConnectAsyncTask(this Socket socket, EndPoint endpoint)
        {
            return Task.Factory.FromAsync(
                (ac, o) => socket.BeginConnect(endpoint, ac, o),
                ar =>
                {
                    socket.EndConnect(ar);
                },
                null);
        }

        public static string GetAddress(this EndPoint endpoint)
        {        
            var ipEndpoint = endpoint as IPEndPoint;
            if(ipEndpoint !=null)
            {
                return string.Format("{0}:{1}", ipEndpoint.Address.ToString(), ipEndpoint.Port);
            }
            return "<unknown address>";
        }
    }
}
