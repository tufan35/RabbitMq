using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://nlehriei:6N6mjtRm7LXKuXMGFgfxGQ-GvZewo-fE@moose.rmq.cloudamqp.com/nlehriei");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

string exchangeName = "example-pub-sub-queue";

channel.ExchangeDeclare(
    exchange: exchangeName,
    type: ExchangeType.Fanout);

string queueName = channel.QueueDeclare().QueueName;

channel.QueueBind(
    queue: queueName,
    exchange: exchangeName,
    routingKey: string.Empty
    );

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);


///tüm consumerar o adna tek bir mesaj işleyeblir sınırsız bouytta 
channel.BasicQos(
    prefetchCount: 1,
    prefetchSize: 0,
    global: false
    );

consumer.Received += async (sender, e) =>
{
    await Console.Out.WriteLineAsync(Encoding.UTF8.GetString(e.Body.Span));
};

Console.Read();


#region Publish/Subscribe Tasarımı
///Bu tasarımda mesajı bir exchnage gönderir ve böylece mesaj bu exchange e bind edilmiş olan tüm kuyruklara yönlendirilir. Butasarım bir mesajın birçok tüketici tarafından işlenmesi gerektiği durumlarda kullanılır.
#endregion

//çalıştırma test etme aşamasında 1 publisher  ve birden fazla consumerı dutnet run ile calıstırıp inceleyebilirsin.