using Assembler.Common;
using Assembler.OutputProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Interpreter.InstructionInterpretation
{
    class EcallInterpreter : IInstructionInterpreter
    {
        public EcallInterpreter(ITerminal terminal)
        {
            m_Terminal = terminal;
        }

        public bool InterpretInstruction(int[] argList, Register[] registers, DataSegmentAccessor dataSegment)
        {
            // correlates to register a0.
            // arguments SHALL be a1-a7.
            const int SYSCALL_IDX = 10;
            const int ARG1_IDX = 11;

#if SYSCALL_AGS
            const int ARG2_IDX = 12;
            const int ARG3_IDX = 13;
            const int ARG4_IDX = 14;
            const int ARG5_IDX = 15;
            const int ARG6_IDX = 16;
            const int ARG7_IDX = 17;
#endif


            if (argList.Length != 0)
            {
                throw new InvalidOperationException("Malformed ECALL instruction - expected zero parameters; received " + argList.Length);
            }

            int sysCall = registers[SYSCALL_IDX].Value;
            switch (sysCall)
            {
                case PRINT_INT_CODE:
                {
                    m_Terminal.PrintInt(registers[ARG1_IDX].Value);
                    break;
                }

                case PRINT_STR_CODE:
                {
                    string dataStr = dataSegment.ReadString(registers[ARG1_IDX].Value);
                    m_Terminal.PrintString(dataStr);
                    break;
                }

                case READ_INT_CODE:
                {
                    registers[SYSCALL_IDX].Value = m_Terminal.ReadInt();
                    break;
                }

                case READ_STR_CODE:
                {
                    throw new NotImplementedException("Read string system call not yet implemented.");
                }

                case ALLOC_MEM_CODE:
                {
                    throw new NotImplementedException("Sbrk system call not yet implemented.");
                }

                case TERMINATE_CODE:
                {
                    m_Terminal.Terminate();
                    break;
                }

                case PRINT_CHAR_CODE:
                {
                    m_Terminal.PrintChar((char)(registers[ARG1_IDX].Value & 0xFF));
                    break;
                }

                case READ_CHAR_CODE:
                {
                    registers[SYSCALL_IDX].Value = m_Terminal.ReadChar();
                    break;
                }
            }

            return false;

        }

        private readonly ITerminal m_Terminal;
        private const int PRINT_INT_CODE = 1;
        private const int PRINT_STR_CODE = 4;
        private const int READ_INT_CODE = 5;
        private const int READ_STR_CODE = 8;
        private const int ALLOC_MEM_CODE = 9;
        private const int TERMINATE_CODE = 10;
        private const int PRINT_CHAR_CODE = 11;
        private const int READ_CHAR_CODE = 12;
    }
}
