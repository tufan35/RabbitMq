using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://nlehriei:6N6mjtRm7LXKuXMGFgfxGQ-GvZewo-fE@moose.rmq.cloudamqp.com/nlehriei");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();


channel.ExchangeDeclare(
    exchange: "header-exchange-example",
    type: ExchangeType.Headers
    );

Console.WriteLine("Header parametresini giriniz : ");
string value = Console.ReadLine();

//kuyruk oluşturdıuk
string _quequename = channel.QueueDeclare().QueueName;



channel.QueueBind(
    queue: _quequename,
    exchange: "header-exchange-example",
    routingKey: string.Empty,
    new Dictionary<string, object>
    {
        ["x-match"] = "all", ///default değer olarak any geçerlidir.
        ["no"] = value
    }
    );


EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(
    queue: _quequename,
    autoAck: true,
    consumer: consumer
    );

consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};

Console.Read();
