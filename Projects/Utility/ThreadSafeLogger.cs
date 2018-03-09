using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Common
{
    public abstract class ThreadSafeLogger : ILogger
    {
        /// <summary>
        /// Constructs the logger base and the object used as a lock.
        /// </summary>
        public ThreadSafeLogger()
        {
            m_LockObj = new object();
        }

        /// <summary>
        /// Delegates to an underlying derived class to print the string in a thread-safe manner.
        /// This means that the string that this function is called with
        /// will be printed in its entirety without interruption from another string.
        /// </summary>
        /// <param name="level">Represents if the string is information, a warning, or
        /// an error.</param>
        /// <param name="str">The string to print.</param>
        public void Log(LogLevel level, string str)
        {
            // lock the object only once we've constructed the full string.
            lock (m_LockObj)
            {
                LogImpl(level, str + '\n');
            }
        }

        protected abstract void LogImpl(LogLevel level, string logStr);

        private readonly object m_LockObj;
    }
}
