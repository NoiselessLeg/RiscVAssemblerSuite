using Assembler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Disassembler.InstructionGenerators
{
    class EcallStringifier : IParameterStringifier
    {
        public EcallStringifier(string instructionName)
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

            retStr += m_Name;
            return retStr;
        }

        private readonly string m_Name;
    }

}
