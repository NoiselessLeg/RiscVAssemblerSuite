using Assembler.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.InstructionProcessing
{
    class LwProcessor : BaseInstructionProcessor
    {
        public LwProcessor(SymbolTable symTbl)
        {
            m_SymTbl = symTbl;
        }

        public override IEnumerable<int> GenerateCodeForInstruction(int nextTextAddress, string[] args)
        {
            if (args.Length != 3)
            {
                throw new ArgumentException("Invalid number of arguments provided. Expected 3, received " + args.Length + '.');
            }

            string rd = args[0].Trim();
            string rs1 = args[1].Trim();
            string imm = args[2].Trim();
            int rdReg = RegisterMap.GetNumericRegisterValue(rd);
            int rs1Reg = RegisterMap.GetNumericRegisterValue(rs1);
            int immVal = 0;
            bool isValidImmediate = int.TryParse(imm, out immVal);
        }

        private readonly SymbolTable m_SymTbl;
    }
}
