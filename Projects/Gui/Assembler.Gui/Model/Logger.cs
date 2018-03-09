using Assembler.Gui.Util;
using Assembler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Gui.Model
{
    class Logger : LoggerBase
    {
        public Logger(ILogStorage logStorage)
        {
            m_LogStore = logStorage;
        }

        protected override void LogImpl(string logStr)
        {
            m_LogStore.AppendLog(logStr);
        }

        private readonly ILogStorage m_LogStore;
    }
}
