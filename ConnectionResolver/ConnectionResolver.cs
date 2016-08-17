using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Networking.ConnectionResolver
{
    public class ConnectionResolver : IConnectionResolver
    {
        public bool EnsureClientIsConnected(SocketState state)
        {
            return InternalEnsureClientIsConnected(state.Client.Client);
        }

        public bool EnsureClientIsConnected(TcpClient tcpClient)
        {
            return InternalEnsureClientIsConnected(tcpClient.Client);
        }

        public bool EnsureClientIsConnected(Socket socket)
        {
            return InternalEnsureClientIsConnected(socket);
        }

        private bool InternalEnsureClientIsConnected(Socket socket)
        {
            try
            {
                return !(socket.Poll(100, SelectMode.SelectRead) && socket.Available == 0);
            }
            catch
            {
                return false;
            }
        }

    }
}
