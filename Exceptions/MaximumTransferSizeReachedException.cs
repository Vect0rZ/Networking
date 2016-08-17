using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Exceptions
{
    public class MaximumTransferSizeReachedException : Exception
    {
        public MaximumTransferSizeReachedException(string message) : base(message)
        {

        }

        public MaximumTransferSizeReachedException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
