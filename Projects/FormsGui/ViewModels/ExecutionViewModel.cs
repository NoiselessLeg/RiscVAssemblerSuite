using Assembler.Common;
using Assembler.FormsGui.Commands;
using Assembler.FormsGui.Services;
using Assembler.FormsGui.Utility;
using Assembler.Interpreter;
using Assembler.OutputProcessing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assembler.FormsGui.ViewModels
{
   public class ExecutionViewModel : BaseViewModel, IRuntimeEnvironment
   {
      public ExecutionViewModel(ITerminal terminal, DisassembledFile file)
      {
         m_Terminal = terminal;
         m_Registers = new RegisterViewModel[InterpreterCommon.MAX_REGISTERS];
         for (int i = 0; i < InterpreterCommon.MAX_REGISTERS; ++i)
         {
            if (i == 0)
            {
               m_Registers[i] = new ZeroRegisterViewModel();
            }
            else
            {
               m_Registers[i] = new RegisterViewModel(i);
            }
         }
         m_Ctx = new Interpreter.ExecutionContext(this, terminal, m_Registers, file);

         m_DefaultRegValues = new Register[m_Ctx.UserRegisters.Count];
         for (int i = 0; i < InterpreterCommon.MAX_REGISTERS; ++i)
         {
            m_DefaultRegValues[i] = new Register(m_Ctx.UserRegisters[i].Value);
         }
         
         m_ExecuteFileCmd = new RelayCommand((param) => OnExecutionTaskBegin());
         m_TerminateExecutionCmd = new RelayCommand((param) => CancelExecution());
         m_SwitchRepresentationCmd = new RelayCommand((param) => SwitchRegisterDisplayType((RegisterDisplayType)param));
      }

      public ICommand ExecuteFileUntilEndCommand
      {
         get { return m_ExecuteFileCmd; }
      }

      public ICommand ChangeRegisterValueDisplayTypeCommand
      {
         get { return m_SwitchRepresentationCmd; }
      }

      
      private void OnExecutionTaskBegin()
      {
         // determine if we're already executing a run. if so, just ignore
         // the command.
         if (!IsRunning)
         {
            Task.Run(() => ExecuteUntilEnd());
         }

      }

      private void CancelExecution()
      {

      }

      private void SwitchRegisterDisplayType(RegisterDisplayType displayType)
      {
         foreach (var register in Registers)
         {
            register.DisplayType = displayType;
         }
      }

      private void ExecuteUntilEnd()
      {
         IsRunning = true;
         ResetProgramContext();

         while (IsRunning && !m_Ctx.EndOfFile)
         {
            m_Ctx.ExecuteNextInstruction();
         }

         m_Terminal.RequestOutputFlush();
         IsRunning = false;
      }

      private void ExecuteNextInstruction()
      {
         m_Ctx.ExecuteNextInstruction();
      }

      public void Terminate()
      {
         m_IsRunning = false;
      }

      public bool IsRunning
      {
         get { return m_IsRunning; }
         private set
         {
            if (m_IsRunning != value)
            {
               m_IsRunning = value;
               OnPropertyChanged();
            }
         }
      }

      public RegisterViewModel[] Registers
      {
         get { return m_Registers; }
      }

      private void ResetProgramContext()
      {
         for (int i = 0; i < InterpreterCommon.MAX_REGISTERS; ++i)
         {
            m_Ctx.UserRegisters[InterpreterCommon.PC_REGISTER].Value = m_DefaultRegValues[i].Value;
         }
      }

      private readonly ITerminal m_Terminal;
      private readonly RelayCommand m_ExecuteFileCmd;
      private readonly RelayCommand m_TerminateExecutionCmd;
      private readonly RelayCommand m_SwitchRepresentationCmd;
      private readonly RegisterViewModel[] m_Registers;
      private readonly Interpreter.ExecutionContext m_Ctx;

      private readonly Register[] m_DefaultRegValues;

      private bool m_IsRunning;

   }
}
