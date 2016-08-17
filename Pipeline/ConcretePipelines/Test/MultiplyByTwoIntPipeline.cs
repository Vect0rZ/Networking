using Networking.Pipeline.PipelineData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Pipeline.ConcretePipelines.Test
{
    public class MultiplyByTwoIntPipeline : Pipeline<IntPipelineData>
    {
        public override IntPipelineData Procedure(IntPipelineData data)
        {
            data.Data *= 2;
            return data;
        }
    }
}
