using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Networking
{
    public class SocketState
    {
        public TcpClient Client { get; set; }

        public byte[] Buffer = new byte[BufferInfo.MAX_BUFFER_SIZE];

        public void ResetBuffer()
        {
            Buffer = new byte[BufferInfo.MAX_BUFFER_SIZE];
        }
    }
}
