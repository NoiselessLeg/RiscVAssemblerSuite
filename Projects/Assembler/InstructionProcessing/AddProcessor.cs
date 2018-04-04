using Assembler.Common;
using Assembler.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.InstructionProcessing
{
    class AddProcessor : BaseInstructionProcessor
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
                instruction |= (rdReg << 7);
                instruction |= 0x33;
                instructionList.Add(instruction);
                returnVal = instructionList;
            }
            catch (ArgumentException)
            {
                // try to parse the string as a number; maybe the user meant addi?
                int immediate = 0;
                bool isShort = int.TryParse(rs2, out immediate);
                if (isShort)
                {
                    var immediateParser = new AddiProcessor();
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

        public override int GetNumGeneratedInstructions(int nextTextAddress, string[] instructionArgs)
        {
            // we expect three arguments. if not, throw an ArgumentException
            if (instructionArgs.Length != 3)
            {
                throw new ArgumentException("Invalid number of arguments provided. Expected 3, received " + instructionArgs.Length + '.');
            }

            // try to parse the string as a number; maybe the user meant addi?
            int numInstructions = 0;

            int immediate = 0;
            bool isShort = int.TryParse(instructionArgs[2], out immediate);
            if (isShort)
            {
                var immediateParser = new AddiProcessor();
                numInstructions = immediateParser.GetNumGeneratedInstructions(nextTextAddress, instructionArgs);
            }
            else
            {
                // otherwise, this is garbage; rethrow the value.
                numInstructions = 1;
            }

            return numInstructions;
        }
    }
}
