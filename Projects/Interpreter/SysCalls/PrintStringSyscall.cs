using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assembler.Common;

namespace Assembler.Interpreter.SysCalls
{
    class PrintStringSysCall : ISystemCall
    {
        public int SystemCallId
        {
            get
            {
                return 4;
            }
        }

        public void ExecuteSystemCall(IRuntimeEnvironment runtimeEnv, ITerminal terminal, Register[] registers, RuntimeDataSegmentAccessor dataSegment)
        {
            string dataStr = dataSegment.ReadString(registers[SysCallRegisters.ARG1_IDX].Value);
            terminal.PrintString(dataStr);
        }
    }
}
