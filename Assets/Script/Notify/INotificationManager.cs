using System.Collections;

public interface INotificationManager
{
    void RequestAuthorization();

    void RegisterNotificationChannel();
    void SendNotification(string notificationTitle, string notificationText, string fireTimeInDays, float notificationFireTime);

    void ClearAllNotifications();
}