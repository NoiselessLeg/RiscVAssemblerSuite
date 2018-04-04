using Assembler.Util;
using System;
using System.Collections.Generic;

namespace Assembler.InstructionProcessing
{
    class BeqProcessor : SymbolicInstructionProcessor
    {
        public BeqProcessor(SymbolTable symbolTable) :
            base(symbolTable)
        {
        }

        public override IEnumerable<int> GenerateCodeForInstruction(int address, string[] args)
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

            Symbol symbolLabel = SymbolTable.GetSymbol(offsetStr);

            // the instruction should always have a last two bits of 0, since they're word aligned.
            System.Diagnostics.Debug.Assert((symbolLabel.Address & 0x3) == 0);
            
            // find the difference between the jump-to address and the theoretical next address.
            // note that the processor internally doubles this value, so we halve it here.
            int offset = (symbolLabel.Address - address) / 2;

            int instruction = 0;

            var instructionList = new List<int>();

            // if the offset is greater than the 12 bit immediate,
            // throw an error so that bad code isn't silently generated.
            if (offset > 2047 || offset < -2048)
            {
                throw new ArgumentException("beq - the offset between the address of \"" + symbolLabel.LabelName + "\"" +
                    " (0x" + symbolLabel.Address.ToString("X") + " and this instruction address (0x" +
                    address.ToString("X") + ") exceeds the 12 bit immediate limit.");
            }

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

        protected override int GetNumOfInstructionsForSymbolicInstruction(int address, string[] args)
        {
            return 1;
        }
    }
}
