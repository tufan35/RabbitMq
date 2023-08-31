using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://nlehriei:6N6mjtRm7LXKuXMGFgfxGQ-GvZewo-fE@moose.rmq.cloudamqp.com/nlehriei");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

string queueName = "example-work-queue";

channel.QueueDeclare(
    queue: queueName,
    durable: false,
    exclusive: false,
    autoDelete: false);

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

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




#region Work Queue Tasarımı 

///Bu tasarımda publisher tarafından yayımlanmış bir mesajın birden fazla consumer arasından yalnızca birisi tarafıdnan tüketilmesi amaçlanmaktadır. Böylece mesajların işlenmesi sürecinde tüm consumerlar aynı iş yüküne ve görev dağılımına sahip olacaktır.
#endregion
