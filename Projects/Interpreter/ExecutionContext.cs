using Assembler.Common;
using Assembler.Interpreter.InstructionInterpretation;
using Assembler.OutputProcessing;
using System;
using System.Collections.Generic;
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
      public ExecutionContext(IRuntimeEnvironment environment, ITerminal terminal, DisassembledFile file)
      {
         m_InterpreterFac = new InterpreterFactory(terminal);
         var dataSegment = new RuntimeDataSegmentAccessor(file.DataSegment);

         m_Ctx = new RuntimeContext(environment, dataSegment);

         m_TextSegment = file.TextSegment;
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

      public Register[] UserRegisters
      {
         get { return m_Ctx.UserRegisters; }
      }

      public void ExecuteNextInstruction()
      {
         int pcValue = m_Ctx.UserRegisters[InterpreterCommon.PC_REGISTER].Value;
         DisassembledInstruction instruction = m_TextSegment.FetchInstruction(pcValue);
         IInstructionInterpreter interpreter = m_InterpreterFac.GetInterpreter(instruction.InstructionType);

         // if this returns false, then increment the program counter by 4. otherwise, this indicates
         // that the instruction needed to change the PC.
         if (!interpreter.InterpretInstruction(m_Ctx, instruction.Parameters.ToArray()))
         {
            m_Ctx.UserRegisters[InterpreterCommon.PC_REGISTER].Value += sizeof(int);
         }
      }

      private readonly TextSegmentAccessor m_TextSegment;
      private readonly InterpreterFactory m_InterpreterFac;
      private readonly RuntimeContext m_Ctx;

   }
}
