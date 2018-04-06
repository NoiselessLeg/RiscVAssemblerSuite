using Assembler.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.InstructionProcessing
{
    abstract class LoadInstructionBase : SymbolicInstructionProcessor
    {
        protected LoadInstructionBase(SymbolTable symTbl):
            base(symTbl)
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
            if (args.Length != 2)
            {
                throw new ArgumentException("Invalid number of arguments provided. Expected 2, received " + args.Length + '.');
            }

            int rdReg = RegisterMap.GetNumericRegisterValue(args[0]);

            var retList = new List<int>();

            byte funcCode = GetFunctionCode();
            if (IsParameterizedToken(args[1]))
            {
                ParameterizedInstructionArg arg = ParameterizedInstructionArg.ParameterizeArgument(args[1]);
                int instruction = 0;
                instruction |= ((arg.Offset & 0xFFF) << 20);
                instruction |= (arg.Register << 15);
                instruction |= (funcCode << 12);
                instruction |= (rdReg << 7);
                instruction |= 0x3;
                retList.Add(instruction);
            }
            else
            {
                Symbol sym = SymbolTable.GetSymbol(args[1]);
                int shiftedAddress = sym.Address >> 12;
                retList.AddRange(new AuipcProcessor().GenerateCodeForInstruction(address, new[] { args[0], shiftedAddress.ToString() }));
                int numericOffset = sym.Address & 0xFFF;
                int instruction = 0;
                instruction |= (numericOffset << 20);
                instruction |= (rdReg << 15);
                instruction |= (funcCode << 12);
                instruction |= (rdReg << 7);
                instruction |= 0x3;
                retList.Add(instruction);
            }

            return retList;
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
            if (args.Length != 2)
            {
                throw new ArgumentException("Invalid number of arguments provided. Expected 2, received " + args.Length + '.');
            }

            int numInstructions = 0;

            // if it is a parameterized token, then this will generate one instruction.
            if (IsParameterizedToken(args[1]))
            {
                numInstructions = 1;
            }
            else
            {
                // otherwise, this will generate 2 instructions (an auipc instruction, and the actual lw instruction)
                numInstructions = 2;
            }

            return numInstructions;
        }

        /// <summary>
        /// Gets the three bit function code associated with this branch instruction that calls out
        /// what type of load instruction this is.
        /// </summary>
        /// <returns>A three bit numeric value that tells the processor what instruction type
        /// this represents.</returns>
        protected abstract byte GetFunctionCode();
    }
}
