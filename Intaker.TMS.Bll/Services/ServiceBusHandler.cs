using System.Text;
using System.Text.Json;
using Intaker.TMS.Bll.Events;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace Intaker.TMS.Bll.Services;

public interface IServiceBusHandler
{
    Task NotifyWorkTaskCreated(IWorkTaskCreatedEvent createdEvent);
}

public class ServiceBusHandler : IServiceBusHandler
{
    private readonly IConfiguration _configuration;

    public ServiceBusHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task NotifyWorkTaskCreated(IWorkTaskCreatedEvent createdEvent)
    {
        var queueName = _configuration["WorkTaskCreatedQueue"];

        var factory = new ConnectionFactory { HostName = _configuration["RabbitMq:Host"] };
        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();


        await channel.QueueDeclareAsync(queue: queueName, durable: true, exclusive: false, autoDelete: false,
            arguments: null);

        string message = JsonSerializer.Serialize(createdEvent);
        var body = Encoding.UTF8.GetBytes(message);

        await channel.BasicPublishAsync(exchange: string.Empty, routingKey: queueName, body: body);
    }
}