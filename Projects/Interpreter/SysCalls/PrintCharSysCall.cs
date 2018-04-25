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

        public void ExecuteSystemCall(ITerminal terminal, RuntimeContext ctx)
        {
            terminal.PrintChar((char)(ctx.UserRegisters[SysCallRegisters.ARG1_IDX].Value & 0xFF));
        }
    }
}
