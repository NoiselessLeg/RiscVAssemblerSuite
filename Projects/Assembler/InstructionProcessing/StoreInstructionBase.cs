using Assembler.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.InstructionProcessing
{
    abstract class StoreInstructionBase : BaseInstructionProcessor
    {
        protected StoreInstructionBase()
        {
        }

        public override IEnumerable<int> GenerateCodeForInstruction(int nextTextAddress, string[] args)
        {
            if (args.Length != 2)
            {
                throw new ArgumentException("Invalid number of arguments provided. Expected 2, received " + args.Length + '.');
            }

            int rs2 = RegisterMap.GetNumericRegisterValue(args[0]);
            
            var retList = new List<int>();
            ParameterizedInstructionArg arg = ParameterizedInstructionArg.ParameterizeArgument(args[1]);
            int instruction = 0;
            int upperOffset = (arg.Offset & 0xFE0);
            int lowerOffset = (arg.Offset & 0x1F);
                
            instruction |= (upperOffset << 25);
            instruction |= (rs2 << 20);
            instruction |= (arg.Register << 15);

            byte funcCode = GetFunctionCode();
            instruction |= (funcCode << 12);

            instruction |= (lowerOffset << 7);
            instruction |= 0x23;
            retList.Add(instruction);

            return retList;
        }

        protected abstract byte GetFunctionCode();
    }
}
