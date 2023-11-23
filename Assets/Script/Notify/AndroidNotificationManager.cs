#if UNITY_ANDROID
using Unity.Notifications.Android;
using UnityEngine;
using UnityEngine.Android;
#endif

public class AndroidNotificationManager : INotificationManager
{
#if UNITY_ANDROID    
    private const string LAST_TIME_CHANNEL_ID = "lastTime";

    public void RequestAuthorization()
    {
        var isFirstRequestAuthor = PlayerPrefs.GetInt("isFirstRequestAuthor", 0) == 0;
        if (!isFirstRequestAuthor) return;
        PlayerPrefs.SetInt("isFirstRequestAuthor", 1);
        if (!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"))
        {
            Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
        }
    }

    public void RegisterNotificationChannel()
    {
        var channel = new AndroidNotificationChannel()
        {
            Id = LAST_TIME_CHANNEL_ID,
            Name = "Color Rings Channel",
            Importance = Importance.Default,
            Description = "Generic notifications",
            
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    public void SendNotification(string title, string text, string icon, float fireTimeInDays)
    {
        var notification = new AndroidNotification
        {
            Title = title,
            Text = text,
            Style = NotificationStyle.BigTextStyle,
            SmallIcon = icon,
            FireTime = System.DateTime.Now.AddDays(fireTimeInDays)
        };

        AndroidNotificationCenter.SendNotification(notification, LAST_TIME_CHANNEL_ID);
    }

    public void ClearAllNotifications()
    {
        AndroidNotificationCenter.CancelAllNotifications();
    }
#endif
}
