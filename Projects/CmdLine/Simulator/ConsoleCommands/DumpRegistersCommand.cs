using Assembler.Common;
using Assembler.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.CmdLine.Simulator.ConsoleCommands
{
   public class DumpRegistersCommand : IConsoleCommand
   {
      public DumpRegistersCommand(Register[] registers, ITerminal terminal)
      {
         m_Registers = registers;
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
         for (int i = 0; i < m_Registers.Length; ++i)
         {
            string regName = ReverseRegisterMap.GetStringifiedRegisterValue(i);
            m_Terminal.PrintString(regName + " (" + i + ") = " + m_Registers[i].Value.ToString() + '\n');
         }
      }

      private readonly Register[] m_Registers;
      private readonly ITerminal m_Terminal;
   }
}
