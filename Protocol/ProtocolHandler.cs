using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Protocol
{
    public class ProtocolHandler : IProtocolHandler
    {
        public byte[] PreProcess(byte[] data)
        {
            ProtocolHeader header = new ProtocolHeader(data.Length);
            byte[] headerBytes = header.ToByteArray();
            byte[] processedData = new byte[data.Length + ProtocolHeader.HEADER_LENGTH];

            Buffer.BlockCopy(headerBytes, 0, processedData, 0, headerBytes.Length);
            Buffer.BlockCopy(data, 0, processedData, ProtocolHeader.HEADER_LENGTH, data.Length);

            return processedData;
        }

        public byte[] PostProcess(byte[] data)
        {
            byte[] result = new byte[data.Length - ProtocolHeader.HEADER_LENGTH];

            Buffer.BlockCopy(data, ProtocolHeader.HEADER_LENGTH, result, 0, result.Length);

            return result;
        }

        public long GetRawPacketLength(byte[] rawData)
        {
            long result = rawData.Length - ProtocolHeader.HEADER_LENGTH;

            return result;
        }
    }
}
