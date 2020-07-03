using Assembler.CmdLine.LoggerTypes;
using Assembler.CmdLine.Simulator;
using Assembler.Common;
using Assembler.Interpreter;
using Assembler.OutputProcessing;
using Assembler.OutputProcessing.FileReaders;
using Assembler.Simulation.Exceptions;
using Assembler.UICommon.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assembler.CmdLine
{
   public class RuntimeProcess : IRuntimeEnvironment
   {
      private enum PrgmExecutionState
      {
         Stopped,
         Paused,
         Running
      }

      public RuntimeProcess(DisassembledFileBase file, ITerminal terminal)
      {
         // intialize this so that we're signaled. this will allow the run task
         // to be initialized to not wait until the user actually commands us to pause
         m_RunTimer = new Stopwatch();
         m_ProcCtrl = new ChildProcControl();
         m_InstructionAddrToBreakpointMap = new Dictionary<int, bool>();
         m_ExecutionState = PrgmExecutionState.Stopped;
         m_Terminal = terminal;
         m_Registers = new Register[InterpreterCommon.MAX_REGISTERS];
         for (int i = 0; i < InterpreterCommon.MAX_REGISTERS; ++i)
         {
            if (i == 0)
            {
               m_Registers[i] = new ZeroRegister();
            }
            else
            {
               m_Registers[i] = new Register();
            }
         }
         
         m_DataSegment = new RuntimeDataSegmentAccessor(file.DataSegment);

         m_Ctx = new Interpreter.ExecutionContext(this, terminal, m_Registers, m_DataSegment, file.TextSegment);

         m_DefaultRegValues = new Register[m_Ctx.UserRegisters.Count];
         for (int i = 0; i < InterpreterCommon.MAX_REGISTERS; ++i)
         {
            m_DefaultRegValues[i] = new Register(m_Ctx.UserRegisters[i].Value);
         }

         // initialize the instruction breakpoint map.
         // this will give us a positive performance boost when we execute the program
         // since we will not be creating new boolean entries in the hash table.
         int endingTxtSegmentAddr = file.TextSegment.StartingSegmentAddress + file.TextSegment.SegmentSize;
         for (int instructionAddr = file.TextSegment.StartingSegmentAddress; 
              instructionAddr < endingTxtSegmentAddr; 
              instructionAddr += sizeof(int))
         {
            m_InstructionAddrToBreakpointMap.Add(instructionAddr, false);
         }
      }

      public bool IsRunning
      {
         get { return m_ExecutionState != PrgmExecutionState.Stopped; }
      }

      public bool IsPaused
      {
         get { return m_ExecutionState == PrgmExecutionState.Paused; }
      }

      public int ProgramCounterValue
      {
         get { return m_Ctx.UserRegisters[InterpreterCommon.PC_REGISTER].Value; }
      }

      public void SetBreakpoint(int instructionAddr)
      {
         m_InstructionAddrToBreakpointMap[instructionAddr] = true;
      }

      public void RemoveBreakpoint(int instructionAddr)
      {
         m_InstructionAddrToBreakpointMap[instructionAddr] = false;
      }

      public void StartProgramExecution()
      {
         // if a user restarted the program while breaked, 
         if (ExecutionState == PrgmExecutionState.Paused)
         {
            TerminatePreExistingRuntimeTask();
         }

         InitializeProcess();
         m_RunTimer.Start();

         Task.Run(() => ExecuteProcess());
         GrantControlToChildProcess();
      }

      private void InitializeProcess()
      {
         ExecutionState = PrgmExecutionState.Running;
         ResetProgramContext();
         m_RunTimer.Reset();
      }

      /// <summary>
      /// Temporarily grants control to the child process. This is primarily
      /// for stepping through instructions.
      /// </summary>
      public void PerformInstructionStep()
      {
         GrantControlToChildProcess();
      }

      public void Terminate()
      {
         m_ExecutionState = PrgmExecutionState.Stopped;
      }

      public void Break()
      {
         PauseExecution();
      }

      private void CleanUpExecutionEnvironment(bool isNormalExit)
      {
         m_RunTimer.Stop();
         if (isNormalExit)
         {
            m_Terminal.PrintString("\n\nINFO: Execution completed in " + m_RunTimer.Elapsed + '\n');
         }
         else
         {
            m_Terminal.PrintChar('\n');
         }

         m_Terminal.RequestOutputFlush();
         m_ExecutionState = PrgmExecutionState.Stopped;
      }

      private void CancelExecution()
      {
         ExecutionState = PrgmExecutionState.Stopped;
         m_Ctx.AbortUserInputOperation();
      }

      private void PauseExecution()
      {
         ExecutionState = PrgmExecutionState.Paused;
         RelinquishControlToParentProcess();
      }

      private bool IsBreakpointAppliedAtInstruction(int pgmCtr)
      {
         return m_InstructionAddrToBreakpointMap[pgmCtr];
      }

      public void ResumeExecution()
      {
         ExecutionState = PrgmExecutionState.Running;
         GrantControlToChildProcess();
      }

      public int ReadMemory(int address)
      {
         return m_DataSegment.ReadWord(address);
      }

      public void WriteMemory(int address, int value)
      {
         m_DataSegment.WriteWord(address, value);
      }

      public void DumpMemorySegment()
      {
         for (int segmentItr = 0; segmentItr < m_DataSegment.TotalDataSegmentSize; segmentItr += sizeof(int))
         {
            int address = m_DataSegment.BaseRuntimeDataAddress + segmentItr;
            m_Terminal.PrintString("\t0x" + address.ToString("X8") + " = " + m_DataSegment.ReadWord(address) + '\n');
         }
      }

      public void DumpMemorySegment(string fmtStr)
      {
         for (int segmentItr = 0; segmentItr < m_DataSegment.TotalDataSegmentSize; segmentItr += sizeof(int))
         {
            int address = m_DataSegment.BaseRuntimeDataAddress + segmentItr;
            m_Terminal.PrintString("\t0x" + address.ToString("X8") + " = " + m_DataSegment.ReadWord(address).ToString(fmtStr) + '\n');
         }
      }

      private void ExecuteProcess()
      {
         bool isExitingNormally = true;
         while (!m_Ctx.EndOfFile &&
                IsRunning)
         {
            try
            {
               if (IsBreakpointAppliedAtInstruction(m_Ctx.UserRegisters[InterpreterCommon.PC_REGISTER].Value))
               {
                  int pcRegValue = ProgramCounterValue;
                  m_Terminal.PrintString("Breakpoint triggered at program counter 0x" + pcRegValue.ToString("x8") + '\n');
                  PauseExecution();
               }

               m_Ctx.ExecuteNextInstruction();

               // we'd feasibly get here if we're paused, and a user steps the next instruction.
               if (IsPaused)
               {
                  RelinquishControlToParentProcess();
               }
            }
            catch (InterruptSignal)
            {
               int pcRegValue = ProgramCounterValue;
               m_Terminal.PrintString("\nReceived SIGINT; current instruction: 0x" + pcRegValue.ToString("x8") + '\n');
               try
               {
                  PauseExecution();
               }
               catch (AbortSignal)
               {
                  Terminate();
                  isExitingNormally = false;
               }
            }
            catch (AbortSignal)
            {
               Terminate();
               isExitingNormally = false;
            }
            catch (Exception)
            {
               int pcRegValue = ProgramCounterValue;
               m_Terminal.PrintString("Received SIGSEGV at instruction pointer address 0x" + pcRegValue.ToString("x8") + ": Segmentation fault\n");
               Terminate();
               isExitingNormally = false;
            }
         }

         CleanUpExecutionEnvironment(isExitingNormally);
         RelinquishControlToParentProcess();
      }

      private void GrantControlToChildProcess()
      {
         m_ProcCtrl.GrantControlToChildProcess();
      }

      private void RelinquishControlToParentProcess()
      {
         m_ProcCtrl.RelinquishControlToParentProcess();
      }

      public Register[] Registers
      {
         get { return m_Registers; }
      }

      private PrgmExecutionState ExecutionState
      {
         get { return m_ExecutionState; }
         set
         {
            if (m_ExecutionState != value)
            {
               m_ExecutionState = value;
            }
         }
      }

      private void ResetProgramContext()
      {
         for (int i = 0; i < InterpreterCommon.MAX_REGISTERS; ++i)
         {
            m_Ctx.UserRegisters[i].Value = m_DefaultRegValues[i].Value;
         }
      }
      
      private void TerminatePreExistingRuntimeTask()
      {
         m_ProcCtrl.AbortChildProcess();
      }

      private readonly ITerminal m_Terminal;
      private readonly Register[] m_Registers;
      private readonly Stopwatch m_RunTimer;
      private readonly ChildProcControl m_ProcCtrl;
      private readonly RuntimeDataSegmentAccessor m_DataSegment;
      private readonly Interpreter.ExecutionContext m_Ctx;
      
      private readonly Register[] m_DefaultRegValues;

      private PrgmExecutionState m_ExecutionState;

      private readonly Dictionary<int, bool> m_InstructionAddrToBreakpointMap;
   }
}
