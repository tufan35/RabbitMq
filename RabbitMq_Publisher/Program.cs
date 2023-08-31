using RabbitMQ.Client;
using System.Text;

///Routing key kullanılarak mesajları kuyruklara yölnedirmek için kullanılan bir xchangedir routing keyin bir kısmına formatına yada yapısındaki keylere göre kuyruklara mesajlar gönderilir. 
///detaylı anlatım : https://www.gencayyildiz.com/blog/rabbitmq-topic-exchange/
ConnectionFactory factory = new();

factory.Uri = new("amqps://nlehriei:6N6mjtRm7LXKuXMGFgfxGQ-GvZewo-fE@moose.rmq.cloudamqp.com/nlehriei");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(
    exchange: "topic-exchange-example",
    type: ExchangeType.Topic

    );

for (int i = 0; i < 100; i++)
{
    Task.Delay(200);

    var message = Encoding.UTF8.GetBytes($"Merhaba {i}");
    Console.Write("Topic formatını Belitriniz.");
    var topic = Console.ReadLine();
    channel.BasicPublish(exchange: "topic-exchange-example",
        routingKey: topic,
        body: message);
}

Console.Read();