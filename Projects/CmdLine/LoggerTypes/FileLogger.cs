using Assembler.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.CmdLine.LoggerTypes
{
    /// <summary>
    /// A logger that logs to an external file.
    /// </summary>
    class FileLogger : ILogger, IDisposable
    {
        /// <summary>
        /// Creates an instance of a FileLogger.
        /// </summary>
        /// <param name="fileName">The file name to open for writing. If the file exists, this will
        /// overwrite it.</param>
        public FileLogger(string fileName)
        {
            m_Writer = new StreamWriter(fileName, false);
        }

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
            m_Writer.WriteLine(str);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                m_Writer.Dispose();
            }
        }

        ~FileLogger()
        {
            Dispose(false);
        }

        private readonly StreamWriter m_Writer;
    }
}
