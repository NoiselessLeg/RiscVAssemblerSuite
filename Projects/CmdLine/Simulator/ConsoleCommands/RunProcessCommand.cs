using Assembler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.CmdLine.Simulator.ConsoleCommands
{
   public class RunProcessCommand : IConsoleCommand
   {
      public RunProcessCommand(RuntimeProcess executive, ITerminal terminal)
      {
         m_Exec = executive;
         m_Terminal = terminal;
      }

      public string CommandString => "run";

      public string CommandStringWithArgs => CommandString + "()";

      public string HelpText
      {
         get
         {
            return CommandStringWithArgs + "-> start or restart execution of the program.";
         }
      }

      public int NumArguments => 0;

      public void Execute(string[] args)
      {
         bool userWishesToContinue = true;
         if (m_Exec.IsRunning || m_Exec.IsPaused)
         {
            m_Terminal.PrintString("The program is already executing. Do you wish to restart it (y/n)? ");
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
            m_Exec.StartProgramExecution();
         }
         else
         {
            m_Terminal.PrintString("User chose not to restart.\n");
         }
      }

      private readonly RuntimeProcess m_Exec;
      private readonly ITerminal m_Terminal;
   }
}
