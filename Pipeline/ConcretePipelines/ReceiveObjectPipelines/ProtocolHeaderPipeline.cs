using Networking.Pipeline.PipelineData;
using Networking.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Pipeline.ConcretePipelines.ReceiveObjectPipelines
{
    public class ProtocolHeaderPipeline : NetworkPipeline<BytePipelineData>
    {
        public override BytePipelineData Procedure(BytePipelineData data)
        {
            //How much data we want to request. Initially the size of the header.
            int requestData = ProtocolHeader.HEADER_LENGTH;

            while (data.BytesRead < ProtocolHeader.HEADER_LENGTH)
            {
                byte[] buffer = InternalReadData(data.NetworkStream, requestData);
                data.FullData.AddRange(buffer);
                data.BytesRead += buffer.Length;

                //Decrement the request data with the received data.
                //This guards taking excessive data from the stream if the first InternalReadData request fails.
                requestData -= buffer.Length;
            }

            if (!ProtocolHeader.TryGetHeader(data.ToArray(), out data.Header))
            {
                throw new Exception("Couldn't parse data header.");
            }

            return data;
        }
    }
}
