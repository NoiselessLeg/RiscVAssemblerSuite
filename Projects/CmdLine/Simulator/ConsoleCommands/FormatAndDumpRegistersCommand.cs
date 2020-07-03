using Assembler.Common;
using Assembler.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.CmdLine.Simulator.ConsoleCommands
{
   class FormatAndDumpRegistersCommand : IConsoleCommand
   {
      public FormatAndDumpRegistersCommand(Register[] registers, ITerminal terminal)
      {
         m_Registers = registers;
         m_Terminal = terminal;
      }

      public string CommandString => "dumpRegisters";

      public string CommandStringWithArgs => CommandString + "(<format string>)";

      public int NumArguments => 1;

      public string HelpText
      {
         get
         {
            return CommandStringWithArgs + "-> dumps the contents of all registers to the console using the specified format string.";
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


         for (int i = 0; i < m_Registers.Length; ++i)
         {
            string regName = ReverseRegisterMap.GetStringifiedRegisterValue(i);
            m_Terminal.PrintString(regName + " (" + i + ") = " + m_Registers[i].Value.ToString(fmtString) + '\n');
         }
      }

      private readonly Register[] m_Registers;
      private readonly ITerminal m_Terminal;
   }
}
