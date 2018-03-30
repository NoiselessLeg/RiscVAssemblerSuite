using Assembler.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assembler.InstructionProcessing
{
    class AndProcessor : BaseInstructionProcessor
    {
        public override IEnumerable<int> GenerateCodeForInstruction(int nextTextAddress, string[] args)
        {
            // we expect three arguments. if not, throw an ArgumentException
            if (args.Length != 3)
            {
                throw new ArgumentException("Invalid number of arguments provided. Expected 3, received " + args.Length + '.');
            }

            string rd = args[0].Trim();
            string rs1 = args[1].Trim();
            string rs2 = args[2].Trim();

            IEnumerable<int> returnVal = null;
            int instruction = 0;
            int rdReg = RegisterMap.GetNumericRegisterValue(rd);
            int rs1Reg = RegisterMap.GetNumericRegisterValue(rs1);
            int rs2Reg = 0;
            try
            {
                rs2Reg = RegisterMap.GetNumericRegisterValue(rs2);

                List<int> instructionList = new List<int>();
                instruction |= (rs2Reg << 20);
                instruction |= (rs1Reg << 15);
                instruction |= (0x7 << 12);
                instruction |= (rdReg << 7);
                instruction |= 0x33;
                instructionList.Add(instruction);
                returnVal = instructionList;
            }
            catch (ArgumentException)
            {
                // try to parse the string as a number; maybe the user meant andi?
                short immediate = 0;
                bool isShort = short.TryParse(rs2, out immediate);
                if (isShort)
                {
                    var immediateParser = new AndiProcessor();
                    returnVal = immediateParser.GenerateCodeForInstruction(nextTextAddress, args);
                }
                else
                {
                    // otherwise, this is garbage; rethrow the value.
                    throw;
                }
            }
            
            return returnVal;
        }
    }
}
