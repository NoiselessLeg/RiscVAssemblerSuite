using Assembler.Common;
using Assembler.Interpreter.InstructionInterpretation;

namespace Assembler.Interpreter
{
   /// <summary>
   /// Class that runs and interprets a .JEF RISC-V output file.
   /// </summary>
   public class FileInterpreter : IRuntimeEnvironment
   {
      /// <summary>
      /// Creates an instance of the file interpreter.
      /// </summary>
      /// <param name="terminal">The terminal implementation that will be used for I/O.</param>
      public FileInterpreter(ITerminal terminal)
      {
         m_InterpreterFac = new InterpreterFactory(terminal);
      }

      /// <summary>
      /// Returns a boolean value representing if the runtime was requested to terminate.
      /// </summary>
      /// <returns>True if a termination request was placed by the runtime; otherwise returns false.</returns>
      public bool IsTerminationRequested()
      {
         return m_TerminationRequested;
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
                var ctx = new RuntimeContext(this, dataSegment);

                ctx.UserRegisters[InterpreterCommon.PC_REGISTER].Value = file.TextSegment.StartingSegmentAddress;
                ctx.UserRegisters[InterpreterCommon.SP_REGISTER].Value = CommonConstants.DEFAULT_STACK_ADDRESS;

                int programCtr = ctx.UserRegisters[InterpreterCommon.PC_REGISTER].Value;

                try 
                {
                    while (!file.TextSegment.EndOfFileReached(ctx.UserRegisters[InterpreterCommon.PC_REGISTER].Value) && !IsTerminationRequested())
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

                    logger.Log(LogLevel.Info, "Execution complete.");
                }
                catch (Exception)
                {
                    //TODO: need to add configurable exception handling
                    throw;
                }
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Critical, "Runtime exception occurred: " + ex.Message);
                logger.Log(LogLevel.Critical, "No exception handler found - terminating program.");
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

      private readonly InterpreterFactory m_InterpreterFac;
      private bool m_TerminationRequested;
   }
}
