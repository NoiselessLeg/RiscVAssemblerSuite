using Assembler.Common;
using Assembler.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.InstructionProcessing
{
    class JalProcessor : SymbolicInstructionProcessor 
    {
        public JalProcessor(SymbolTable symbolTable):
            base(symbolTable)
        {
        }

        /// <summary>
        /// Parses an instruction and generates the binary code for it.
        /// </summary>
        /// <param name="address">The address of the instruction being parsed in the .text segment.</param>
        /// <param name="args">An array containing the arguments of the instruction.</param>
        /// <returns>One or more 32-bit integers representing this instruction. If this interface is implemented
        /// for a pseudo-instruction, this may return more than one instruction value.</returns>
        public override IEnumerable<int> GenerateCodeForInstruction(int address, string[] args)
        {
            // we expect two arguments. if not, throw an ArgumentException
            if (args.Length != 2)
            {
                throw new ArgumentException("Invalid number of arguments provided. Expected 2, received " + args.Length + '.');
            }

            int rdReg = RegisterMap.GetNumericRegisterValue(args[0]);

#if DEBUG
            int targetAddress = 0;

            if (SymbolTable.ContainsSymbol(args[1]))
            {
                Symbol symbolLabel = SymbolTable.GetSymbol(args[1]);
                targetAddress = symbolLabel.Address;
            }
            else if (!IntExtensions.TryParseEx(args[1], out targetAddress))
            {
                throw new ArgumentException(args[1] + " was not a symbol name or valid 32-bit address.");
            }
            var instructionList = new List<int>();

            // the offset is doubled implicitly by the processor, so halve it here.
            int offset = (targetAddress - address);

            // this should rarely happen, but if the halved immediate exceeds the 21 bit boundary,
            // error out and notify the user.
            if ((Math.Abs(offset / 2) & 0xFFE00000) != 0)
            {
                throw new ArgumentException("jal - the offset between the address of \"0x" + targetAddress.ToString("X") + "\"" +
                    " and this instruction address (0x" +
                    address.ToString("X") + ") exceeds the 21 bit immediate limit. Use jalr instead.");
            }
#else
            Symbol symbolLabel = SymbolTable.GetSymbol(args[1]);
                targetAddress
            }
            var instructionList = new List<int>();

            // the offset is doubled implicitly by the processor, so halve it here.
            int offset = (symbolLabel.Address - address);

            // this should rarely happen, but if the halved immediate exceeds the 21 bit boundary,
            // error out and notify the user.
            if ((Math.Abs(offset / 2) & 0xFFE00000) != 0)
            {
                throw new ArgumentException("jal - the offset between the address of \"" + symbolLabel.LabelName + "\"" +
                    " (0x" + symbolLabel.Address.ToString("X") + " and this instruction address (0x" +
                    address.ToString("X") + ") exceeds the 21 bit immediate limit. Use jalr instead.");
            }
#endif

            int instruction = 0;

            // get the twentieth bit offset (21st bit) of the offset value
            // and shift it to the 31st bit offset.
            instruction |= ((offset & 0x100000) << 11);

            // get the 10-1 bit offsets and shift that range to the 30-20 offset.
            instruction |= ((offset & 0x7FE) << 20);

            // get the 11th bit offset and shift it to offset 20.
            instruction |= ((offset & 0x800) << 9);

            // get the 19-12 bit offsets and shift them to position 18-11
            instruction |= ((offset & 0xFF000));

            // shift the rd register value up to offset 11-7
            instruction |= (rdReg << 7);

            instruction |= 0x6F;
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
            // always should return one instruction.
            return 1;
        }
    }
}
