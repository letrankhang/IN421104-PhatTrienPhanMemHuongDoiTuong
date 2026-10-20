using EduHub.Ch02.Logging;

namespace EduHub.Ch02.Logging.Impl;

public class ConsoleLoggerImpl : IAppLogger
{
    public void Info(string message) => Console.WriteLine($"[LOG] {message}");
}