using Assembler.CmdLine.LoggerTypes;
using Assembler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.CmdLine.LoggerTypes
{
    /// <summary>
    /// Represents a logger that logs to a command line and a file.
    /// </summary>
    class HybridLogger : ThreadSafeLogger
    {
        /// <summary>
        /// Creates a new instance of the HybridLogger.
        /// </summary>
        /// <param name="fileName">The file that will be logged to. This will overwrite a file with the same name.</param>
        public HybridLogger(string fileName) 
        {
            m_ConsoleLogger = new ConsoleLogger();
            m_FileLogger = new FileLogger(fileName);
        }

        /// <summary>
        /// Override for thread safe logger
        /// </summary>
        /// <param name="level"></param>
        /// <param name="logStr"></param>
        protected override void LogImpl(LogLevel level, string logStr)
        {
            m_ConsoleLogger.Log(level, logStr);
            m_FileLogger.Log(level, logStr);
        }

        private readonly FileLogger m_FileLogger;
        private readonly ConsoleLogger m_ConsoleLogger;
    }
}
