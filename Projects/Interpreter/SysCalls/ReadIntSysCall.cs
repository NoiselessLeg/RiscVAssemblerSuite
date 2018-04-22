using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assembler.Common;

namespace Assembler.Interpreter.SysCalls
{
    class ReadIntSysCall : ISystemCall
    {
        public int SystemCallId
        {
            get
            {
                return 5;
            }
        }

        public void ExecuteSystemCall(IRuntimeEnvironment runtimeEnv, ITerminal terminal, Register[] registers, RuntimeDataSegmentAccessor dataSegment)
        {
            registers[SysCallRegisters.SYSCALL_IDX].Value = terminal.ReadInt();
        }
    }
}
