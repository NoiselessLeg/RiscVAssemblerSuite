using Assembler.Util;
using System;
using System.Collections.Generic;

namespace Assembler.InstructionProcessing
{
    class BeqProcessor : BaseInstructionProcessor
    {
        public BeqProcessor(SymbolTable symbolTable)
        {
            m_SymbolTable = symbolTable;
        }

        public override IEnumerable<int> GenerateCodeForInstruction(int nextTextAddress, string[] args)
        {
            // we expect three arguments. if not, throw an ArgumentException
            if (args.Length != 3)
            {
                throw new ArgumentException("Invalid number of arguments provided. Expected 3, received " + args.Length + '.');
            }


            string rs1 = args[0].Trim();
            string rs2 = args[1].Trim();
            string offsetStr = args[2].Trim();

            int rs1Reg = RegisterMap.GetNumericRegisterValue(rs1);
            int rs2Reg = RegisterMap.GetNumericRegisterValue(rs2);

            IEnumerable<int> returnVal = null;
            
            returnVal = GenerateInstructionWithSymbolicOffset(nextTextAddress, rs1Reg, rs2Reg, offsetStr); 
            
            return returnVal;
        }

        /// <summary>
        /// Generates a BEQ instruction when provided a symbol as the offset. We
        /// easily look it up in our symbol table, if it exists.
        /// </summary>
        private IEnumerable<int> GenerateInstructionWithSymbolicOffset(int nextTextAddress, int rs1Reg, int rs2Reg, string labelName)
        {
            Symbol symbolLabel = m_SymbolTable.GetSymbol(labelName);

            // the instruction should always have a last two bits of 0, since they're word aligned.
            System.Diagnostics.Debug.Assert((symbolLabel.Address & 0x3) == 0);

            // divide the difference by 4 since all instructions must reside on 4-byte aligned address.
            // find the difference between the jump-to address and the theoretical next address.
            int offset = ((symbolLabel.Address - nextTextAddress) / 4);
            
            return GenerateInstructionWithNumericOffset(rs1Reg, rs2Reg, offset);
        }

        /// <summary>
        /// Generates a BEQ instruction if given a numeric offset. This is unlikely, as we can
        /// assume our user isn't a masochist, but if it is provided a numeric address, we can 
        /// resolve it. This can be also be used when the symbol is resolved in GenerateInstructionWithSymbolicOffset.
        /// </summary>
        private IEnumerable<int> GenerateInstructionWithNumericOffset(int rs1Reg, int rs2Reg, int offset)
        {
            var instructionList = new List<int>();

            int instruction = 0;

            // this is a B-type instruction, so bits go all over the place.
            // last bit is ignored, since it would be zero anyway.
            
            // get the thirteenth (offset 12) bit of the immediate (counting from zero), and shift it to the end.
            int offset0 = offset & 0x1000;
            instruction |= (offset0 << 19);

            // get the 11-6 (offsets 10-5) bits of the immediate.
            int offset1 = offset & 0x7E0;
            instruction |= (offset1 << 20);

            instruction |= (rs2Reg << 20);
            instruction |= (rs1Reg << 15);

            // get the 5-2 (offsets 4-1) bits of the immediate
            // they belong in the 11-8th offset of the code, so left shift by 7 bits.
            int offset2 = offset & 0x1E;
            instruction |= (offset2 << 7);

            // get the 12th (offset 11) bit of the immediate.
            // this belongs in the 7th offset, so we need to bit shift it right by 4.
            int offset3 = offset & 0x800;
            instruction |= (offset3 >> 4);
            instruction |= 0x63;

            instructionList.Add(instruction);
            return instructionList;
        }

        private readonly SymbolTable m_SymbolTable;
    }
}
