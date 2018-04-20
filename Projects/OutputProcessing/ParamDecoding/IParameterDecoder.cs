using System.Collections.Generic;

namespace Assembler.OutputProcessing.ParamDecoding
{
    /// <summary>
    /// Defines a class that can decode the parameters of an instruction.
    /// </summary>
    interface IParameterDecoder
    {
        /// <summary>
        /// Decodes the parameters of a 32-bit instruction word.
        /// </summary>
        /// <param name="instruction">The instruction to decode.</param>
        /// <returns>An IEnumerable of integers representing the instruction parameters.</returns>
        IEnumerable<int> DecodeParameters(int instruction);
    }
}
