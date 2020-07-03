using Assembler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.CmdLine.Simulator.ConsoleCommands
{
   public class GenericHelpCommand : IConsoleCommand
   {
      public GenericHelpCommand(CommandTable cmdTable, ITerminal terminal)
      {
         m_CmdTable = cmdTable;
         m_Terminal = terminal;
      }

      public string CommandString => "help";

      public string CommandStringWithArgs => CommandString + "()";

      public int NumArguments => 0;

      public string HelpText
      {
         get
         {
            return CommandStringWithArgs + "-> displays this help screen.";
         }
      }

      public void Execute(string[] args)
      {
         foreach (var cmd in m_CmdTable.AllCommands)
         {
            string helpText = cmd.HelpText;
            m_Terminal.PrintString("\t" + helpText + '\n');
         }
      }

      private readonly CommandTable m_CmdTable;
      private readonly ITerminal m_Terminal;

   }
}
