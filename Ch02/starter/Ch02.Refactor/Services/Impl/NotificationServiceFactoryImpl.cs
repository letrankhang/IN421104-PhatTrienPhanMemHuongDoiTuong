using EduHub.Ch02.Enum;
using EduHub.Ch02.Services;

namespace EduHub.Ch02.Services.Impl;

public sealed class NotificationServiceFactoryImpl : INotificationServiceFactory
{
    public INotificationService Create (NotificationChannel channel) => channel switch
    {
        NotificationChannel.Email => new EmailNotificationServiceImpl(),
        NotificationChannel.SMS => new SMSNotificationServiceImpl(),
        NotificationChannel.Slack => new SlackNotificationServiceImpl(),
        _ => throw new ArgumentOutOfRangeException(nameof(channel), channel, "Unsupported channel")
    };
}