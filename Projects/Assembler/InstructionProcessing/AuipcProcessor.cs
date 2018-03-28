using Assembler.Util;
using System;
using System.Collections.Generic;

namespace Assembler.InstructionProcessing
{
    class AuipcInstructionParser : IParser
    {
        public IEnumerable<int> ParseInstruction(int nextTextAddress, string[] args)
        {
            // we expect two arguments. if not, throw an ArgumentException
            if (args.Length != 2)
            {
                throw new ArgumentException("Invalid number of arguments provided. Expected 2, received " + args.Length + '.');
            }

            string rs1 = args[0].Trim();
            string immediateStr = args[1].Trim();

            int immediate = 0;
            if (!int.TryParse(immediateStr, out immediate))
            {
                throw new ArgumentException("Lui - argument 2 was non-integer immediate value.");
            }

            int rs1Reg = RegisterMap.GetNumericRegisterValue(rs1);
            int bitShiftedImm = immediate >> 12;

            int instruction = 0;
            instruction |= bitShiftedImm;
            instruction |= (rs1Reg << 7);
            instruction |= 0x17;
            var inList = new List<int>();
            inList.Add(instruction);
            return inList;
        }
    }
}
