using Assembler.CmdLine.LoggerTypes;
using Assembler.Common;
using Assembler.Interpreter;
using Assembler.OutputProcessing;
using Assembler.OutputProcessing.FileReaders;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assembler.CmdLine
{
   class ProgramExecutor : IRuntimeEnvironment
   {
      public ProgramExecutor(string compiledFileName, ITerminal terminal)
      {
         m_Logger = new ConsoleLogger();
         m_FileProc = new FileReaderFactory();

         var fileReader = m_FileProc.GetFileParser(compiledFileName);
         DisassembledFileBase file = fileReader.ParseFile(compiledFileName, m_Logger);
         
         m_Registers = new Register[InterpreterCommon.MAX_REGISTERS];
         for (int i = 0; i < InterpreterCommon.MAX_REGISTERS; ++i)
         {
            if (i == 0)
            {
               m_Registers.Add(new ZeroRegister());
            }
            else
            {
               m_Registers.Add(new Register());
            }
         }

         var dataSegmentAccessor = new RuntimeDataSegmentAccessor(file.DataSegment);
         m_Ctx = new Interpreter.ExecutionContext(this, terminal, m_Registers, 
            dataSegmentAccessor, file.TextSegment);
         
         m_Terminal = terminal;
      }

      public void ExecuteProgram()
      {
#if false
         var runTimer = new Stopwatch();
         ExecutionState = PrgmExecutionState.Running;

         runTimer.Start();
         while (m_IsRunning && !m_Ctx.EndOfFile)
         {
            // double check this here, to see if the user paused it
            // or there is a breakpoint at our current instruction. if the user
            // pauses this, a breakpoint will not be applied at the instruction so
            // we need to see the flag value (as the flag will be set).
            if (IsPaused ||
                IsBreakpointAppliedAtInstruction(m_Ctx.UserRegisters[InterpreterCommon.PC_REGISTER].Value))
            {
               // we set the pause flag here. if the user steps to the next instruction, we want to 
               // once again reset the condition variable so we pause again. this is because the step needs
               // to happen in this task
               PauseExecution();
            }
            m_PauseEvent.WaitOne();
            ExecuteNextInstruction();
         }
         runTimer.Stop();
         m_Terminal.PrintString("\n\nINFO: Execution completed in " + runTimer.Elapsed);

         m_Terminal.RequestOutputFlush();
#endif
      }

      public void Break()
      {
         throw new NotImplementedException();
      }

      public void Terminate()
      {
         throw new NotImplementedException();
      }

      private readonly IList<IRegister> m_Registers;

      private readonly ConsoleLogger m_Logger;
      private readonly FileReaderFactory m_FileProc;
      private readonly Interpreter.ExecutionContext m_Ctx;
      private readonly ITerminal m_Terminal;
      private readonly ManualResetEvent m_PauseEvent;
      private bool m_IsRunning;
   }
}
