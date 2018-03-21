using System;

namespace Assembler
{
    public class AssemblyException : Exception
    {
        
        public AssemblyException(int lineNum, string reason):
            base("Line " + lineNum + ": " + reason)
        {
        }
    }
}
