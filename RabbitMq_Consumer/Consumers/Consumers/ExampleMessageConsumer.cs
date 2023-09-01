using MassTransit;
using Shared.Messages;

namespace RabbitMq_Consumer.Consumers.Consumers
{
    public class ExampleMessageConsumer : IConsumer<IMessage>
    {
        public Task Consume(ConsumeContext<IMessage> context)
        {
            Console.WriteLine($"Gelene mesaj : {context.Message.Text}");

            return Task.CompletedTask;
        }
    }
}
