using Assembler.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.CmdLine.Simulator.ConsoleCommands
{
   public class PwdCommand : IConsoleCommand
   {
      public PwdCommand(ITerminal terminal)
      {
         m_Terminal = terminal;
      }

      public string CommandString => "pwd";

      public string CommandStringWithArgs => CommandString + "()";

      public string HelpText
      {
         get
         {
            return CommandStringWithArgs + "-> prints the current directory path.";
         }
      }

      public int NumArguments => 0;

      public void Execute(string[] args)
      {
         var directory = Directory.GetCurrentDirectory();
         m_Terminal.PrintString(directory + '\n');
      }

      private readonly ITerminal m_Terminal;
   }
}
