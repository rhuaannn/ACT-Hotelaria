namespace ACT_Hotelaria.RabbitMq.Message;

public interface IMessageServiceBus
{
    Task PublishAsync(object message, string exchangeName);    
    Task Initialize();
}