using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assembler.Common;

namespace Assembler.Interpreter.SysCalls
{
    class PrintIntSysCall : ISystemCall
    {
        public int SystemCallId
        {
            get
            {
                return 1;
            }
        }

        public void ExecuteSystemCall(ITerminal terminal, RuntimeContext ctx)
        {
            terminal.PrintInt(ctx.RuntimeRegisters[SysCallRegisters.ARG1_IDX].Value);
        }
    }
}
