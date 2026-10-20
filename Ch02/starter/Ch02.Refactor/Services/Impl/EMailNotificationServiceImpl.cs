using EduHub.Ch02.Services;

namespace EduHub.Ch02.Services.Impl;

public class EmailNotificationServiceImpl : INotificationService
{
    public void Send(string to, string message)
    {
        Console.WriteLine($"[Email] to {to}: {message}");
    }
}