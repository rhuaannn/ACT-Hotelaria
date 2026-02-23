namespace ACT_Hotelaria.Domain.Notification;

public class Notification
{
    public string? Message { get; }
    
    public Notification(string? message)
    {
        Message = message;
    }
}