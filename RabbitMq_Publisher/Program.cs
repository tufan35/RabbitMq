using RabbitMQ.Client;
using System.Text;

///Direct_Exchange : Mesajların direkt olarak belirli bir kuyruga gönderilmesinisağlayan exchangedir

ConnectionFactory factory = new();

factory.Uri = new("amqps://nlehriei:6N6mjtRm7LXKuXMGFgfxGQ-GvZewo-fE@moose.rmq.cloudamqp.com/nlehriei");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "direct-exchange-example", type: ExchangeType.Direct);

while (true)
{
    Console.WriteLine("mesaj : ");

    string mes = Console.ReadLine();
    var bytemes = Encoding.UTF8.GetBytes(mes);

    channel.BasicPublish(
        exchange: "direct-exchange-example",
        routingKey: "direct-queque-example",
        body: bytemes
        );
}



Console.Read();
