using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new();

factory.Uri = new("amqps://nlehriei:6N6mjtRm7LXKuXMGFgfxGQ-GvZewo-fE@moose.rmq.cloudamqp.com/nlehriei");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

string queueName = "example-p2p-queue";

//default direct exchange
channel.QueueDeclare(queue: queueName,
    durable: false,
    exclusive: false,
    autoDelete: false);


await Task.Delay(1000);
var message = Encoding.UTF8.GetBytes("merhaba");

channel.BasicPublish(
    exchange: "",
    routingKey: queueName,
    body: message
    );

Console.Read();

#region P2P Tasarımı

///Bu tasarımda bir publisher ilgili mesajı direkt bir kuyruğa göndeirir ve bu mesaj kuyruğu işleyen consumer tarafından tüketilir Eğerki senaryo gereği bir mesajın bir tüketici taradınfan işlenmesi gerekiyorsa bu yaklaşım kullanılır.

#endregion

