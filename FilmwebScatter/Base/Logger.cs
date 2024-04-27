namespace FilmwebScatter.Base;
internal class Logger
{
    private static StreamWriter _logWriter;

    public static void Log(string message, LogLevel logLevel = LogLevel.Info)
    {
        var logLine = $"{DateTime.Now} - {logLevelStringMap[logLevel]} - {message}";
        Console.WriteLine(logLine);
        _logWriter.WriteLine(logLine);
        _logWriter.Flush();
    }

    public static void Log(Exception ex)
    {
        Log(ex.Message, LogLevel.Error);
        Log(ex.StackTrace, LogLevel.Error);
    }

    public static void StartLog()
    {
        _logWriter = new StreamWriter($"log{DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH'-'mm'-'ss")}.txt");
        _logWriter.WriteLine("");
        _logWriter.WriteLine("===============================");
        _logWriter.WriteLine("FILMWEB STCATTER STARTED");
        _logWriter.WriteLine("===============================");
        _logWriter.WriteLine("");
        _logWriter.Flush();
    }

    public static void Close()
    {
        _logWriter.Close();
    }

    public enum LogLevel
    {
        Info,
        Warning,
        Error
    }

    private static Dictionary<LogLevel, string> logLevelStringMap = new()
    {
        { LogLevel.Info, "INFO" },
        { LogLevel.Warning, "WARNING" },
        { LogLevel.Error, "ERROR" }
    };
}
