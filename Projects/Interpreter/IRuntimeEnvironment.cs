using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Interpreter
{
    /// <summary>
    /// Defines a class that simulates a RISC-V runtime environment.
    /// </summary>
    public interface IRuntimeEnvironment
    {
        /// <summary>
        /// Terminates the current running program.
        /// </summary>
        void Terminate();

        /// <summary>
        /// Allocates memory from the heap
        /// </summary>
        byte[] AllocMemory();
    }
}
