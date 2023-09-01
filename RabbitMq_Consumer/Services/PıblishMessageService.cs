using MassTransit;
using Microsoft.Extensions.Hosting;
using Shared.Messages;

namespace RabbitMq_Consumer.Services
{
    public class PıblishMessageService : BackgroundService
    {
        readonly IPublishEndpoint _publishEndpoint;
        public PıblishMessageService(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int i = 0;

            while (true)
            {
                ExampleMessage message = new()
                {
                    Text = $"{++i}. mesaj"
                };

                await _publishEndpoint.Publish(message);

            }
        }
    }
}
