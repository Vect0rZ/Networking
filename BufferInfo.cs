using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking
{
    public static class BufferInfo
    {
        public const int INT32_SIZE = 4;
        public const int INT64_SIZE = 8;
        public const int MAX_BUFFER_SIZE = 1024;
        public const byte CLIENT_DISCONNECTED = 0x00;
    }
}
