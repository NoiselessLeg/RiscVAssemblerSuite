using Assembler.Common;
using Assembler.Interpreter.InstructionInterpretation;
using System;

namespace Assembler.Interpreter
{
   [Obsolete("Use the execution context to perform actions", false)]
   public class FileDebugger : IRuntimeEnvironment
   {
      /// <summary>
      /// Creates an instance of the file interpreter.
      /// </summary>
      /// <param name="terminal">The terminal implementation that will be used for I/O.</param>
      public FileDebugger(ITerminal terminal)
      {

         m_InterpreterFac = new InterpreterFactory(this, terminal);
         m_Terminal = terminal;
      }

      /// <summary>
      /// Diassembles and interprets a .JEF file.
      /// </summary>
      /// <param name="fileName">The file name to run the interpreter with.</param>
      /// <param name="logger">A logging implementation to use to disassemble the file.</param>
      public void RunJefFile(string fileName, ILogger logger)
      {
#if false
         try
            {
                var disassembler = new JefFileProcessor();
                DisassembledFile file = disassembler.ProcessJefFile(fileName, logger);

                var dataSegment = new RuntimeDataSegmentAccessor(file.DataSegment);
                RuntimeContext ctx = new RuntimeContext(this, dataSegment);

                ctx.UserRegisters[InterpreterCommon.PC_REGISTER].Value = file.TextSegment.StartingSegmentAddress;
                ctx.UserRegisters[InterpreterCommon.SP_REGISTER].Value = CommonConstants.DEFAULT_STACK_ADDRESS;

                int programCtr = ctx.UserRegisters[InterpreterCommon.PC_REGISTER].Value;

                logger.Log(LogLevel.Info, "Successfully loaded file \"" + fileName + "\"");

                try
                {
                    while (!file.TextSegment.EndOfFileReached(ctx.UserRegisters[InterpreterCommon.PC_REGISTER].Value) && !m_TerminationRequested)
                    {
                        DisassembledInstruction instruction = file.TextSegment.FetchInstruction(ctx.UserRegisters[InterpreterCommon.PC_REGISTER].Value);
                        IInstructionInterpreter interpreter = m_InterpreterFac.GetInterpreter(instruction.InstructionType);

                        // if this returns false, then increment the program counter by 4. otherwise, this indicates
                        // that the instruction needed to change the PC.
                        if (!interpreter.InterpretInstruction(ctx, instruction.Parameters.ToArray()))
                        {
                            ctx.UserRegisters[InterpreterCommon.PC_REGISTER].Value += sizeof(int);
                        }
                    }

                    logger.Log(LogLevel.Info, "Execution completed normally.");
                }
                catch (AccessViolationException)
                {
                    m_Terminal.PrintString("Received SIGSEGV during execution: segmentation fault.\n");
                    m_Terminal.PrintString("Register data:\n");
                    int regCtr = 0;
                    foreach (Register r in ctx.UserRegisters)
                    {
                        if (regCtr == InterpreterCommon.PC_REGISTER)
                        {
                            m_Terminal.PrintString("\tpc: 0x" + r.Value.ToString("X6") + '\n');
                        }
                        else
                        {
                            m_Terminal.PrintString("\t" + ReverseRegisterMap.GetStringifiedRegisterValue(regCtr) + ": 0x" + r.Value.ToString("X6") + '\n');
                        }
                        ++regCtr;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Critical, "Runtime exception occurred: " + ex.Message);
            }
#endif
      }

      /// <summary>
      /// Terminates execution of the current assembly program.
      /// </summary>
      public void Terminate()
      {
         m_TerminationRequested = true;
      }

      public void Break()
      {

      }

      private readonly ITerminal m_Terminal;
      private readonly InterpreterFactory m_InterpreterFac;
      private bool m_TerminationRequested;
   }
}
