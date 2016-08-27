using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Serialization
{
    public class StringSerializer : IStringSerializer
    {
        public string Deserialize(byte[] @object)
        {
            string result = UTF8Encoding.UTF8.GetString(@object);

            return result;
        }

        public byte[] Serialize(string @object)
        {
            byte[] result = UTF8Encoding.UTF8.GetBytes(@object);

            return result;
        }
    }
}
