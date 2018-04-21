using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Interpreter
{
    /// <summary>
    /// Defines constants common to the interpreter classes.
    /// </summary>
    class InterpreterCommon
    {
        /// <summary>
        /// Defines how many registers are used in the "processor."
        /// </summary>
        public const int MAX_REGISTERS = 33;

        /// <summary>
        /// Defines the index of the program counter register.
        /// </summary>
        public const int PC_REGISTER = MAX_REGISTERS - 1;
    }
}
