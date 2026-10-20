using EduHub.Ch02.Services;

namespace EduHub.Ch02.Services.Impl;

public class SMSNotificationServiceImpl : INotificationService
{
    public void Send(string to, string message)
    {
        Console.WriteLine($"[SMS] to {to}: {message}");
    }
}