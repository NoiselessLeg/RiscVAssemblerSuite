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
   public class DumpRegistersCommand : IConsoleCommand
   {
      public DumpRegistersCommand(RegisterManager regMgr, ITerminal terminal)
      {
         m_RegMgr = regMgr;
         m_Terminal = terminal;
      }

      public string CommandString => "dumpRegisters";

      public string CommandStringWithArgs => CommandString + "()";

      public int NumArguments => 0;

      public string HelpText
      {
         get
         {
            return CommandString + "() -> dumps the contents of all registers to the console.";
         }
      }

      public void Execute(string[] args)
      {
         int regItr = 0;
         foreach (var iRegister in m_RegMgr.UserIntRegisters)
         {
            string regName = ReverseRegisterMap.GetStringifiedRegisterValue(regItr);
            m_Terminal.PrintString(regName + " (" + regItr + ") = " + iRegister.Value.ToString() + '\n');
            ++regItr;
         }

         regItr = 0;
         foreach (var fRegister in m_RegMgr.UserFloatingPointRegisters)
         {
            string regName = ReverseRegisterMap.GetStringifiedFloatingPtRegisterValue(regItr);
            m_Terminal.PrintString(regName + " (" + regItr + ") = " + fRegister.Value.ToString("0.0######") + '\n');
            ++regItr;
         }
      }

      private readonly RegisterManager m_RegMgr;
      private readonly ITerminal m_Terminal;
   }
}
