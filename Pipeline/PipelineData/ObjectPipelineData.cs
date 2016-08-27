using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Pipeline.PipelineData
{
    public class ObjectPipelineData : IPipelineData
    {
        public NetworkStream NetworkStream;

        public string Input;

        public byte[] Output;

        public ObjectPipelineData(NetworkStream ns, string data)
        {
            NetworkStream = ns;
            Input = data;
        }
    }
}
