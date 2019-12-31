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
    public class InterpreterCommon
    {
        /// <summary>
        /// Defines how many registers are used in the "processor."
        /// </summary>
        public const int MAX_REGISTERS = 33;

        /// <summary>
        /// Defines how many privileged-level registers are used in the "processor."
        /// </summary>
        public const int MAX_PRIVILEGED_REGISTERS = 32;

        /// <summary>
        /// Defines the index of the program counter register.
        /// </summary>
        public const int PC_REGISTER = MAX_REGISTERS - 1;

        /// <summary>
        /// Defines the default location for the stack pointer.
        /// </summary>
        public const int DEFAULT_STACK_POINTER = 0x7fffeffc;

        /// <summary>
        /// The default base offset for the stack memory subsection.
        /// </summary>
        public const int STACK_BASE_OFFSET = 0x7ffffffc;

        /// <summary>
        /// The default base offset for the heap subsection.
        /// </summary>
        public const int HEAP_BASE_OFFSET = 0x10040000;

        /// <summary>
        /// Defines the stack pointer register index.
        /// </summary>
        public const int SP_REGISTER = 2;

        /// <summary>
        /// Defines the maximum allocation size for a specific segment in the simulator (4MiB).
        /// </summary>
        public const int MAX_SEGMENT_SIZE = 4194304;
    }
}
