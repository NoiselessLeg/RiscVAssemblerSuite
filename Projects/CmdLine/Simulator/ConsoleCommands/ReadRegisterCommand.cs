using Assembler.Common;
using Assembler.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.CmdLine.Simulator.ConsoleCommands
{
   public class ReadRegisterCommand : IConsoleCommand
   {
      public ReadRegisterCommand(Register[] registers, ITerminal terminal)
      {
         m_Registers = registers;
         m_Terminal = terminal;
      }

      public string CommandString => "readRegister";

      public string CommandStringWithArgs => CommandString + "(<register name | register index>)";

      public int NumArguments => 1;

      public string HelpText
      {
         get
         {
            return CommandStringWithArgs + " -> prints the current value of the specified register.";
         }
      }

      public void Execute(string[] args)
      {
         try
         {
            int regIdx = -1;
            string regName = string.Empty;
            if (RegisterMap.IsNamedRegister(args[0]))
            {
               regIdx = RegisterMap.GetNumericRegisterValue(args[0]);
               regName = args[0];
            }
            else
            {
               regIdx = int.Parse(args[0]);
               if (regIdx < m_Registers.Length)
               {
                  regName = ReverseRegisterMap.GetStringifiedRegisterValue(regIdx);
               }
               else
               {
                  throw new ParseException(regIdx + " was not a valid register index.");
               }
            }

            m_Terminal.PrintString("\t" + regName + " = " + m_Registers[regIdx].Value + '\n');
         }
         catch (Exception ex)
         {
            m_Terminal.PrintString(ex.Message + '\n');
         }
      }

      private readonly Register[] m_Registers;
      private readonly ITerminal m_Terminal;
   }
}
