public class SlackNotificationServiceImpl : INotificationService
{
    public void Send(string to, string message)
    {
        Console.WriteLine($"[Slack] to {to}: {message}");
    }
}