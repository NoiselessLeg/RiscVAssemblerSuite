using System;
using System.Collections.Generic;

namespace Assembler.CodeGeneration.InstructionGenerators
{
    class NopInstructionParser : IParser
    {
        public IEnumerable<int> ParseInstruction(int nextTextAddress, string[] instructionArgs)
        {
            // we expect no arguments. if not, throw an ArgumentException
            if (instructionArgs.Length != 0)
            {
                throw new ArgumentException("Invalid number of arguments provided. Expected 0, received " + instructionArgs.Length + '.');
            }
            var delegateParser = new AddImmediateInstructionParser();
            return delegateParser.ParseInstruction(nextTextAddress, new string[] { "x0", "x0", "0" });
        }
    }
}
