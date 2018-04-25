using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assembler.Common;

namespace Assembler.Interpreter.SysCalls
{
    class TerminateSysCall : ISystemCall
    {
        public int SystemCallId
        {
            get
            {
                return 10;
            }
        }

        public void ExecuteSystemCall(ITerminal terminal, RuntimeContext ctx)
        {
            ctx.TerminateApplication();
        }
    }
}
