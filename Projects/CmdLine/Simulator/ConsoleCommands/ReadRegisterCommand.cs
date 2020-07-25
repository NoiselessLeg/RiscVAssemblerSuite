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
   public class ReadRegisterCommand : IConsoleCommand
   {
      public ReadRegisterCommand(RegisterManager registers, ITerminal terminal)
      {
         m_Registers = registers;
         m_Terminal = terminal;
      }

      public string CommandString => "readRegister";

      public string CommandStringWithArgs => CommandString + "(<register name>)";

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
            string regName = args[0];
            if (RegisterMap.IsNamedIntegerRegister(regName))
            {
               regIdx = RegisterMap.GetNumericRegisterValue(regName);
               m_Terminal.PrintString("\t" + regName + " = " + m_Registers.UserIntRegisters[regIdx].Value + '\n');
            }
            else if (RegisterMap.IsNamedFloatingPointRegister(regName))
            {
               regIdx = RegisterMap.GetNumericFloatingPointRegisterValue(regName);
               m_Terminal.PrintString("\t" + regName + " = " + m_Registers.UserFloatingPointRegisters[regIdx].Value + '\n');
            }
            else
            {
               throw new ParseException(regName + " is not a valid integer register name.");
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
