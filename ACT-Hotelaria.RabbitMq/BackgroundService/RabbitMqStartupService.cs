using Microsoft.Extensions.Hosting;

namespace ACT_Hotelaria.RabbitMq.Message;

public class RabbitMqStartupService : IHostedService
{
    private readonly IMessageServiceBus _messageBus;

    public RabbitMqStartupService(IMessageServiceBus messageBus)
    {
        _messageBus = messageBus;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _messageBus.Initialize();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}