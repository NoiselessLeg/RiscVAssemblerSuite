using Assembler.Common;
using Assembler.Interpreter.Exceptions;
using Assembler.Interpreter.InstructionInterpretation;
using Assembler.OutputProcessing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Interpreter
{
   public class ExecutionContext
   {
      /// <summary>
      /// Creates an instance of the file interpreter.
      /// </summary>
      /// <param name="terminal">The terminal implementation that will be used for I/O.</param>
      public ExecutionContext(IRuntimeEnvironment environment, ITerminal terminal, IList<IRegister> registers,
                              IDataSegmentAccessor dataSegment, TextSegmentAccessor textSegment)
      {
         m_Environment = environment;
         m_Terminal = terminal;
         m_InterpreterFac = new InterpreterFactory(environment, terminal);

         m_Ctx = new RuntimeContext(environment, dataSegment, registers);
         
         m_TextSegment = textSegment;
         m_Ctx.UserRegisters[InterpreterCommon.PC_REGISTER].Value = m_TextSegment.StartingSegmentAddress;
         m_Ctx.UserRegisters[InterpreterCommon.SP_REGISTER].Value = CommonConstants.DEFAULT_STACK_ADDRESS;
      }

      public bool EndOfFile
      {
         get
         {
            int pcValue = m_Ctx.UserRegisters[InterpreterCommon.PC_REGISTER].Value;
            return m_TextSegment.EndOfFileReached(pcValue);
         }
      }

      public IList<IRegister> UserRegisters
      {
         get { return m_Ctx.UserRegisters; }
      }

      public void AbortUserInputOperation()
      {
         m_Terminal.InterruptInputOperation();
      }

      public void ExecuteNextInstruction()
      {
         // make this separate so the user can figure out where the problem when wrong
         int originalPcValue = m_Ctx.UserRegisters[InterpreterCommon.PC_REGISTER].Value;
         try
         {
            DisassembledInstruction instruction = m_TextSegment.FetchInstruction(originalPcValue);
            IInstructionInterpreter interpreter = m_InterpreterFac.GetInterpreter(instruction.InstructionType);

            // if this returns false, then increment the program counter by 4. otherwise, this indicates
            // that the instruction needed to change the PC.
            if (!interpreter.InterpretInstruction(m_Ctx, instruction.Parameters.ToArray()))
            {
               m_Ctx.UserRegisters[InterpreterCommon.PC_REGISTER].Value += sizeof(int);
            }

            // check to make sure something didn't put us in some crazy address
            int newPcValue = m_Ctx.UserRegisters[InterpreterCommon.PC_REGISTER].Value;
            if (newPcValue < m_TextSegment.StartingSegmentAddress)
            {
               throw new Exceptions.AccessViolationException(string.Format("Program counter value 0x{0} was outside " +
                  "text segment.", newPcValue.ToString("x8")));
            }
         }
         catch (Exceptions.AccessViolationException ex)
         {
            m_Terminal.PrintString("Received SIGSEGV at 0x" + originalPcValue.ToString("x8") + ": Segmentation fault\n");
            m_Terminal.PrintString(ex.Message);
            m_Environment.Terminate();
         }
         catch (Simulation.Exceptions.RuntimeSignal)
         {
            throw;
         }
         catch (RuntimeException ex)
         {
            m_Terminal.PrintString("Received SIGABRT at 0x" + originalPcValue.ToString("x8") + ": ");
            m_Terminal.PrintString(ex.Message);
            m_Environment.Terminate();
         }
         catch (Exception ex)
         {
            m_Terminal.PrintString("Received SIGABRT at 0x" + originalPcValue.ToString("x8") + ": ");
            m_Terminal.PrintString(ex.Message);
            m_Terminal.PrintString(ex.StackTrace);
            m_Environment.Terminate();
         }
      }

      public void IncrementInstructionPointer()
      {
         m_Ctx.UserRegisters[InterpreterCommon.PC_REGISTER].Value += sizeof(int);
      }

      private readonly TextSegmentAccessor m_TextSegment;
      private readonly InterpreterFactory m_InterpreterFac;
      private readonly RuntimeContext m_Ctx;
      private readonly IRuntimeEnvironment m_Environment;
      private readonly ITerminal m_Terminal;

   }
}
