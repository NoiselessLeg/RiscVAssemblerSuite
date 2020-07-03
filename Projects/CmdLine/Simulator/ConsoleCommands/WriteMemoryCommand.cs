using Assembler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.CmdLine.Simulator.ConsoleCommands
{
   public class WriteMemoryCommand : IConsoleCommand
   {

      public WriteMemoryCommand(RuntimeProcess proc, ITerminal terminal)
      {
         m_Proc = proc;
         m_Terminal = terminal;
      }

      public string CommandString => "writeMemory";

      public string CommandStringWithArgs => CommandString + "(<address>, <value>)";

      public string HelpText => CommandStringWithArgs + " -> writes a 32-bit word to an address in memory.";

      public int NumArguments => 2;

      public void Execute(string[] args)
      {
         try
         {

            if (IntExtensions.TryParseEx(args[0], out int address))
            {
               if (IntExtensions.TryParseEx(args[1], out int value))
               {
                  m_Proc.WriteMemory(address, value);
                  m_Terminal.PrintString("\t" + args[0] + " = " + args[1] + '\n');
               }
               else
               {
                  m_Terminal.PrintString(args[1] + " was not a valid 32 bit integer.\n");
               }
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
