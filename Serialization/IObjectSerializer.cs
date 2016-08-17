using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Serialization
{
    public interface IObjectSerializer : ISerializer
    {
        byte[] Serialize(object @object);

        object Deserialize(byte[] @object);
    }
}
