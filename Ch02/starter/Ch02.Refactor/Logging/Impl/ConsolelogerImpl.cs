public class ConsoleLoggerImpl : IAppLogger
{
    public void Info(string message) => Console.WriteLine($"[LOG] {message}");
}