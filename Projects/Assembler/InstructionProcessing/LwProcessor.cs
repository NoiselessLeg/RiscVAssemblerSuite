using Assembler.Util;
using System;
using System.Collections.Generic;

namespace Assembler.InstructionProcessing
{
    class LwProcessor : BaseInstructionProcessor
    {
        public override IEnumerable<int> GenerateCodeForInstruction(int nextTextAddress, string[] args)
        {
            if (args.Length != 2)
            {
                throw new ArgumentException("Invalid number of arguments provided. Expected 2, received " + args.Length + '.');
            }
            
            int rdReg = RegisterMap.GetNumericRegisterValue(args[0]);

            ParameterizedInstructionArg arg = ParameterizedInstructionArg.ParameterizeArgument(args[1]);

            var retList = new List<int>();

            int instruction = 0;
            instruction |= ((arg.Offset & 0xFFF) << 20);
            instruction |= (arg.Register << 15);
            instruction |= (0x10 << 12);
            instruction |= (rdReg << 7);
            instruction |= 0x3;
            retList.Add(instruction);

            return retList;
        }
    }
}
