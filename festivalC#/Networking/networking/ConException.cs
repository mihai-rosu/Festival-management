using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking.networking
{
    public class ConException : Exception
    {
        public ConException() : base() { }

        public ConException(String msg) : base(msg) { }

        public ConException(String msg, Exception ex) : base(msg, ex) { }

    }
}
