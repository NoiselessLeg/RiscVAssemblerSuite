﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Interpreter.Exceptions
{
    class AccessViolationException : RuntimeException
    {
        public AccessViolationException(string why):
            base(why)
        {

        }
    }
}
