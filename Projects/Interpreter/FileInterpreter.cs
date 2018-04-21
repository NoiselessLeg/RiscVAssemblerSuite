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
    /// <summary>
    /// Class that runs and interprets a .JEF RISC-V output file.
    /// </summary>
    public class FileInterpreter : IExecutionEnvironment
    {
        /// <summary>
        /// Creates an instance of the file interpreter.
        /// </summary>
        /// <param name="terminal">The terminal implementation that will be used for I/O.</param>
        public FileInterpreter(ITerminal terminal)
        {
            m_InterpreterFac = new InterpreterFactory(this, terminal);

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
        }

        /// <summary>
        /// Diassembles and interprets a .JEF file.
        /// </summary>
        /// <param name="fileName">The file name to run the interpreter with.</param>
        /// <param name="logger">A logging implementation to use to disassemble the file.</param>
        public void RunJefFile(string fileName, ILogger logger)
        {
            try
            {
                var disassembler = new JefFileProcessor();
                DisassembledFile file = disassembler.ProcessJefFile(fileName, logger);

                var dataSegment = new RuntimeDataSegmentAccessor(file.DataSegment);

                m_Registers[InterpreterCommon.PC_REGISTER].Value = file.TextSegment.StartingSegmentAddress;
                m_Registers[InterpreterCommon.SP_REGISTER].Value = CommonConstants.DEFAULT_STACK_ADDRESS;

                int programCtr = m_Registers[InterpreterCommon.PC_REGISTER].Value;

                while (!file.TextSegment.EndOfFileReached(m_Registers[InterpreterCommon.PC_REGISTER].Value) && !m_TerminationRequested)
                {
                    DisassembledInstruction instruction = file.TextSegment.FetchInstruction(m_Registers[InterpreterCommon.PC_REGISTER].Value);
                    IInstructionInterpreter interpreter = m_InterpreterFac.GetInterpreter(instruction.InstructionType);

                    // if this returns false, then increment the program counter by 4. otherwise, this indicates
                    // that the instruction needed to change the PC.
                    if (!interpreter.InterpretInstruction(instruction.Parameters.ToArray(), m_Registers, dataSegment))
                    {
                        m_Registers[InterpreterCommon.PC_REGISTER].Value += sizeof(int);
                    }
                }

                logger.Log(LogLevel.Info, "Execution complete.");
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Critical, "Runtime exception occurred: " + ex.Message);
            }
        }

        public void Terminate()
        {
            m_TerminationRequested = true;
        }

        private readonly Register[] m_Registers;
        private readonly InterpreterFactory m_InterpreterFac;
        private bool m_TerminationRequested;
    }
}
