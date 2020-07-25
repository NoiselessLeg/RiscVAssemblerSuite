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
   class WriteRegisterCommand : IConsoleCommand
   {
      public WriteRegisterCommand(RegisterManager registers, ITerminal terminal)
      {
         m_Registers = registers;
         m_Terminal = terminal;
      }

      public string CommandString => "writeRegister";

      public string CommandStringWithArgs => CommandString + "(<register name>, value)";

      public int NumArguments => 2;

      public string HelpText
      {
         get
         {
            return CommandStringWithArgs + " -> writes the specified value to the specified register.";
         }
      }

      public void Execute(string[] args)
      {
         try
         {
            string regName = args[0];
            if (IntExtensions.TryParseEx(args[1], out int iValue))
            {
               if (RegisterMap.IsNamedIntegerRegister(regName))
               {
                  int regIdx = RegisterMap.GetNumericRegisterValue(regName);
                  m_Registers.UserIntRegisters[regIdx].Value = iValue;
                  m_Terminal.PrintString("\t" + regName + " = " + iValue + '\n');
               }
               else
               {
                  throw new ParseException(regName + " was not a valid register name.");
               }
            }
            else if (FloatExtensions.TryParseEx(args[1], out float fValue))
            {
               if (RegisterMap.IsNamedFloatingPointRegister(regName))
               {
                  int regIdx = RegisterMap.GetNumericFloatingPointRegisterValue(regName);
                  m_Registers.UserFloatingPointRegisters[regIdx].Value = fValue;
                  m_Terminal.PrintString("\t" + regName + " = " + fValue + '\n');
               }
               else
               {
                  throw new ParseException(regName + " was not a valid register name.");
               }
            }
            else
            {
               throw new ParseException(args[1] + " was not a valid 32-bit value");
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
