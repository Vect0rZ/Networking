using Networking.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Pipeline.PipelineData
{
    public class BytePipelineData : IPipelineData
    {
        public NetworkStream NetworkStream;
        public List<byte> FullData;
        public int BytesRead;
        public ProtocolHeader Header;
        public string Result;

        public BytePipelineData(NetworkStream ns)
        {
            NetworkStream = ns;
            FullData = new List<byte>();
            BytesRead = 0;
        }

        public byte[] ToArray()
        {
            return FullData.ToArray();
        }
    }
}
