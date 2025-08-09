using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NotificationService.Application.Interfaces;
using NotificationService.Contracts.Events;
using NotificationService.Contracts.Requests;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
//using Microsoft.Extensions.DependencyInjection; TODO//fix messageMQ Broker

namespace NotificationService.Infrastructure.Messaging
{
    public class RabbitMqNotificationConsumer : BackgroundService
    {
        private readonly INotificationService _notificationService;
        //private readonly IServiceScopeFactory _scopeFactory; TODO//fix messageMQ Broker
        private IConnection? _connection;
        private IChannel? _channel;

        public RabbitMqNotificationConsumer(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            _connection = await factory.CreateConnectionAsync(stoppingToken);
            _channel = await _connection.CreateChannelAsync(cancellationToken: stoppingToken);

            await _channel.QueueDeclareAsync(
                queue: "notifications",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null,
                cancellationToken: stoppingToken
            );

            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += async (sender, eventArgs) =>
            {
                try
                {
                    var body = eventArgs.Body.ToArray();
                    var json = Encoding.UTF8.GetString(body);
                    var message = JsonSerializer.Deserialize<InspectionEvent>(json);

                    if (message != null)
                    {
                        await _notificationService.SendNotificationAsync(new SendNotificationRequest
                        {
                            Recipient = message.Recipient,
                            Subject = message.EventType,
                            Message = message.Details,
                            Type = "Email"
                        });
                    }

                    await ((AsyncEventingBasicConsumer)sender)
                        .Channel
                        .BasicAckAsync(eventArgs.DeliveryTag, multiple: false);
                }
                catch (Exception ex)
                {
                    // TODO: log error
                    Console.WriteLine($"Error processing message: {ex.Message}");
                }
            };

            await _channel.BasicConsumeAsync(
                queue: "notifications",
                autoAck: false,
                consumer: consumer,
                cancellationToken: stoppingToken
            );

            // Keep alive until service is stopped
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_channel != null)
                await _channel.CloseAsync(cancellationToken: cancellationToken);

            if (_connection != null)
                await _connection.CloseAsync(cancellationToken: cancellationToken);

            await base.StopAsync(cancellationToken);
        }
    }
}
