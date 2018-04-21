using Assembler.OutputProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Disassembler.InstructionGenerators
{
    class JalInstructionStringifier : IParameterStringifier
    {
        public JalInstructionStringifier(string instructionName)
        {
            m_Name = instructionName;
        }
        /// <summary>
        /// Formats and stringifies an instruction as well as its parameters.
        /// </summary>
        /// <param name="currPgrmCtr">The value that the program counter would theoretically be at
        /// upon encountering this instruction.</param>
        /// <param name="inst">The disassembled instruction to stringify.</param>
        /// <param name="symTable">A reverse symbol table used to map addresses back to label names.</param>
        /// <returns>A string representing the instruction and its parameters that can be written to a text file.</returns>
        public string GetFormattedInstruction(int currPgrmCtr, DisassembledInstruction inst, ReverseSymbolTable symTable)
        {
            string retStr = string.Empty;
            // first, see if the program counter has a symbol mapped to it.
            if (symTable.ContainsSymbol(currPgrmCtr))
            {
                retStr += symTable.GetLabel(currPgrmCtr) + ":\t\t";
            }
            else
            {
                retStr += "\t\t\t";
            }

            retStr += m_Name + ' ';
            if (inst.Parameters.Count() != 2)
            {
                throw new ArgumentException("Jal instruction expected 2 arguments, received " + inst.Parameters.Count());
            }

            string rs1 = ReverseRegisterMap.GetStringifiedRegisterValue(inst.Parameters.ElementAt(0));

            retStr += rs1 + ", ";

            int offset = inst.Parameters.ElementAt(1);

            int address = currPgrmCtr + offset;
            // see if there's a symbol mapped to it.
            if (symTable.ContainsSymbol(address))
            {
                string symName = symTable.GetLabel(address);
                retStr += symName;
            }
            else
            {
                retStr += "0x" + address.ToString("X2");
            }

            return retStr;
        }

        private readonly string m_Name;
    }
}
