using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

//exchange üzerinden herhangi bir mesaj iletilyorsa exchange type direct olduuğu için rabbitmq routing keye bakacaktır routing keye karsılık gelen hangi que ise mesaj oaraya iletilcektir.

ConnectionFactory factory = new();
factory.Uri = new("amqps://nlehriei:6N6mjtRm7LXKuXMGFgfxGQ-GvZewo-fE@moose.rmq.cloudamqp.com/nlehriei");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//1.adım
channel.ExchangeDeclare(exchange: "direct-exchange-example", type: ExchangeType.Direct);

//2.adım
string qname = channel.QueueDeclare().QueueName;

//3.adım 
channel.QueueBind(
    queue: qname,
    exchange: "direct-exchange-example",
    routingKey: "direct-queque-example"
    );

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(
    queue: qname,
    autoAck: true,
    consumer: consumer
    );

consumer.Received += (sender, e) =>
{
    var message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};

Console.Read();


//1.adım publisherdaki aynı isim ve type sahip bir exhange tanımalncaktır.
//2.adım publishr tarfından routing keydeli bulunan değerdeki kuyruğa gönddeilen mesajları kendi oluiştrdugumz kuyruya yönlendirerek tüketmemiz gerekit nbunun için öncelikle kuyruk olusturulmaldur
