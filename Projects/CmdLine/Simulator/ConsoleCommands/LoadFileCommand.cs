using Assembler.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.CmdLine.Simulator.ConsoleCommands
{
   public class LoadFileCommand : IConsoleCommand
   {
      public LoadFileCommand(RuntimeProcess executive, ITerminal terminal, TerminationManager termMgr)
      {
         m_TermMgr = termMgr;
         m_Exec = executive;
         m_Terminal = terminal;
      }

      public string CommandString => "load";

      public string CommandStringWithArgs => CommandString + "(<assembled file path>)";

      public string HelpText
      {
         get
         {
            return CommandStringWithArgs + "-> exits the current process and loads a new file.";
         }
      }

      public int NumArguments => 1;

      public void Execute(string[] args)
      {
         // first, determine if the file even exists.
         bool fileExists = File.Exists(args[0]);
         bool isValidFileExtension = IsValidFileExtension(args[0]);
         if (fileExists && isValidFileExtension)
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
               m_TermMgr.RestartSimulator(args[0]);
            }
            else
            {
               m_Terminal.PrintString("User chose not to exit.\n");
            }
         }
         else if (!fileExists)
         {
            m_Terminal.PrintString("Could not find file \"" + args[0] + "\"\n");
         }
         else if (!isValidFileExtension)
         {
            m_Terminal.PrintString('\"' + args[0] + "\" is not a valid RISC-V JEF or object file.\n");
         }
      }

      private bool IsValidFileExtension(string filePath)
      {
         return filePath.EndsWith(".jef") || filePath.EndsWith(".o");
      }

      private readonly TerminationManager m_TermMgr;
      private readonly RuntimeProcess m_Exec;
      private readonly ITerminal m_Terminal;
   }
}
