using Assembler.Common;
using Assembler.Interpreter.Exceptions;
using System;

namespace Assembler.Interpreter.SysCalls
{
   internal class ReadIntSysCall : ISystemCall
   {
      public int SystemCallId => 5;

      public void ExecuteSystemCall(ITerminal terminal, RuntimeContext ctx)
      {
         ctx.UserRegisters[SysCallRegisters.SYSCALL_IDX].Value = terminal.ReadInt();
      }
   }
}
