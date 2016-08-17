using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Protocol
{
    public class ProtocolHeader
    {
        public const int HEADER_LENGTH = 12;
        public long DataLength { get; set; }
        public long RawDataLength { get; set; }

        public ProtocolHeader(long dataLength)
        {
            DataLength = dataLength;
            RawDataLength = dataLength + HEADER_LENGTH;
        }

        public byte[] ToByteArray()
        {
            byte[] headerLength = BitConverter.GetBytes(HEADER_LENGTH);
            byte[] dataLength = BitConverter.GetBytes(DataLength);
            byte[] result = new byte[HEADER_LENGTH];

            if (headerLength.Length != BufferInfo.INT32_SIZE)
                throw new Exception("Something went wrong with converting the header length.");
            if (dataLength.Length != BufferInfo.INT64_SIZE)
                throw new Exception("Something went wrong with converting the data length.");

            Buffer.BlockCopy(headerLength, 0, result, 0, headerLength.Length);
            Buffer.BlockCopy(dataLength, 0, result, BufferInfo.INT32_SIZE, dataLength.Length);

            return result;
        }

        public static bool TryGetHeader(byte[] data, out ProtocolHeader header)
        {
            header = null;

            if (data.Length < HEADER_LENGTH)
                return false;

            byte[] headerLength = new byte[BufferInfo.INT32_SIZE];
            byte[] dataLength = new byte[BufferInfo.INT64_SIZE];

            Buffer.BlockCopy(data, 0, headerLength, 0, BufferInfo.INT32_SIZE);
            Buffer.BlockCopy(data, BufferInfo.INT32_SIZE, dataLength, 0, BufferInfo.INT64_SIZE);

            int headerLengthInt = BitConverter.ToInt32(headerLength, 0);
            long dataLengthInt = BitConverter.ToInt64(dataLength, 0);

            if (headerLengthInt != HEADER_LENGTH)
                return false;

            header = new ProtocolHeader(dataLengthInt);

            return true;
        }
    }
}
