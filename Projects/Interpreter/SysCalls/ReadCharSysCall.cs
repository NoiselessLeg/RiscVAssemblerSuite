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

        public void ExecuteSystemCall(ITerminal terminal, RuntimeContext ctx)
        {
            ctx.RuntimeRegisters[SysCallRegisters.SYSCALL_IDX].Value = terminal.ReadChar();
        }
    }
}
