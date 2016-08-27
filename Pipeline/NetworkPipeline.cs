using Networking;
using Networking.Pipeline.PipelineData;
using Networking.Protocol;
using Networking.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Pipeline
{
    public abstract class NetworkPipeline<T> : Pipeline<T> where T : IPipelineData
    {
        protected IProtocolHandler _protocolHandler = new ProtocolHandler();
        protected IObjectSerializer _objectSerializer = new ObjectSerializer();
        protected IStringSerializer _stringSerializer = new StringSerializer();

        public override abstract T Procedure(T data);

        protected byte[] InternalReadData(NetworkStream ns, int treshHold = BufferInfo.MAX_BUFFER_SIZE)
        {
            byte[] bufferData = new byte[BufferInfo.MAX_BUFFER_SIZE];
            int bytesRead = ns.Read(bufferData, 0, treshHold);

            byte[] data = new byte[bytesRead];
            Buffer.BlockCopy(bufferData, 0, data, 0, bytesRead);

            return data;
        }
    }
}
