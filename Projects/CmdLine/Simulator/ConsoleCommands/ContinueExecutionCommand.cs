using Assembler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.CmdLine.Simulator.ConsoleCommands
{
   class ContinueExecutionCommand : IConsoleCommand
   {
      public ContinueExecutionCommand(RuntimeProcess executive, ITerminal terminal)
      {
         m_Exec = executive;
         m_Terminal = terminal;
      }

      public string CommandString => "continue";

      public string CommandStringWithArgs => CommandString + "()";

      public string HelpText
      {
         get
         {
            return CommandStringWithArgs + "-> continue execution of the program.";
         }
      }

      public int NumArguments => 0;

      public void Execute(string[] args)
      {
         if (!m_Exec.IsRunning)
         {
            m_Terminal.PrintString("Program is not running.\n");
            return;
         }

         m_Exec.ResumeExecution();
      }

      private readonly RuntimeProcess m_Exec;
      private readonly ITerminal m_Terminal;
   }
}
