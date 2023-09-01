using MassTransit;
using Shared.RequestResponseMessage;

namespace RabbitMq_Consumer.Consumers
{
    public class RequestMessageConsumer : IConsumer<RequestMessage>
    {
        public async Task Consume(ConsumeContext<RequestMessage> context)
        {
            Console.WriteLine(context.Message.Text);
            await context.RespondAsync<ResponseMessage>(new() { Text = $"{context.Message.MessageNo}. response to request" });
        }
    }
}
