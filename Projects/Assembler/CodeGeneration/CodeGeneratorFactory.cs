using Assembler.Common;
using Assembler.InstructionProcessing;
using Assembler.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.CodeGeneration
{
    class CodeGeneratorFactory
    {
        /// <summary>
        /// Creates an instance of the CodeGeneratorFactory.
        /// </summary>
        /// <param name="logger">The logging implementation to use for logging warnings/information.</param>
        /// <param name="symTable">The populated symbol table.</param>
        /// <param name="procFactory">The instruction processor factory to retrieve code generator implementations from.</param>
        public CodeGeneratorFactory(ILogger logger, SymbolTable symTable, InstructionProcessorFactory procFactory)
        {
            m_CodeGeneratorTable = new Dictionary<SegmentType, ISegmentCodeGenerator>()
            {
                { SegmentType.Data, new DataCodeGenerator(symTable) },
                { SegmentType.Text, new TextCodeGenerator(logger, symTable, procFactory) }
            };
        }

        /// <summary>
        /// Retrieves a code generator implementation for a specified segment type.
        /// </summary>
        /// <param name="segType">The segment type to retrieve a code generator for.</param>
        /// <returns>The code generator implementation for the specified segment.</returns>
        public ISegmentCodeGenerator GetCodeGeneratorForSegment(SegmentType segType)
        {
            ISegmentCodeGenerator codeGen = default(ISegmentCodeGenerator);
            if (!m_CodeGeneratorTable.TryGetValue(segType, out codeGen))
            {
                throw new ArgumentException("No code generator available for segment type " + segType);
            }

            return codeGen;
        }

        private readonly Dictionary<SegmentType, ISegmentCodeGenerator> m_CodeGeneratorTable;
    }
}
