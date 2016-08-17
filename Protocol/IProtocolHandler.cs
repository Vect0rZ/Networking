using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Protocol
{
    public interface IProtocolHandler
    {
        byte[] PreProcess(byte[] data);
        byte[] PostProcess(byte[] data);
        long GetRawPacketLength(byte[] rawData);
    }
}
