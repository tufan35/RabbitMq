using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://nlehriei:6N6mjtRm7LXKuXMGFgfxGQ-GvZewo-fE@moose.rmq.cloudamqp.com/nlehriei");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare("fanout_exchange_example", type: ExchangeType.Fanout);

Console.WriteLine("Kuyruk adını gir");
string _quename = Console.ReadLine();
channel.QueueDeclare(
    queue: _quename,
    exclusive: false);

//binding mekanizması : exchange ile kuyruklar arasında birbirlerini ilişkilendirilmesidir. 

channel.QueueBind(queue: _quename,
    exchange: "fanout_exchange_example",
    routingKey: string.Empty);

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: _quename
    , autoAck: true,
    consumer: consumer);

consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};


Console.Read();