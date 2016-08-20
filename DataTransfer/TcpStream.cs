using Networking.Pipeline;
using Networking.Pipeline.ConcretePipelines;
using Networking.Pipeline.PipelineData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Networking.DataTransfer
{
    public class TcpStream
    {
        private NetworkStream _stream;
        private PipelineProcessor<BytePipelineData> _receivePipelineProcessor;
        private PipelineProcessor<ObjectPipelineData> _sendPipelineProcessor;

        /// <summary>
        /// Constructs a TcpStream by given network stream.
        /// The receiver does not close the stream by any means.
        /// Any closure of the stream should be handled manually.
        /// </summary>
        public TcpStream(NetworkStream tcpStream)
        {
            _stream = tcpStream;
            InitializePipeline();
        }

        //TODO: check what happens with the remaining buffer data in the tcp stream when the header is not present. 
        /// <summary>
        /// Blocking receive.
        /// This method obtains an incoming serialized object with included Protocol Header.
        /// If a protocol header is not present returns null.
        /// </summary>
        /// <returns></returns>
        public object Receive()
        {
            BytePipelineData data = new BytePipelineData(_stream);

            try
            {
                _receivePipelineProcessor.Process(data);
            }
            catch
            {
                return null;
            }

            return data.Result;
        }

        /// <summary>
        /// Serializes the object, appends the protocol header and sends the data to the receiver.
        /// </summary>
        /// <param name="object"></param>
        public void Send(object @object)
        {
            ObjectPipelineData data = new ObjectPipelineData(_stream, @object);

            _sendPipelineProcessor.Process(data);
        }

        private void InitializePipeline()
        {
            _receivePipelineProcessor = new PipelineProcessor<BytePipelineData>();
            _receivePipelineProcessor.Link(new ProtocolHeaderPipeline());
            _receivePipelineProcessor.Link(new DataPacketPipeline());
            _receivePipelineProcessor.Link(new DeserializeDataPipeline());

            _sendPipelineProcessor = new PipelineProcessor<ObjectPipelineData>();
            _sendPipelineProcessor.Link(new SerializeDataPipeline());
            _sendPipelineProcessor.Link(new AppendHeaderPipeline());
            _sendPipelineProcessor.Link(new SendDataPipeline());
        }
    }
}
