using Networking.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using Networking.Exceptions;
using Networking.ConnectionResolver;

namespace Networking
{
    //TODO: change to sync
    [Obsolete]
    public class FullPacketRetriever
    {
        //Assuming that on one recieve callback we retrieve 1024 bytes of data
        //looping 1024 times at maximum, means we can retrieve 1024x1024 bytes of data
        //wich is 1 mb.
        public readonly byte[] CONNECTION_ERROR = new byte[1] { BufferInfo.CLIENT_DISCONNECTED };

        private int MAX_DATA_RECEIVE_GUARD = 2048;
        private int MaxMegabyteRetrieval { get { return (1024 * MAX_DATA_RECEIVE_GUARD) / 1000000; } }

        private IProtocolHandler _protocolHandler;
        private IConnectionResolver _connectionResolver;
        private List<byte> _fullData = new List<byte>();
        private byte[] _localStreamBuffer = new byte[BufferInfo.MAX_BUFFER_SIZE];

        public FullPacketRetriever(IProtocolHandler protocolHandler, IConnectionResolver connectionResolver)
        {
            _protocolHandler = protocolHandler;
            _connectionResolver = connectionResolver;
        }

        /// <summary>
        /// Returns an array with one element 0x00 if the client is disconnected.
        /// Use 
        /// </summary>
        public byte[] RetrievePacketWithPostProcessing(IAsyncResult ar, SocketState state)
        {
            byte[] resultData = RetrievePacket(ar, state);

            return _protocolHandler.PostProcess(resultData);
        }

        /// <summary>
        /// Returns an array with one element 0x00 if the client is disconnected.
        /// </summary>
        public byte[] RetrievePacket(IAsyncResult ar, SocketState state)
        {
            int asd = MaxMegabyteRetrieval;
            //Here stops the asynchronous recieve
            int dataLength = state.Client.Client.EndReceive(ar);
            byte[] bufferData = new byte[dataLength];

            Buffer.BlockCopy(state.Buffer, 0, bufferData, 0, dataLength);
            _fullData.AddRange(bufferData);

            //Get the packet length
            ProtocolHeader header;
            if (!ProtocolHeader.TryGetHeader(bufferData, out header))
            {
                
            }

            //Index for packet retrieval, start from the first retrieved ones.
            long packetIndex = bufferData.Length;

            //Index for maximum data retrieval
            int i = 0;

            //Check if the data is full, if not loop while data is being send, but non-asynchronously.
            while (packetIndex < header.DataLength)
            {
                if (i > MAX_DATA_RECEIVE_GUARD)
                {
                    throw new MaximumTransferSizeReachedException(string.Format("More than {0}mb of data transfer or non-valid data reached the endpoint.", MaxMegabyteRetrieval));
                }
                i++;

                state.ResetBuffer();
                int recieveLength = state.Client.Client.Receive(state.Buffer, 0, state.Buffer.Length, SocketFlags.None);

                packetIndex += recieveLength;

                bufferData = new byte[recieveLength];
                Buffer.BlockCopy(state.Buffer, 0, bufferData, 0, recieveLength);

                _fullData.AddRange(bufferData);
            }

            byte[] resultData = _fullData.ToArray();

            ResetBuffers();
            return resultData;
        }

        public void SetMaximumRetrieveMegabytes(int megabytes)
        {
            if (megabytes <= 0 )
            {
                throw new ArgumentException("Maximum data retrieval cannot be less or equal to zero.");
            }
            else if (megabytes >= 1024)
            {
                throw new ArgumentException("Maximum data retrieval cannot be more than 1GB.");
            }

            MAX_DATA_RECEIVE_GUARD = 1024 * megabytes;
        }

        private void ResetBuffers()
        {
            _fullData.Clear();
            _localStreamBuffer = new byte[BufferInfo.MAX_BUFFER_SIZE];
        }
    }
}
