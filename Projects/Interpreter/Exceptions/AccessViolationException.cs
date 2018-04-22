using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Interpreter.Exceptions
{
    class AccessViolationException : Exception
    {
        public AccessViolationException(string why):
            base(why)
        {

        }
    }
}
