using EduHub.Ch02.Logging;

namespace EduHub.Ch02.Logging.Impl;

public class FileLoggerImpl : IAppLogger
{
    private readonly string _path;
    public FileLoggerImpl(string path) => _path = path;

    public void Info(string message)
    {
        File.AppendAllText(_path, $"[{DateTime.Now:s}] {message}{Environment.NewLine}");
    }
}