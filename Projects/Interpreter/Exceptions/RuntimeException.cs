using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Interpreter.Exceptions
{
    class RuntimeException : Exception
    {
        public RuntimeException(string why):
            base(why)
        {

        }
    }
}
