using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comida
{
    class NoMoreOrdersException : Exception
    {
        public NoMoreOrdersException(string message) : base(message) { }
    }
}
