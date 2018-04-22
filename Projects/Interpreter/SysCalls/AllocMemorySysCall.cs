using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assembler.Common;

namespace Assembler.Interpreter.SysCalls
{
    class AllocMemorySysCall : ISystemCall
    {
        public int SystemCallId
        {
            get
            {
                return 9;
            }
        }

        public void ExecuteSystemCall(IRuntimeEnvironment runtimeEnv, ITerminal terminal, Register[] registers, RuntimeDataSegmentAccessor dataSegment)
        {
            int newAddress = dataSegment.Sbrk(registers[SysCallRegisters.ARG1_IDX].Value);
            registers[SysCallRegisters.SYSCALL_IDX].Value = newAddress;
        }
    }
}
