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
        public CodeGeneratorFactory(SymbolTable symTable)
        {
            m_CodeGeneratorTable = new Dictionary<SegmentType, ISegmentCodeGenerator>()
            {
                { SegmentType.Data, new DataCodeGenerator() },
                { SegmentType.Text, new TextCodeGenerator(symTable) }
            };
        }

        private readonly Dictionary<SegmentType, ISegmentCodeGenerator> m_CodeGeneratorTable;
    }
}
