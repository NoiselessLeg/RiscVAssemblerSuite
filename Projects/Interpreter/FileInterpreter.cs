using Assembler.Common;
using Assembler.OutputProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Interpreter
{
    public class FileInterpreter
    {
        public FileInterpreter()
        {
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

        public void RunJefFile(string fileName, ILogger logger, ITerminal terminal)
        {
            var disassembler = new JefFileProcessor();
            DisassembledFile file = disassembler.ProcessJefFile(fileName, logger);

            m_Registers[InterpreterCommon.PC_REGISTER].Value = file.TextSegment.StartingSegmentAddress;

            int programCtr = m_Registers[InterpreterCommon.PC_REGISTER].Value;

            while (!file.TextSegment.EndOfFileReached(m_Registers[InterpreterCommon.PC_REGISTER].Value))
            {

            }
        }

        private readonly Register[] m_Registers;
    }
}
