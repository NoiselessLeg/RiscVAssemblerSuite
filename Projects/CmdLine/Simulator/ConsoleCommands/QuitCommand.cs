using Assembler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.CmdLine.Simulator.ConsoleCommands
{
   class QuitCommand : IConsoleCommand
   {
      public QuitCommand(RuntimeProcess executive, ITerminal terminal, TerminationManager termMgr)
      {
         m_TermMgr = termMgr;
         m_Exec = executive;
         m_Terminal = terminal;
      }

      public string CommandString => "quit";

      public string CommandStringWithArgs => CommandString + "()";

      public string HelpText
      {
         get
         {
            return CommandStringWithArgs + "-> terminate the process and exit the simulator.";
         }
      }

      public int NumArguments => 0;

      public void Execute(string[] args)
      {
         bool userWishesToContinue = true;
         if (m_Exec.IsRunning || m_Exec.IsPaused)
         {
            m_Terminal.PrintString("A program is currently executing. Do you wish to kill it and exit (y/n)? ");
            string answer = m_Terminal.ReadString().ToLower();
            while (answer != "y" && answer != "n")
            {
               m_Terminal.PrintString("Please respond with either \"y\" or \"n.\" ");
               answer = m_Terminal.ReadString().ToLower();
            }

            if (answer == "n")
            {
               userWishesToContinue = false;
            }
         }

         if (userWishesToContinue)
         {
            m_Exec.Terminate();
            m_TermMgr.Terminate();
         }
         else
         {
            m_Terminal.PrintString("User chose not to exit.\n");
         }
      }

      private readonly TerminationManager m_TermMgr;
      private readonly RuntimeProcess m_Exec;
      private readonly ITerminal m_Terminal;
   }
}
