namespace EduHub.Ch02.Services;
public interface INotificationServiceFactory
{
    INotificationService Create(NotificationChannel channel);
}