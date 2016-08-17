﻿using Networking.Pipeline.PipelineData;
using Networking.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Pipeline.ConcretePipelines
{
    public class DeserializeDataPipeline : NetworkPipeline<BytePipelineData>
    {
        public override BytePipelineData Procedure(BytePipelineData data)
        {
            byte[] finalData = data.ToArray();

            finalData = _protocolHandler.PostProcess(finalData);

            object result = _serializer.Deserialize(finalData);

            data.Result = result;

            return data;
        }
    }
}
