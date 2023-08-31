using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://nlehriei:6N6mjtRm7LXKuXMGFgfxGQ-GvZewo-fE@moose.rmq.cloudamqp.com/nlehriei");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(
    exchange: "topic-exchange-example",
    type: ExchangeType.Topic
    );

Console.WriteLine("dinlencek topic formatını beilitriniz.");
string topic = Console.ReadLine();

string _qname = channel.QueueDeclare().QueueName;

channel.QueueUnbind(queue: _qname,
     exchange: "topic-exchange-example",
     routingKey: topic);

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume
    (
    queue: _qname,
    autoAck: true,
    consumer
    );

consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};

Console.Read();