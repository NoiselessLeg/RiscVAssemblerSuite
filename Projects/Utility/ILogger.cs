namespace Assembler.Common
{
    /// <summary>
    /// Defines the various types of information levels that the assembler will use.
    /// </summary>
    public enum LogLevel
    {
        Critical,
        Warning,
        Info,
        DebugFine
    }

    /// <summary>
    /// Defines a class that can log to I/O with varying levels of severity.
    /// </summary>
    public interface ILogger
    {
        void Log(LogLevel level, string str);
    }
}
