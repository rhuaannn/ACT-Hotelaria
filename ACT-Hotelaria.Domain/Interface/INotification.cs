namespace ACT_Hotelaria.Domain.Interface;

public interface INotification
{
    public bool HasValidNotication();
    List<Notification.Notification> GetNotification();
    void Handle(Notification.Notification notification);
}