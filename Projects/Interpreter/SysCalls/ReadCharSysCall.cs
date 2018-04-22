using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assembler.Common;

namespace Assembler.Interpreter.SysCalls
{
    class ReadCharSysCall : ISystemCall
    {
        public int SystemCallId
        {
            get
            {
                return 12;
            }
        }

        public void ExecuteSystemCall(IRuntimeEnvironment runtimeEnv, ITerminal terminal, Register[] registers, RuntimeDataSegmentAccessor dataSegment)
        {
            registers[SysCallRegisters.SYSCALL_IDX].Value = terminal.ReadChar();
        }
    }
}
