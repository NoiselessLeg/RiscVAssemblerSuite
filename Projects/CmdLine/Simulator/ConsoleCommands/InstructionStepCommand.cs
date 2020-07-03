using Assembler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.CmdLine.Simulator.ConsoleCommands
{
   public class InstructionStepCommand : IConsoleCommand
   {
      public InstructionStepCommand(RuntimeProcess proc, ITerminal terminal)
      {
         m_Exec = proc;
         m_Terminal = terminal;
      }
      
      public string CommandString => "step";

      public string CommandStringWithArgs => CommandString + "()";

      public string HelpText => CommandStringWithArgs + " -> executes one instruction.";

      public int NumArguments => 0;

      public void Execute(string[] args)
      {
         if (!m_Exec.IsRunning && !m_Exec.IsPaused)
         {
            m_Terminal.PrintString("Program is not running.\n");
         }
         else if (m_Exec.IsPaused)
         {
            m_Exec.PerformInstructionStep();
         }
      }

      private readonly RuntimeProcess m_Exec;
      private readonly ITerminal m_Terminal;
   }
}
