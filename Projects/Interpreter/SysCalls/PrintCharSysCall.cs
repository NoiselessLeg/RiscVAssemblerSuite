using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assembler.Common;

namespace Assembler.Interpreter.SysCalls
{
    class PrintCharSysCall : ISystemCall
    {
        public int SystemCallId
        {
            get
            {
                return 11;
            }
        }

        public void ExecuteSystemCall(IRuntimeEnvironment runtimeEnv, ITerminal terminal, Register[] registers, RuntimeDataSegmentAccessor dataSegment)
        {
            terminal.PrintChar((char)(registers[SysCallRegisters.ARG1_IDX].Value & 0xFF));
        }
    }
}
