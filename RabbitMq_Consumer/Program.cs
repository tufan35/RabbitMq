using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://nlehriei:6N6mjtRm7LXKuXMGFgfxGQ-GvZewo-fE@moose.rmq.cloudamqp.com/nlehriei");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

string queueName = "example-p2p-queue";

channel.QueueDeclare(queue: queueName,
    durable: false,
    exclusive: false,
    autoDelete: false);

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);

consumer.Received += async (sender, e) =>
{
    await Console.Out.WriteLineAsync(Encoding.UTF8.GetString(e.Body.Span));
};

Console.Read();


#region P2P Tasarımı

///Bu tasarımda bir publisher ilgili mesajı direkt bir kuyruğa göndeirir ve bu mesaj kuyruğu işleyen consumer tarafından tüketilir Eğerki senaryo gereği bir mesajın bir tüketici taradınfan işlenmesi gerekiyorsa bu yaklaşım kullanılır.

#endregion

