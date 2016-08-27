using Networking.Pipeline.PipelineData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Pipeline.ConcretePipelines.SendObjectPipelines
{
    public class AppendHeaderPipeline : NetworkPipeline<ObjectPipelineData>
    {
        public override ObjectPipelineData Procedure(ObjectPipelineData data)
        {
            byte[] bytes = _protocolHandler.PreProcess(data.Output);

            data.Output = bytes;

            return data;
        }
    }
}
