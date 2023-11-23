using System;
using UnityEngine;

public class PushNotification : MonoBehaviour
{
    [SerializeField]  private NotificationInformation[] _daysNotificationsInform;
    private INotificationManager _notificationManager;

    private void Awake()
    {
#if UNITY_ANDROID
        _notificationManager = new AndroidNotificationManager();
#endif
    }

#if UNITY_ANDROID
    private void Start()
    {
        _notificationManager.ClearAllNotifications();
        _notificationManager.RequestAuthorization();
        _notificationManager.RegisterNotificationChannel();
        SendLastTimeNotification();
    }

    private void SendLastTimeNotification()
    {
        foreach (var notification in _daysNotificationsInform)
        {
            _notificationManager.SendNotification
                (notification.title, notification.text, notification.smallIcon, notification.fireTime);
        }
    }
#endif
}

[Serializable]
public class NotificationInformation
{
    public string title;
    public string text;
    public string smallIcon;
    public float fireTime;
}