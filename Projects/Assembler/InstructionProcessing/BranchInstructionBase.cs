using Assembler.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.InstructionProcessing
{
    abstract class BranchInstructionBase : SymbolicInstructionProcessor
    {
        protected BranchInstructionBase(SymbolTable symTbl) :
            base(symTbl)
        {
        }

        /// <summary>
        /// Generates the basic code for a branch instruction by delegating to implementing classes
        /// to provide a function code.
        /// </summary>
        /// <param name="address">The address of the instruction being parsed in the .text segment.</param>
        /// <param name="args">An array containing the arguments of the instruction.</param>
        /// <returns>One or more 32-bit integers representing this instruction. If this interface is implemented
        /// for a pseudo-instruction, this may return more than one instruction value.</returns>
        public override IEnumerable<int> GenerateCodeForInstruction(int address, string[] args)
        {
            // we expect three arguments. if not, throw an ArgumentException
            if (args.Length != 3)
            {
                throw new ArgumentException("Invalid number of arguments provided. Expected 3, received " + args.Length + '.');
            }

            int rs1Reg = RegisterMap.GetNumericRegisterValue(args[0]);
            int rs2Reg = RegisterMap.GetNumericRegisterValue(args[1]);

            Symbol symbolLabel = SymbolTable.GetSymbol(args[2]);

            // the instruction should always have a last two bits of 0, since they're word aligned.
            System.Diagnostics.Debug.Assert((symbolLabel.Address & 0x3) == 0);

            // find the difference between the jump-to address and the theoretical next address.
            // note that the processor internally doubles this value, so we halve it here.
            int offset = (symbolLabel.Address - address) / 2;
            
            // if the offset is greater than the 12 bit immediate,
            // throw an error so that bad code isn't silently generated.
            if ((Math.Abs(offset) & 0xFFFFF000) != 0)
            {
                throw new ArgumentException("The offset between the address of \"" + symbolLabel.LabelName + "\"" +
                    " (0x" + symbolLabel.Address.ToString("X") + " and this instruction address (0x" +
                    address.ToString("X") + ") exceeds the 12 bit immediate limit.");
            }
            
            // this is a B-type instruction, so bits go all over the place.
            // last bit is ignored, since it would be zero anyway.

            // get the thirteenth (offset 12) bit of the immediate (counting from zero), and shift it to the end.
            int offset0 = offset & 0x1000;
            int instruction = 0;
            instruction |= (offset0 << 19);

            // get the 11-6 (offsets 10-5) bits of the immediate.
            int offset1 = offset & 0x7E0;
            instruction |= (offset1 << 20);

            instruction |= (rs2Reg << 20);
            instruction |= (rs1Reg << 15);

            // Get the implementation function code, and move it into position
            byte funcCode = GetFunctionCode();
            instruction |= (funcCode << 12);

            // get the 5-2 (offsets 4-1) bits of the immediate
            // they belong in the 11-8th offset of the code, so left shift by 7 bits.
            int offset2 = offset & 0x1E;
            instruction |= (offset2 << 7);

            // get the 12th (offset 11) bit of the immediate.
            // this belongs in the 7th offset, so we need to bit shift it right by 4.
            int offset3 = offset & 0x800;
            instruction |= (offset3 >> 4);
            instruction |= 0x63;

            var instructionList = new List<int>();
            instructionList.Add(instruction);
            return instructionList;
        }

        /// <summary>
        /// Explicitly forces implementors to define a specific implementation per-instruction that calculates
        /// the number of instructions generated for an instruction that accepts a symbol as a parameter.
        /// Implementors should take care to NOT necessarily rely on the SymbolTable as part of this calculation,
        /// as it is not guaranteed that all symbols will have been loaded prior to this calculation being performed.
        /// </summary>
        /// <param name="address">The address in the .text segment of the instruction being parsed.</param>
        /// <param name="args">The parameters of the instruction</param>
        /// <returns>An integer representing how many instructions will be generated for a line of assembly.</returns>
        protected override int GetNumOfInstructionsForSymbolicInstruction(int address, string[] args)
        {
            return 1;
        }

        /// <summary>
        /// Gets the three bit function code associated with this branch instruction that calls out
        /// what type of branch instruction this is.
        /// </summary>
        /// <returns>A three bit numeric value that tells the processor what instruction type
        /// this represents.</returns>
        protected abstract byte GetFunctionCode();
    }
}
