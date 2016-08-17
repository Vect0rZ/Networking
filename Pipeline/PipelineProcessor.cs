using Networking.Pipeline.PipelineData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Pipeline
{
    public class PipelineProcessor<T> where T : IPipelineData
    {
        private List<Pipeline<T>> _pipelines;

        public PipelineProcessor()
        {
            _pipelines = new List<Pipeline<T>>();
        }

        public void Link(Pipeline<T> pipeline)
        {
            _pipelines.Add(pipeline);
            int count = _pipelines.Count;

            if (count > 1)
            {
                _pipelines[count - 2].Link(_pipelines[count - 1]);
            }
        }

        public T Process(T input)
        {
            T result = _pipelines[0].Process(input);

            return result;
        }
    }
}
