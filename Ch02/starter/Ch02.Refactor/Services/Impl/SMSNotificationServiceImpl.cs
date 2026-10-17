public class SMSNotificationServiceImpl : INotificationService
{
    public void Send(string to, string message)
    {
        Console.WriteLine($"[SMS] to {to}: {message}");
    }
}