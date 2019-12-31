using Assembler.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.PrototypeGui.Models
{
    class LogConsole : ILogger
    {
        private readonly BindingList<string> m_Logs;

        public LogConsole()
        {
            m_Logs = new BindingList<string>();
        }

        public BindingList<string> LogOutput
        {
            get { return m_Logs; }
        }

        public void Log(LogLevel level, string str)
        {
            string preamble = string.Empty;

            switch (level)
            {
                case LogLevel.Info:
                {
                    preamble = "INFO: ";
                    break;
                }
                case LogLevel.Warning:
                {
                    preamble = "WARNING: ";
                    break;
                }
                case LogLevel.Critical:
                {
                    preamble = "ERROR: ";
                    break;
                }

                case LogLevel.DebugFine:
                {
                    preamble = "FINE: ";
                    break;
                }
            }

            string finalStr = preamble + str;
            m_Logs.Add(finalStr);
        }
    }
}
