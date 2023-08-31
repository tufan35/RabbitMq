using RabbitMQ.Client;
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

IBasicProperties properties = channel.CreateBasicProperties();
properties.Persistent = true;


for (int i = 0; i < 100; i++)
{
    await Task.Delay(1000);
    var message = Encoding.UTF8.GetBytes("merhaba " + i);

    channel.BasicPublish(
        exchange: string.Empty,
        routingKey: queueName,
        body: message,
        basicProperties: properties
        );
}

Console.Read();


#region Work Queue Tasarımı 

///Bu tasarımda publisher tarafından yayımlanmış bir mesajın birden fazla consumer arasından yalnızca birisi tarafıdnan tüketilmesi amaçlanmaktadır. Böylece mesajların işlenmesi sürecinde tüm consumerlar aynı iş yüküne ve görev dağılımına sahip olacaktır.
///genellikle work queue calısmalarında direct exchange paramtresi kullanılır
#endregion
