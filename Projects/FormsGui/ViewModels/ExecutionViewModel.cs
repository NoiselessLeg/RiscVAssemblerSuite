﻿using Assembler.Common;
using Assembler.FormsGui.DataModels;
using Assembler.FormsGui.Services;
using Assembler.FormsGui.Utility;
using Assembler.Interpreter;
using Assembler.Interpreter.Exceptions;
using Assembler.OutputProcessing;
using Assembler.Simulation;
using Assembler.Simulation.Exceptions;
using Assembler.UICommon.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assembler.FormsGui.ViewModels
{
   public class ExecutionViewModel : BaseViewModel, IRuntimeEnvironment
   {
      private enum PrgmExecutionState
      {
         Stopped,
         Paused,
         Running
      }

      public ExecutionViewModel(ITerminal terminal, DisassembledFileViewModel underlyingVm, IList<ProgramInstructionViewModel> instructionVm)
      {
         // intialize this so that we're signaled. this will allow the run task
         // to be initialized to not wait until the user actually commands us to pause
         m_PauseCtrl = new PauseController();
         m_InstructionAddrToBreakpointMap = new Dictionary<int, bool>();
         m_ExecutionState = PrgmExecutionState.Stopped;
         m_Terminal = terminal;
         var registers = new RegisterViewModel[InterpreterCommon.MAX_BASIC_REGISTERS];
         for (int i = 0; i < InterpreterCommon.MAX_BASIC_REGISTERS; ++i)
         {
            if (i == 0)
            {
               registers[i] = new ZeroRegisterViewModel();
            }
            else
            {
               registers[i] = new RegisterViewModel(i);
            }
         }

         DisassembledFileBase file = underlyingVm.FileData;

         var dataSegmentAccessor = new BindableDataSegmentAccessor(file.DataSegment);
         
         var fltPtRegs = new FloatingPointRegisterViewModel[InterpreterCommon.MAX_FLOATING_PT_REGISTERS];
         for (int i = 0; i < fltPtRegs.Length; ++i)
         {
            fltPtRegs[i] = new FloatingPointRegisterViewModel(i);
         }

         m_RegMgr = new RegisterManager(registers, fltPtRegs, file.TextSegment.StartingSegmentAddress, CommonConstants.DEFAULT_STACK_ADDRESS);
         m_Ctx = new Interpreter.ExecutionContext(this, terminal, dataSegmentAccessor, m_RegMgr, file.TextSegment);

         m_DataSegmentElements = new BindingList<DataAddressViewModel>();

         // increment this by 16. Each row will display four words.
         for (int currElem = file.DataSegment.BaseRuntimeDataAddress; 
             currElem < file.DataSegment.BaseRuntimeDataAddress + file.DataSegmentLength;
             currElem += 16)
         {
            m_DataSegmentElements.Add(new DataAddressViewModel(currElem, dataSegmentAccessor));
         }

         m_ExecuteFileCmd = new RelayCommand(() => OnExecutionTaskBegin(), true);
         m_TerminateExecutionCmd = new RelayCommand(() => CancelExecution(), false);
         m_SwitchRepresentationCmd = new RelayCommand<RegisterDisplayType>((param) => SwitchRegisterDisplayType(param), true);
         m_SwitchDataRepresentationCmd = new RelayCommand<RegisterDisplayType>((param) => SwitchDataDisplayType(param), true);
         m_PauseExecutionCmd = new RelayCommand(() => PauseExecution(), false);
         m_ResumeExecutionCmd = new RelayCommand(() => ResumeExecution(), false);
         m_InstructionStepCmd = new RelayCommand(() => TemporarilyUnblockExecutionTask(), false);
         m_SetBreakpointCmd = new RelayCommand<int>(param => SetProgramBreakpoint(param), true);
         m_UnsetBreakpointCmd = new RelayCommand<int>(param => UnsetProgramBreakpoint(param), true);

         // initialize the instruction breakpoint map.
         // this will give us a positive performance boost when we execute the program
         // since we will not be creating new boolean entries in the hash table.
         foreach (var instruction in instructionVm)
         {
            m_InstructionAddrToBreakpointMap.Add(instruction.ProgramCounterLocation, false);
            instruction.PropertyChanged += OnFileViewModelPropertyChanged;
         }
      }

      public ICommand PauseExecutionCommand
      {
         get { return m_PauseExecutionCmd; }
      }

      public ICommand ResumeExecutionCommand
      {
         get { return m_ResumeExecutionCmd; }
      }

      public ICommand ExecuteFileUntilEndCommand
      {
         get { return m_ExecuteFileCmd; }
      }

      public ICommand ChangeRegisterValueDisplayTypeCommand
      {
         get { return m_SwitchRepresentationCmd; }
      }

      public ICommand ChangeDataValueDisplayTypeCommand
      {
         get { return m_SwitchDataRepresentationCmd; }
      }

      public ICommand SetBreakpointCommand
      {
         get { return m_SetBreakpointCmd; }
      }

      public ICommand UnsetBreakpointCommand
      {
         get { return m_UnsetBreakpointCmd; }
      }

      public ICommand StepToNextInstructionCommand
      {
         get { return m_InstructionStepCmd; }
      }

      public ICommand TerminateExecutionCommand
      {
         get { return m_TerminateExecutionCmd; }
      }

      /// <summary>
      /// Returns a boolean value indicating the program is NOT in the STOPPED state.
      /// This returns true for cases where the program may be paused and/or running.
      /// </summary>
      public bool IsRunning
      {
         get { return m_ExecutionState != PrgmExecutionState.Stopped; }
      }

      /// <summary>
      /// Gets the index of the instruction that is currently being
      /// executed from an array of all program instructions.
      /// </summary>
      public int ActiveInstructionIdx
      {
         get { return m_ActiveInstructionIdx; }
         private set
         {
            if (PreviousInstructionIndex != value)
            {
               m_PrevInstructionIdx = m_ActiveInstructionIdx;
               m_ActiveInstructionIdx = value;
               OnPropertyChanged();
            }
         }
      }

      public int PreviousInstructionIndex
      {
         get { return m_PrevInstructionIdx; }
         private set
         {
            if (m_PrevInstructionIdx != value)
            {
               m_PrevInstructionIdx = value;
               OnPropertyChanged();
            }
         }
      }

      /// <summary>
      /// Gets or sets the current execution state of the program.
      /// </summary>
      private PrgmExecutionState ExecutionState
      {
         get { return m_ExecutionState; }
         set
         {
            if (m_ExecutionState != value)
            {
               m_ExecutionState = value;
               
               // since IsRunning is the only publicly exposed
               // property, notify a user that that property changed
               // (as saying this property changed will cause the base
               // class to throw an exception since it's not a public property).
               OnPropertyChanged(nameof(IsRunning));
            }
         }
      }

      public void Terminate()
      {
         m_ExecutionState = PrgmExecutionState.Stopped;
      }
      
      public void Break()
      {
         PauseExecution();
      }

      private void OnExecutionTaskBegin()
      {

         if (!IsRunning)
         {
            m_ExecuteFileCmd.CanExecute = false;
            m_TerminateExecutionCmd.CanExecute = true;
            m_PauseExecutionCmd.CanExecute = true;
            Task.Run(() => ExecuteUntilEnd());
         }

      }

      private void CancelExecution()
      {
         ExecutionState = PrgmExecutionState.Stopped;
         m_PauseCtrl.AbortChildProcess();
         m_Ctx.AbortUserInputOperation();
      }

      private void PauseExecution()
      {
         ExecutionState = PrgmExecutionState.Paused;
         m_PauseCtrl.PauseChildTaskExecution();
         m_InstructionStepCmd.CanExecute = true;
         m_ResumeExecutionCmd.CanExecute = true;
         m_PauseExecutionCmd.CanExecute = false;
      }

      private bool IsBreakpointAppliedAtInstruction(int pgmCtr)
      {
         return m_InstructionAddrToBreakpointMap[pgmCtr];
      }

      private void ResumeExecution()
      {
         ExecutionState = PrgmExecutionState.Running;
         m_PauseCtrl.ResumeChildExecution();
         m_InstructionStepCmd.CanExecute = false;
         m_ResumeExecutionCmd.CanExecute = false;
         m_PauseExecutionCmd.CanExecute = true;
      }

      private void SwitchRegisterDisplayType(RegisterDisplayType displayType)
      {
         foreach (var register in Registers)
         {
            register.DisplayType = displayType;
         }

         foreach (var register in FloatingPointRegisters)
         {
            register.DisplayType = displayType;
         }
      }

      private void SwitchDataDisplayType(RegisterDisplayType displayType)
      {
         foreach (DataAddressViewModel address in m_DataSegmentElements)
         {
            address.DisplayType = displayType;
         }
      }

      private void ExecuteUntilEnd()
      {
         var runTimer = new Stopwatch();
         ExecutionState = PrgmExecutionState.Running;
         ResetProgramContext();

         runTimer.Start();
         try
         {
            while (IsRunning && !m_Ctx.EndOfFile)
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

               m_PauseCtrl.WaitIfPauseCommanded();
               ExecuteNextInstruction();
            }
            runTimer.Stop();
            m_Terminal.PrintString("\n\nINFO: Execution completed in " + runTimer.Elapsed);
         }
         catch (AbortSignal)
         {

         }

         m_Terminal.RequestOutputFlush();
         m_ExecutionState = PrgmExecutionState.Stopped;

         m_InstructionStepCmd.CanExecute = false;
         m_TerminateExecutionCmd.CanExecute = false;
         m_ResumeExecutionCmd.CanExecute = false;
         m_PauseExecutionCmd.CanExecute = false;
         m_ExecuteFileCmd.CanExecute = true;
      }

      /// <summary>
      /// Sets the condition variable to allow the execution task
      /// to execute one instruction. This will be reset by the task
      /// on the next instruction cycle.
      /// </summary>
      private void TemporarilyUnblockExecutionTask()
      {
         m_Terminal.RequestOutputFlush();
         m_PauseCtrl.ResumeChildExecution();
      }

      /// <summary>
      /// Executes the next instruction as pointed to by the program counter,
      /// and determines the next active instruction index.
      /// </summary>
      private void ExecuteNextInstruction()
      {
         m_Ctx.ExecuteNextInstruction();
         if (IsRunning && !m_Ctx.EndOfFile)
         {
            int pcVal = m_Ctx.UserRegisters[InterpreterCommon.PC_REGISTER].Value;
            ActiveInstructionIdx = CalculateActiveInstructionIndex(pcVal);
         }
      }

      public IList<RegisterViewModel> Registers
      {
         get { return (IList<RegisterViewModel>)m_RegMgr.UserIntRegisters; }
      }

      public IList<FloatingPointRegisterViewModel> FloatingPointRegisters
      {
         get { return (IList<FloatingPointRegisterViewModel>)m_RegMgr.UserFloatingPointRegisters; }
      }

      public BindingList<DataAddressViewModel> DataElements
      {
         get { return m_DataSegmentElements; }
      }

      /// <summary>
      /// Gets whether or not the program is explicitly in the paused state.
      /// </summary>
      private bool IsPaused
      {
         get { return m_ExecutionState == PrgmExecutionState.Paused; }
      }

      private void ResetProgramContext()
      {
         ActiveInstructionIdx = 0;
         m_RegMgr.RestoreOriginalRegisterValues();
      }

      private void SetProgramBreakpoint(int instructionAddr)
      {
         m_InstructionAddrToBreakpointMap[instructionAddr] = true;
      }

      private void UnsetProgramBreakpoint(int instructionAddr)
      {
         m_InstructionAddrToBreakpointMap[instructionAddr] = false;
      }

      private int CalculateActiveInstructionIndex(int programCtr)
      {
         // this will give us our initial program counter value which we can use
         // to determine how many instructions we've executed.
         int originalPcValue = m_RegMgr.GetOriginalValue(InterpreterCommon.PC_REGISTER);
         int delta = programCtr - originalPcValue;
         return (delta / sizeof(int));
      }

      private void OnFileViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
      {
         var instructionVm = sender as ProgramInstructionViewModel;
         if (e.PropertyName == nameof(instructionVm.IsBreakpointApplied))
         {
            if (instructionVm.IsBreakpointApplied)
            {
               SetProgramBreakpoint(instructionVm.ProgramCounterLocation);
            }
            else
            {
               UnsetProgramBreakpoint(instructionVm.ProgramCounterLocation);
            }
         }
      }

      private int m_ActiveInstructionIdx;
      private int m_PrevInstructionIdx;

      private readonly ITerminal m_Terminal;

      private readonly PauseController m_PauseCtrl;
      private readonly RelayCommand m_ExecuteFileCmd;
      private readonly RelayCommand m_PauseExecutionCmd;
      private readonly RelayCommand m_ResumeExecutionCmd;
      private readonly RelayCommand m_TerminateExecutionCmd;
      private readonly RelayCommand<RegisterDisplayType> m_SwitchRepresentationCmd;
      private readonly RelayCommand<RegisterDisplayType> m_SwitchDataRepresentationCmd;
      private readonly RelayCommand m_InstructionStepCmd;
      private readonly RelayCommand<int> m_SetBreakpointCmd;
      private readonly RelayCommand<int> m_UnsetBreakpointCmd;
      private readonly RegisterManager m_RegMgr;
      private readonly BindingList<DataAddressViewModel> m_DataSegmentElements;


      private readonly Interpreter.ExecutionContext m_Ctx;

      private PrgmExecutionState m_ExecutionState;

      private readonly Dictionary<int, bool> m_InstructionAddrToBreakpointMap;
   }
}
