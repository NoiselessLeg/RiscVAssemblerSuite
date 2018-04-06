using Assembler.CodeGeneration;
using Assembler.Common;
using Assembler.SymbolTableConstruction;
using Assembler.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assembler.InstructionProcessing
{
    abstract class BaseInstructionProcessor : IInstructionGenerator, IInstructionSizeEstimator
    {
        /// <summary>
        /// Parses an instruction and generates the binary code for it.
        /// </summary>
        /// <param name="address">The address of the instruction being parsed in the .text segment.</param>
        /// <param name="instructionArgs">An array containing the arguments of the instruction.</param>
        /// <returns>One or more 32-bit integers representing this instruction. If this interface is implemented
        /// for a pseudo-instruction, this may return more than one instruction value.</returns>
        public abstract IEnumerable<int> GenerateCodeForInstruction(int address, string[] instructionArgs);

        /// <summary>
        /// Determines how many instructions are generated via a pseudo-instruction. The default implementation assumes
        /// that only one instruction will be returned.
        /// </summary>
        /// <param name="address">The address of the instruction being parsed in the .text segment.</param>
        /// <param name="instructionArgs">An array containing the arguments of the instruction.</param>
        /// <returns>An integer representing how many instructions will be generated for a line of assembly.</returns>
        public virtual int GetNumGeneratedInstructions(int address, string[] instructionArgs)
        {
            return GenerateCodeForInstruction(address, instructionArgs).Count();
        }

        /// <summary>
        /// Determines if a token is a "parameterized" token. In other words,
        /// this token specifies that a register holds an address, and an offset
        /// to dereference it by (e.g. 4(x5)).
        /// </summary>
        /// <param name="token">The token to examine.</param>
        /// <returns>True if the token appears to be a parameterized token. This does
        /// not necessarily indicate the token is well-formed, this merely indicates
        /// that the token looks to be parameterized.</returns>
        protected bool IsParameterizedToken(string token)
        {
            return token.Contains("(") && token.Contains(")");
        }

        /// <summary>
        /// Provides access to the different portions of an instruction argument taking
        /// the form of <offset>(<registername>) (e.g. 0(x0), 4(t0))
        /// </summary>
        protected struct ParameterizedInstructionArg
        {
            /// <summary>
            /// Takes an argument (e.g. 4(x9)) and parameterizes it into the offset component
            /// and its numeric register ID.
            /// </summary>
            /// <param name="trimmedArgToken">The token to parameterize, with whitespace trimmed on both left/right sides.</param>
            /// <returns>A parameterized register/offset structure.</returns>
            public static ParameterizedInstructionArg ParameterizeArgument(string trimmedArgToken)
            {
                string[] parameterizedArgs = trimmedArgToken.Split(new[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries).Apply((str) => str.Trim()).ToArray();

                // we should expect one or two arguments.
                if (parameterizedArgs.Length != 1 && parameterizedArgs.Length != 2)
                {
                    throw new ArgumentException(trimmedArgToken + " was not in a valid format.");
                }

                ParameterizedInstructionArg retVal = default(ParameterizedInstructionArg);
                // if we have one argument, assume its the register name, and that the offset is 0.
                if (parameterizedArgs.Length == 1)
                {
                    int registerId = RegisterMap.GetNumericRegisterValue(parameterizedArgs[0]);
                    retVal = new ParameterizedInstructionArg(0, registerId);
                }
                else
                {
                    short offsetVal = 0;
                    bool isValidOffset = short.TryParse(parameterizedArgs[0], out offsetVal) && ((offsetVal & 0xF000) != 0);
                    if (!isValidOffset)
                    {
                        throw new ArgumentException(parameterizedArgs[0] + " is not a valid 12-bit offset.");
                    }

                    int registerId = RegisterMap.GetNumericRegisterValue(parameterizedArgs[1]);
                    retVal = new ParameterizedInstructionArg(offsetVal, registerId);
                }

                return retVal;
            }

            /// <summary>
            /// Creates an instance of the DereferencedRegister structure.
            /// </summary>
            /// <param name="offset">The offset to add to the address stored in the register.</param>
            /// <param name="registerName">The register containing the address to dereference.</param>
            private ParameterizedInstructionArg(short offset, int register)
            {
                m_Offset = offset;
                m_Register = register;
            }

            /// <summary>
            /// Retrieves the 12-bit immediate to offset the address stored in the register by.
            /// </summary>
            public short Offset
            {
                get { return m_Offset; }
            }

            /// <summary>
            /// Gets the register that the requested address is stored in.
            /// </summary>
            public int Register
            {
                get { return m_Register; }
            }

            
            private readonly short m_Offset;
            private readonly int m_Register;
        }
        
    }
}
