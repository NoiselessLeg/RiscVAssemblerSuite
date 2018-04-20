using Assembler.Common;
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
