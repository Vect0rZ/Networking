using Networking;
using Networking.Pipeline.PipelineData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Pipeline.ConcretePipelines
{
    public class DataPacketPipeline : NetworkPipeline<BytePipelineData>
    {
        public override BytePipelineData Procedure(BytePipelineData data)
        {
            //While the data is not full
            while (data.BytesRead < data.Header.RawDataLength)
            {
                //Get the difference so we can make sure we don't get
                //byte from the next arriving packet after the whole object.
                long diff = data.Header.RawDataLength - (long)data.BytesRead;
                if (diff > BufferInfo.MAX_BUFFER_SIZE)
                {
                    byte[] buffer = InternalReadData(data.NetworkStream);
                    data.FullData.AddRange(buffer);
                    data.BytesRead += buffer.Length;
                }
                else
                {
                    byte[] buffer = InternalReadData(data.NetworkStream, (int)diff);
                    data.FullData.AddRange(buffer);
                    data.BytesRead += buffer.Length;
                }
            }

            return data;
        }
    }
}
