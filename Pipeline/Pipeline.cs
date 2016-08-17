using Networking.Pipeline.PipelineData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Pipeline
{
    public abstract class Pipeline<T> where T : IPipelineData
    {
        private Pipeline<T> _next;
        private T Input;

        public abstract T Procedure(T data);

        public virtual T Process(T data)
        {
            Input = Procedure(data);

            if (_next != null)
                return _next.Process(Input);

            return Input;
        }

        public virtual void Link(Pipeline<T> pipeline)
        {
            _next = pipeline;
        }
    }
}
