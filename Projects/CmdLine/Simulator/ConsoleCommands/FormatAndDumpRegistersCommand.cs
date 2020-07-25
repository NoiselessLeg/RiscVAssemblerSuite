using Assembler.Common;
using Assembler.Interpreter;
using Assembler.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.CmdLine.Simulator.ConsoleCommands
{
   class FormatAndDumpRegistersCommand : IConsoleCommand
   {
      public FormatAndDumpRegistersCommand(RegisterManager regMgr, ITerminal terminal)
      {
         m_Registers = regMgr;
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

         int regItr = 0;
         foreach (var iRegister in m_Registers.UserIntRegisters)
         {
            string regName = ReverseRegisterMap.GetStringifiedRegisterValue(regItr);
            m_Terminal.PrintString(regName + " (" + regItr + ") = " + iRegister.Value.ToString(fmtString) + '\n');
            ++regItr;
         }

         regItr = 0;
         foreach (var fRegister in m_Registers.UserFloatingPointRegisters)
         {
            string regName = ReverseRegisterMap.GetStringifiedFloatingPtRegisterValue(regItr);
            m_Terminal.PrintString(regName + " (" + regItr + ") = " + fRegister.Value.ToString(fmtString) + '\n');
            ++regItr;
         }
      }

      private readonly RegisterManager m_Registers;
      private readonly ITerminal m_Terminal;
   }
}
