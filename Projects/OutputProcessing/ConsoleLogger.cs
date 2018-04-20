using Assembler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.OutputProcessing
{
    class ConsoleLogger : ILogger
    {
        public void Log(LogLevel level, string logStr)
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

            string str = preamble + logStr;
            if (str.EndsWith("\n"))
            {
                Console.Write(str);
            }
            else
            {
                Console.WriteLine(str);
            }
        }
    }
}
