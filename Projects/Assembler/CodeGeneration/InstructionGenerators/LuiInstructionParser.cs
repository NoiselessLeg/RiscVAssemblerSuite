using Assembler.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.CodeGeneration.InstructionGenerators
{
    class LuiInstructionParser : IParser
    {
        public IEnumerable<int> ParseInstruction(int currentTextAddress, string[] args)
        {
            // we expect two arguments. if not, throw an ArgumentException
            if (args.Length != 2)
            {
                throw new ArgumentException("Invalid number of arguments provided. Expected 2, received " + args.Length + '.');
            }

            string rd = args[0].Trim();
            string immediateStr = args[1].Trim();

            int immediate = 0;
            if (!int.TryParse(immediateStr, out immediate))
            {
                throw new ArgumentException("Lui - argument 2 was non-integer immediate value.");
            }
            
            int rdReg = RegisterMap.GetNumericRegisterValue(rd);
            int bitShiftedImm = immediate << 12;

            int instruction = 0;
            instruction |= bitShiftedImm;
            instruction |= (rdReg << 7);
            instruction |= 0x37;
            var inList = new List<int>();
            inList.Add(instruction);
            return inList;
        }
    }
}
