using Assembler.Common;
using Assembler.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.InstructionProcessing
{
    class LaProcessor : SymbolicInstructionProcessor
    {
        public LaProcessor(SymbolTable symTable) :
            base(symTable)
        {
        }

        /// <summary>
        /// Parses an instruction and generates the binary code for it.
        /// </summary>
        /// <param name="address">The address of the instruction being parsed in the .text segment.</param>
        /// <param name="instructionArgs">An array containing the arguments of the instruction.</param>
        /// <returns>One or more 32-bit integers representing this instruction. If this interface is implemented
        /// for a pseudo-instruction, this may return more than one instruction value.</returns>
        public override IEnumerable<int> GenerateCodeForInstruction(int address, string[] instructionArgs)
        {
            if (instructionArgs.Length != 2)
            {
                throw new ArgumentException("la - expected 2 arguments; received " + instructionArgs.Length);
            }

            Symbol sym = SymbolTable.GetSymbol(instructionArgs[1]);
            var instructionList = new List<int>();

            // if the symbol we're trying to load is in the .text segment,
            // then we have no choice but to generate the worst-case code,
            // as our symbol generator may not have adequately predicted 
            // where the symbol will lie.
            if (sym.SegmentType == SegmentType.Data)
            {
                instructionList.AddRange(new AddiProcessor().GenerateCodeForInstruction(address, new string[] { instructionArgs[0], "x0", sym.Address.ToString() }));
            }
            else
            {
                int shiftedAddress = sym.Address >> 12;
                instructionList.AddRange(new LuiProcessor().GenerateCodeForInstruction(address, new string[] { instructionArgs[0], shiftedAddress.ToString() }));

                // need to do something if this value is less 
                int orImmVal = sym.Address & 0xFFF;
                instructionList.AddRange(new OriProcessor().GenerateCodeForInstruction(address, new string[] { instructionArgs[0], instructionArgs[0], orImmVal.ToString() }));
            }

            return instructionList;
        }

        /// <summary>
        /// Determines how many instructions are generated via a pseudo-instruction. The default implementation assumes
        /// that only one instruction will be returned.
        /// </summary>
        /// <param name="address">The address of the instruction being parsed in the .text segment.</param>
        /// <param name="args">An array containing the arguments of the instruction.</param>
        /// <returns>An integer representing how many instructions will be generated for a line of assembly.</returns>
        protected override int GetNumOfInstructionsForSymbolicInstruction(int address, string[] args)
        {
            if (args.Length != 2)
            {
                throw new ArgumentException("la - expected 2 arguments; received " + args.Length);
            }

            int numGeneratedInstructions = 0;
            const int UNKNOWN_SYMBOL_INST_COUNT = 2;

            // see if we've seen this symbol before.
            // if we have and its a text segment symbol, assume worst case (eventually when we add linkage modifiers,
            // all assumptions about known addresses are going to most likely change).
            // otherwise, if we don't have this symbol by now, then it is a text segment symbol that we haven't read.
            // assume worst case.
            if (SymbolTable.ContainsSymbol(args[1]))
            {
                Symbol sym = SymbolTable.GetSymbol(args[1]);
                if (sym.SegmentType == SegmentType.Data)
                {
                    var tempProc = new AddiProcessor();
                    numGeneratedInstructions = tempProc.GenerateCodeForInstruction(address, new string[] { args[0], "x0", sym.Address.ToString() }).Count();
                }
                else
                {
                    numGeneratedInstructions = UNKNOWN_SYMBOL_INST_COUNT;
                }
            }
            else
            {
                numGeneratedInstructions = UNKNOWN_SYMBOL_INST_COUNT;
            }

            return numGeneratedInstructions;
        }
    }
}
