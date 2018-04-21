using Assembler.Common;
using Assembler.OutputProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Interpreter.InstructionInterpretation
{
    class InterpreterFactory
    {
        public InterpreterFactory(IExecutionEnvironment env, ITerminal terminalWindow)
        {
            m_InterpreterTable = new Dictionary<InstructionType, IInstructionInterpreter>()
            {
                { InstructionType.Addi, new AddiInterpreter() },
                { InstructionType.Add, new AddInterpreter() },
                { InstructionType.Andi, new AndiInterpreter() },
                { InstructionType.And, new AndInterpreter() },
                { InstructionType.Auipc, new AuipcInterpreter() },
                { InstructionType.Beq, new BeqInterpreter() },
                { InstructionType.Bge, new BgeInterpreter() },
                { InstructionType.Bgeu, new BgeuInterpreter() },
                { InstructionType.Blt, new BltInterpreter() },
                { InstructionType.Bltu, new BltuInterpreter() },
                { InstructionType.Bne, new BneInterpreter() },
                { InstructionType.Ecall, new EcallInterpreter(env, terminalWindow) },
                { InstructionType.Jal, new JalInterpreter() },
                { InstructionType.Jalr, new JalrInterpreter() },
                { InstructionType.Lb, new LbInterpreter() },
                { InstructionType.Lbu, new LbuInterpreter() },
                { InstructionType.Lh, new LhInterpreter() },
                { InstructionType.Lhu, new LhuInterpreter() },
                { InstructionType.Lui, new LuiInterpreter() },
                { InstructionType.Lw, new LwInterpreter() },
                { InstructionType.Ori, new OriInterpreter() },
                { InstructionType.Or, new OrInterpreter() },
                { InstructionType.Sb, new SbInterpreter() },
                { InstructionType.Sh, new ShInterpreter() },
                { InstructionType.Slli, new SlliInterpreter() },
                { InstructionType.Sll, new SllInterpreter() },
                { InstructionType.Slti, new SltiInterpreter() },
                { InstructionType.Slt, new SltInterpreter() },
                { InstructionType.Sltu, new SltuInterpreter() },
                { InstructionType.Srai, new SraiInterpreter() },
                { InstructionType.Sra, new SraInterpreter() },
                { InstructionType.Srli, new SrliInterpreter() },
                { InstructionType.Srl, new SrlInterpreter() },
                { InstructionType.Sub, new SubInterpreter() },
                { InstructionType.Sw, new SwInterpreter() },
                { InstructionType.Xor, new XorInterpreter() },
                { InstructionType.Xori, new XoriInterpreter() },
            };
        }



        /// <summary>
        /// Gets an interpreter for a specified instruction type. Throws an ArgumentException
        /// if the instruction is not recognized.
        /// </summary>
        /// <param name="instructionType">The type of instruction to retrieve an interpreter for.</param>
        /// <returns>The appropriate interpreter implementation for the instruction.</returns>
        public IInstructionInterpreter GetInterpreter(InstructionType instructionType)
        {
            IInstructionInterpreter stringifier = default(IInstructionInterpreter);
            if (!m_InterpreterTable.TryGetValue(instructionType, out stringifier))
            {
                throw new ArgumentException(instructionType + " is not a valid RISC-V instruction.");
            }

            return stringifier;
        }

        private readonly Dictionary<InstructionType, IInstructionInterpreter> m_InterpreterTable;
    }
}
