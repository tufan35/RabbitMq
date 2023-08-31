using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();

factory.Uri = new("amqps://nlehriei:6N6mjtRm7LXKuXMGFgfxGQ-GvZewo-fE@moose.rmq.cloudamqp.com/nlehriei");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

string requestQueueName = "example-request-respnse-queuename";

channel.QueueDeclare(
    queue: requestQueueName,
    durable: false,
    exclusive: false,
    autoDelete: false
    );

string replyQueueName = channel.QueueDeclare().QueueName;

string correlationId = Guid.NewGuid().ToString();

#region Request Mesajını oluiturma ve gönderme

IBasicProperties properties = channel.CreateBasicProperties();
properties.CorrelationId = correlationId;
properties.ReplyTo = replyQueueName;

for (int i = 1; i < 10; i++)
{
    var message = Encoding.UTF8.GetBytes("merhaba : " + i);
    channel.BasicPublish(
        exchange: string.Empty,
        routingKey: requestQueueName,
        body: message,
        basicProperties: properties
        );

}

#endregion

#region Response kuyruğu dinleme
EventingBasicConsumer consumer = new(channel);

channel.BasicConsume(
    queue: replyQueueName,
    autoAck: true,
    consumer: consumer
    );

consumer.Received += (sender, e) =>
{
    ///kuyruga gelen corretlation idsi  CorrelationId bu geleni işliycez
    if (e.BasicProperties.CorrelationId == correlationId)
    {
        Console.WriteLine($"Response :  {Encoding.UTF8.GetString(e.Body.Span)}");
    }
};

#endregion



Console.Read();



#region Request/Response Tasarımı
///Bu tasarımda publisher bir request yapar gibi kuyruğa mesaj gönderir ve bu mesajı tüketen consumerdan sonuca dair başka bir kuyruktan yanıt bekle bu tarz senaryoalar için oldukca yaygındır
#endregion
