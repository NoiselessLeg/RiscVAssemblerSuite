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

        public override IEnumerable<int> GenerateCodeForInstruction(int nextTextAddress, string[] args)
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
                retList.AddRange(new AuipcProcessor().GenerateCodeForInstruction(nextTextAddress, new[] { args[0], shiftedAddress.ToString() }));
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

        protected abstract byte GetFunctionCode();
    }
}
