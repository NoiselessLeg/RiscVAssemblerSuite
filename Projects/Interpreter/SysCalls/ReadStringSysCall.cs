using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assembler.Common;

namespace Assembler.Interpreter.SysCalls
{
    class ReadStringSysCall : ISystemCall
    {
        public int SystemCallId
        {
            get
            {
                return 8;
            }
        }

        public void ExecuteSystemCall(ITerminal terminal, RuntimeContext ctx)
        {
            string str = terminal.ReadString();
            ctx.WriteString(ctx.UserRegisters[SysCallRegisters.ARG1_IDX].Value, str);
        }
    }
}
