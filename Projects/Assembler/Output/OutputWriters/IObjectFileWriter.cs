using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Output.OutputWriters
{
    interface IObjectFileWriter
    {
        void WriteObjectFile(string fileName, BasicObjectFile file);
    }
}
