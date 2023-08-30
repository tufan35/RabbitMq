using RabbitMQ.Client;
using System.Text;

///Mesajların bu exchange bind olmuş olan tüm kuyruklara gönderilmesini sağlar 
///Publisher mesajların gönderildiği kuyruk isimlerini dikkate almaz ve mesajları bütün kuyruklara iletir

ConnectionFactory factory = new();

factory.Uri = new("amqps://nlehriei:6N6mjtRm7LXKuXMGFgfxGQ-GvZewo-fE@moose.rmq.cloudamqp.com/nlehriei");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare("fanout_exchange_example", type: ExchangeType.Fanout);

for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);

    byte[] message = Encoding.UTF8.GetBytes($"Merhaba {i}");
    channel.BasicPublish(exchange: "fanout_exchange_example", routingKey: string.Empty,
        body: message);
}

Console.Read();