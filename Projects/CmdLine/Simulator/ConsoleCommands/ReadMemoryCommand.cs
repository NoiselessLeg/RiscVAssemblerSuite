using Assembler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.CmdLine.Simulator.ConsoleCommands
{
   public class ReadMemoryCommand : IConsoleCommand
   {

      public ReadMemoryCommand(RuntimeProcess proc, ITerminal terminal)
      {
         m_Proc = proc;
         m_Terminal = terminal;
      }
      public string CommandString => "readMemory";

      public string CommandStringWithArgs => CommandString + "(<address>)";

      public string HelpText => CommandStringWithArgs + " -> reads a 32-bit word from an address in memory.";

      public int NumArguments => 1;

      public void Execute(string[] args)
      {

         try
         {

            if (IntExtensions.TryParseEx(args[0], out int iValue))
            {
               int readData = m_Proc.ReadMemory(iValue);
               m_Terminal.PrintString("\t" + args[0] + " = " + readData + '\n');
            }
            else
            {
               m_Terminal.PrintString(args[0] + " was not a valid 32 bit integer.\n");
            }
         }
         catch (Exception ex)
         {
            m_Terminal.PrintString(ex.Message + '\n');
         }
      }

      private readonly ITerminal m_Terminal;
      private readonly RuntimeProcess m_Proc;
   }
}
