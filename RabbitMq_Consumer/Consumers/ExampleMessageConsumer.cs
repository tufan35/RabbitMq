using MassTransit;
using Shared.Messages;

namespace RabbitMq_Consumer.Consumers
{
    public class ExampleMessageConsumer : IConsumer<IMessage>
    {
        public Task Consume(ConsumeContext<IMessage> context)
        {
            Console.WriteLine($"Gelen mesaj : {context.Message.Text} ");

            return Task.CompletedTask;
        }
    }
}


/// IConsumer uerinden IMessage implemente ediyoruz
/// hangi messaj tüketilecekse onun referansını bildirecektir. 
/// bu consumerın calısma sekli ise 
/// ne zaman kuyruga IMesaage türünden bir mesaj geldi bu consumer consume fonksiyonunu tetikleyerek context üzerinden mesaj işlenecektir. 