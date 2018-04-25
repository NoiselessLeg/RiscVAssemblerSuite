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

        public void ExecuteSystemCall(ITerminal terminal, RuntimeContext ctx)
        {
            int newAddress = ctx.Sbrk(ctx.RuntimeRegisters[SysCallRegisters.ARG1_IDX].Value);
            ctx.RuntimeRegisters[SysCallRegisters.SYSCALL_IDX].Value = newAddress;
        }
    }
}
