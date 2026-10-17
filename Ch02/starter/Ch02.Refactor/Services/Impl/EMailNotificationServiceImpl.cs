public class EmailNotificationServiceImpl : INotificationService
{
    public void Send(string to, string message)
    {
        Console.WriteLine($"[Email] to {to}: {message}");
    }
}