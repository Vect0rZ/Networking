using Networking.Pipeline.PipelineData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Pipeline.ConcretePipelines.SendObjectPipelines
{
    public class SerializeDataPipeline : NetworkPipeline<ObjectPipelineData>
    {
        public override ObjectPipelineData Procedure(ObjectPipelineData data)
        {
            byte[] bytes = _stringSerializer.Serialize(data.Input);
            data.Output = bytes;

            return data;
        }
    }
}
