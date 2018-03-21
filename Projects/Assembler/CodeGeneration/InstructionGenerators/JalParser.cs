using Assembler.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.CodeGeneration.InstructionGenerators
{
    class JalParser 
    {
        public JalParser(SymbolTable symbolTable)
        {
            m_SymbolTable = symbolTable;
        }

        public IEnumerable<int> ParseInstruction(int currentTextAddress, string[] args)
        {
            // we expect two arguments. if not, throw an ArgumentException
            if (args.Length != 2)
            {
                throw new ArgumentException("Invalid number of arguments provided. Expected 2, received " + args.Length + '.');
            }

            string rd = args[0].Trim();
            string offsetStr = args[1].Trim();

            int rdReg = RegisterMap.GetNumericRegisterValue(rd);
            int offset = 0;

            IEnumerable<int> returnVal = null;
            bool isNumericOffset = int.TryParse(offsetStr, out offset);
            if (isNumericOffset)
            {
                returnVal = GenerateInstructionWithNumericOffset(rdReg, offset);
            }
            else
            {
                returnVal = GenerateInstructionWithSymbolicOffset(rdReg, offsetStr);
            }

            return returnVal;
        }

        /// <summary>
        /// Generates a BEQ instruction when provided a symbol as the offset. This is acceptable, as we can
        /// easily look it up in our symbol table if it exists.
        /// </summary>
        private IEnumerable<int> GenerateInstructionWithSymbolicOffset(int rdReg, string labelName)
        {
            Symbol symbolLabel = m_SymbolTable.GetSymbol(labelName);

            //TODO: do we need to do anything with this address?
            int offset = symbolLabel.Address;
            return GenerateInstructionWithNumericOffset(rdReg, offset);
            
        }

        /// <summary>
        /// Generates a JAL instruction if given a numeric offset. This is unlikely, as we can
        /// assume our user isn't a masochist, but if it is provided a numeric address, we can 
        /// resolve it. This can be also be used when the symbol is resolved in GenerateInstructionWithSymbolicOffset.
        /// </summary>
        private IEnumerable<int> GenerateInstructionWithNumericOffset(int rdReg, int offset)
        {
            var instructionList = new List<int>();

            int instruction = 0;
            
            // get the twentieth bit offset (21st bit) of the offset value
            // and shift it to the 31st bit offset.
            instruction |= ((offset & 0x100000) << 11);

            // get the 10-1 bit offsets and shift that range to the 30-20 offset.
            instruction |= ((offset & 0x7FE) << 20);

            // get the 11th bit offset and shift it to offset 19.
            instruction |= ((offset & 0x800) << 8);

            // get the 19-12 bit offsets and shift them to position 18-11
            instruction |= ((offset & 0xFF00) >> 1);

            // shift the rd register value up to offset 11-7
            instruction |= (rdReg << 7);

            instruction |= 0x6F;
            instructionList.Add(instruction);
        
            return instructionList;
        }

        private readonly SymbolTable m_SymbolTable;
    }
}
