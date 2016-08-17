using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Serialization
{
    public class ObjectSerializer : IObjectSerializer
    {
        public byte[] Serialize(object @object)
        {
            byte[] result;
            BinaryFormatter formatter = new BinaryFormatter();

            using (MemoryStream stream = new MemoryStream())
            {
                try
                {
                    formatter.Serialize(stream, @object);
                }
                catch(SerializationException e)
                {
                    throw new SerializationException("The object is not serializable. Use attribute [Serializalbe].", e);
                }

                result = stream.ToArray();
                stream.Flush();
                stream.Close();
            }

            return result;
        }

        public object Deserialize(byte[] @object)
        {
            object result;
            BinaryFormatter formatter = new BinaryFormatter();

            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(@object, 0, @object.Length);
                stream.Position = 0;
                stream.Seek(0, SeekOrigin.Begin);
                try
                {
                    stream.Position = 0;
                    result = formatter.Deserialize(stream);
                }
                catch (SerializationException e)
                {
                    throw new SerializationException("The object is not deserializalbe. Use attribute [Serializalbe].", e);
                }
                stream.Flush();
                stream.Close();
            }

            return result;
        }
    }
}
