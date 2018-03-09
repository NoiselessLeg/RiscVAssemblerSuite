using System;

namespace Assembler
{
    public class AssemblyException : Exception
    {
        public AssemblyException(string reason):
            base(reason)
        {
        }


        public AssemblyException(int lineNum, string reason):
            base("Line " + lineNum + ": " + reason)
        {
        }
    }
}
