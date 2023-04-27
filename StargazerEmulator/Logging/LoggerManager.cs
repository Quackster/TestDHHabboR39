using Microsoft.Extensions.Logging;

namespace Stargazer.Logging;

public class LoggerManager
{
    private static LoggerManager? _instance;

    private readonly ILoggerFactory _loggerFactory;

    private LoggerManager()
    {
        _loggerFactory = LoggerFactory.Create((builder =>
        {
            builder.AddConsole();
        }));
    }

    public static ILogger<T> GetLogger<T>()
    {
        _instance ??= new LoggerManager();
        return _instance._loggerFactory.CreateLogger<T>();
    }
}