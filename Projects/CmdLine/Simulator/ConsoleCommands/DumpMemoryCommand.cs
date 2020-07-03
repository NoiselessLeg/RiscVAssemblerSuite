using Assembler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.CmdLine.Simulator.ConsoleCommands
{
   class DumpMemoryCommand : IConsoleCommand
   {
      public DumpMemoryCommand(RuntimeProcess proc, ITerminal terminal)
      {
         m_Proc = proc;
         m_Terminal = terminal;
      }

      public string CommandString => "dumpMemory";

      public string CommandStringWithArgs => CommandString + "()";

      public int NumArguments => 0;

      public string HelpText
      {
         get
         {
            return CommandStringWithArgs + " -> dumps the contents of the current memory segment to the console.";
         }
      }

      public void Execute(string[] args)
      {
         m_Proc.DumpMemorySegment();
      }

      private readonly RuntimeProcess m_Proc;
      private readonly ITerminal m_Terminal;
   }
}
