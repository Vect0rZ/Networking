using Networking.Pipeline.PipelineData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Pipeline.ConcretePipelines
{
    public class SerializeDataPipeline : NetworkPipeline<ObjectPipelineData>
    {
        public override ObjectPipelineData Procedure(ObjectPipelineData data)
        {
            byte[] bytes = _serializer.Serialize(data.Input);
            data.Output = bytes;

            return data;
        }
    }
}
