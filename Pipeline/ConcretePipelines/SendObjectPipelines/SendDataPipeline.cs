using Networking.Pipeline.PipelineData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Pipeline.ConcretePipelines.SendObjectPipelines
{
    public class SendDataPipeline : NetworkPipeline<ObjectPipelineData>
    {
        public override ObjectPipelineData Procedure(ObjectPipelineData data)
        {
            data.NetworkStream.Write(data.Output, 0, data.Output.Length);

            return data;
        }
    }
}
