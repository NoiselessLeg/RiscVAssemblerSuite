using Assembler.Common;
using Assembler.Interpreter.SysCalls;
using Assembler.OutputProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Interpreter.InstructionInterpretation
{
    class EcallInterpreter : IInstructionInterpreter
    {
        public EcallInterpreter(ITerminal terminal)
        {
            m_Terminal = terminal;

            // get all of the system calls in the assembly.
            m_SystemCalls = new Dictionary<int, ISystemCall>();
            var sysCallType = typeof(ISystemCall);
            var availableCalls = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => sysCallType.IsAssignableFrom(p) && p.IsClass && !p.IsAbstract);

            foreach (Type callType in availableCalls)
            {
                var callInstance = (ISystemCall) Activator.CreateInstance(callType);
                m_SystemCalls.Add(callInstance.SystemCallId, callInstance);
            }
        }

        /// <summary>
        /// Performs processing related to a given instruction and its parameters.
        /// </summary>
        /// <param name="argList">The parameter list associated with the instruction.</param>
        /// <param name="registers">The array of 32-bit registers that this instruction may read from/write to.</param>
        /// <param name="dataSegment">An accessor to the program .data segment.</param>
        /// <returns>True if the program counter value is modified in this register and should not be modified otherwise,
        /// false otherwise.</returns>
        public bool InterpretInstruction(RuntimeContext ctx, int[] argList)
        {
            if (argList.Length != 0)
            {
                throw new InvalidOperationException("Malformed ECALL instruction - expected zero parameters; received " + argList.Length);
            }

            ISystemCall sysCall = default(ISystemCall);
            if (!m_SystemCalls.TryGetValue(ctx.RuntimeRegisters[SysCallRegisters.SYSCALL_IDX].Value, out sysCall))
            {
                throw new ArgumentException(ctx.RuntimeRegisters[SysCallRegisters.SYSCALL_IDX].Value + " does not correspond to a valid system call.");
            }

            sysCall.ExecuteSystemCall(m_Terminal, ctx);

            return false;

        }

        private readonly ITerminal m_Terminal;
        private readonly Dictionary<int, ISystemCall> m_SystemCalls;
    }
}
