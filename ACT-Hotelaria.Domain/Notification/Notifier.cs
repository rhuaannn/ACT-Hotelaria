using ACT_Hotelaria.Domain.Interface;

namespace ACT_Hotelaria.Domain.Notification;

public class Notifier : INotification
{
    private List<Notification> _notifications;

    public Notifier()
    {
        _notifications = new List<Notification>();
    }
    public bool HasValidNotication()
    {
        return _notifications.Any();
    }

    public List<Notification> GetNotification()
    {
        return _notifications;
    }

    public void Handle(Notification notification)
    {
        _notifications.Add(notification);
    }
}