using Assembler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.CmdLine.Simulator.ConsoleCommands
{
   class FormatAndDumpMemoryCommand : IConsoleCommand
   {
      public FormatAndDumpMemoryCommand(RuntimeProcess proc, ITerminal terminal)
      {
         m_Proc = proc;
         m_Terminal = terminal;
      }

      public string CommandString => "dumpMemory";

      public string CommandStringWithArgs => CommandString + "(<format string>)";

      public int NumArguments => 1;

      public string HelpText
      {
         get
         {
            return CommandStringWithArgs + " -> dumps the contents of the current memory segment to the console.";
         }
      }

      public void Execute(string[] args)
      {
         string fmtString = args[0];

         if (fmtString.Contains("\""))
         {
            fmtString = fmtString.Replace("\"", string.Empty);
         }

         fmtString = fmtString.Trim();

         m_Proc.DumpMemorySegment(fmtString);
      }

      private readonly RuntimeProcess m_Proc;
      private readonly ITerminal m_Terminal;
   }
}

