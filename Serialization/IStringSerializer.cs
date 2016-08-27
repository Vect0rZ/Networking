using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Serialization
{
    public interface IStringSerializer
    {
        byte[] Serialize(string @object);

        string Deserialize(byte[] @object);
    }
}
