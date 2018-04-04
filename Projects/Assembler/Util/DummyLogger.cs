using Assembler.Common;

namespace Assembler.Util
{
    /// <summary>
    /// Serves as an implementation for a logger that doesn't output anything.
    /// </summary>
    class DummyLogger : ILogger
    {
        /// <summary>
        /// No-op; does not log anything.
        /// </summary>
        /// <param name="level">Unused.</param>
        /// <param name="str">Unused.</param>
        public void Log(LogLevel level, string str)
        {
            // no-op is intentional here.
        }
    }
}
