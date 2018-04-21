using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Interpreter
{
    /// <summary>
    /// Defines a class that acts as an environment harness for the interpreter.
    /// </summary>
    public interface IExecutionEnvironment
    {
        /// <summary>
        /// Requests that the environment implementation terminates the application runtime.
        /// </summary>
        void Terminate();
    }
}
