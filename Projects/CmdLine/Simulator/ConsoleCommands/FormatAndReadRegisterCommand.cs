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
   class FormatAndReadRegisterCommand : IConsoleCommand
   {
      public FormatAndReadRegisterCommand(RegisterManager registers, ITerminal terminal)
      {
         m_Registers = registers;
         m_Terminal = terminal;
      }

      public string CommandString => "readRegister";

      public string CommandStringWithArgs => CommandString + "(<register name>, <format string>)";

      public int NumArguments => 2;

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
            string fmtString = args[1];

            if (fmtString.Contains("\""))
            {
               fmtString = fmtString.Replace("\"", string.Empty);
            }

            fmtString = fmtString.Trim();

            int regIdx = -1;
            string regName = string.Empty;
            if (RegisterMap.IsNamedIntegerRegister(args[0]))
            {
               regIdx = RegisterMap.GetNumericRegisterValue(args[0]);
               regName = args[0];
               m_Terminal.PrintString("\t" + regName + " = " + 
                  m_Registers.UserIntRegisters[regIdx].Value.ToString(fmtString) + '\n');
            }
            else if (RegisterMap.IsNamedFloatingPointRegister(args[0]))
            {
               regIdx = RegisterMap.GetNumericFloatingPointRegisterValue(args[0]);
               regName = args[0];
               m_Terminal.PrintString("\t" + regName + " = " + 
                  m_Registers.UserFloatingPointRegisters[regIdx].Value.ToString(fmtString) + '\n');
            }
            else
            {
               throw new ParseException(regIdx + " was not a valid register name.");
            }

         }
         catch (Exception ex)
         {
            m_Terminal.PrintString(ex.Message + '\n');
         }
      }

      private readonly RegisterManager m_Registers;
      private readonly ITerminal m_Terminal;
   }
}
