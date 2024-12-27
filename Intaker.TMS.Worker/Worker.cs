using System.Text;
using System.Text.Json;
using Intaker.TMS.Bll.Events;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Intaker.TMS.Worker;

public class Worker : BackgroundService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<Worker> _logger;

    public Worker(IConfiguration configuration, ILogger<Worker> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var queueName = _configuration["WorkTaskCreatedQueue"];

        var factory = new ConnectionFactory { HostName = _configuration["RabbitMq:Host"] };
        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(queue: queueName, durable: true, exclusive: false, autoDelete: false,
            arguments: null);

        _logger.LogInformation(" [*] Waiting for messages.");

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += (_, eventArgs) =>
        {
            var body = eventArgs.Body.ToArray();
            var rawMessage = Encoding.UTF8.GetString(body);
            IWorkTaskCreatedEvent createdEvent = null;
            try
            {
                createdEvent = JsonSerializer.Deserialize<WorkTaskCreatedEvent>(rawMessage);
            }
            catch (Exception e)
            {
                _logger.LogError($" [x] {e.Message}");
            }

            _logger.LogInformation($" [x] Task {createdEvent?.WorkTaskId}: {createdEvent?.WorkTaskName} created");
            return Task.CompletedTask;
        };

        await channel.BasicConsumeAsync(queueName, autoAck: true, consumer: consumer);

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }
}
