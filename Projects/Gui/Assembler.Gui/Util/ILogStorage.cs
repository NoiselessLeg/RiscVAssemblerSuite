using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Gui.Util
{
    interface ILogStorage
    {
        void AppendLog(string logStr);
    }
}
