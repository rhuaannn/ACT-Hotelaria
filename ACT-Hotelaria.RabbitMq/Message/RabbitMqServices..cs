using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace ACT_Hotelaria.RabbitMq.Message;

public class RabbitMqServices : IMessageServiceBus
{
    private readonly ConnectionFactory _factory;
    private IConnection? _connection;
    private IChannel? _channel; 
    private const string _exchange = "ACT-Hotelaria";

    public RabbitMqServices()
    {
        _factory = new ConnectionFactory
        {
            HostName = "rabbitmq",
            UserName = "guest",
            Password = "guest"
        };
    }
    private async Task EnsureConnectionAsync()
    {
        if (_channel is not null) return;

        _connection = await _factory.CreateConnectionAsync("hotelaria-publisher-consumer");
        _channel = await _connection.CreateChannelAsync();
        
        await _channel.ExchangeDeclareAsync(exchange: _exchange, type: ExchangeType.Fanout);
    }
    public async Task PublishAsync(object message, string exchangeName)
    {
        await EnsureConnectionAsync();

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        await _channel.BasicPublishAsync(
            exchange: exchangeName, 
            routingKey: string.Empty, 
            body: body);
    }

    public async Task Initialize()
    {
        await EnsureConnectionAsync();
    }

    public void Publish(object message, string exchangeName)
    {
        PublishAsync(message, exchangeName).GetAwaiter().GetResult();
    }
}