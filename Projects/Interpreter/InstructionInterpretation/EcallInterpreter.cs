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
        public EcallInterpreter(IRuntimeEnvironment env, ITerminal terminal)
        {
            m_Terminal = terminal;
            m_Environment = env;

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
        public bool InterpretInstruction(int[] argList, Register[] registers, RuntimeDataSegmentAccessor dataSegment)
        {
            if (argList.Length != 0)
            {
                throw new InvalidOperationException("Malformed ECALL instruction - expected zero parameters; received " + argList.Length);
            }

            ISystemCall sysCall = default(ISystemCall);
            if (!m_SystemCalls.TryGetValue(registers[SysCallRegisters.SYSCALL_IDX].Value, out sysCall))
            {
                throw new ArgumentException(registers[SysCallRegisters.SYSCALL_IDX].Value + " does not correspond to a valid system call.");
            }

            sysCall.ExecuteSystemCall(m_Environment, m_Terminal, registers, dataSegment);

            return false;

        }

        private readonly ITerminal m_Terminal;
        private readonly IRuntimeEnvironment m_Environment;

        private readonly Dictionary<int, ISystemCall> m_SystemCalls;
    }
}
