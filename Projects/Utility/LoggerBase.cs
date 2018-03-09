namespace Assembler.Common
{
    /// <summary>
    /// Provides a base logger implementation that will ensure all logged strings
    /// are printed without interruption by another thread.
    /// </summary>
    public abstract class LoggerBase : ILogger
    {
        /// <summary>
        /// Constructs the logger base and the object used as a lock.
        /// </summary>
        public LoggerBase()
        {
            m_LockObj = new object();
        }

        /// <summary>
        /// Attaches an appropriate preamble to the log string, and delegates
        /// to the underlying implementation to print the string. This also is
        /// thread-safe. This means that the string that this function is called with
        /// will be printed in its entirety without interruption from another string.
        /// </summary>
        /// <param name="level">Represents if the string is information, a warning, or
        /// an error.</param>
        /// <param name="str">The string to print.</param>
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
            }

            string logStr = preamble + str + '\n';

            // lock the object only once we've constructed the full string.
            lock (m_LockObj)
            {
                LogImpl(logStr);
            }
        }

        /// <summary>
        /// The implementation that will write a string to I/O.
        /// </summary>
        /// <param name="logStr">The string to log.</param>
        protected abstract void LogImpl(string logStr);

        private readonly object m_LockObj;
    }
}
