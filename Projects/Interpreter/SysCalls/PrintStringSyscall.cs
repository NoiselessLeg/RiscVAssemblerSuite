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

        public void ExecuteSystemCall(ITerminal terminal, RuntimeContext ctx)
        {
            string dataStr = ctx.ReadString(ctx.UserRegisters[SysCallRegisters.ARG1_IDX].Value);
            terminal.PrintString(dataStr);
        }
    }
}
