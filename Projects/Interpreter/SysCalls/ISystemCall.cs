using Assembler.Common;

namespace Assembler.Interpreter.SysCalls
{
    /// <summary>
    /// Defines a class that performs a system call. These implementations
    /// can request actions of the runtime environment, can output data to a terminal,
    /// or can request data from a terminal.
    /// </summary>
    public interface ISystemCall
    {
        /// <summary>
        /// Gets the ID associated with the system call.
        /// </summary>
        int SystemCallId { get; }

        /// <summary>
        /// Performs the execution of a system call.
        /// </summary>
        /// <param name="terminal">The terminal window which can perform user I/O.</param>
        /// <param name="ctx">The runtime environment context that can perform operating system
        /// calls on behalf of the assembled child program.</param>
        void ExecuteSystemCall(ITerminal terminal, RuntimeContext ctx);

    }
}
