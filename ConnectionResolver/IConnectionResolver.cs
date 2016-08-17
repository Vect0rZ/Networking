using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Networking.ConnectionResolver
{
    public interface IConnectionResolver
    {
        bool EnsureClientIsConnected(SocketState socket);
        bool EnsureClientIsConnected(TcpClient socket);
        bool EnsureClientIsConnected(Socket socket);
    }
}
