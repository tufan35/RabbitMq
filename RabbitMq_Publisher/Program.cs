using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new();

factory.Uri = new("amqps://nlehriei:6N6mjtRm7LXKuXMGFgfxGQ-GvZewo-fE@moose.rmq.cloudamqp.com/nlehriei");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();
string exchangeName = "example-pub-sub-queue";

channel.ExchangeDeclare(
    exchange: exchangeName,
    type: ExchangeType.Fanout);



for (int i = 0; i < 100; i++)
{
    await Task.Delay(100);

    var message = Encoding.UTF8.GetBytes("merhaba " + i);

    channel.BasicPublish(
    exchange: exchangeName,
    routingKey: string.Empty,
    body: message
    );
}

Console.Read();


#region Publish/Subscribe Tasarımı
///Bu tasarımda mesajı bir exchnage gönderir ve böylece mesaj bu exchange e bind edilmiş olan tüm kuyruklara yönlendirilir. Butasarım bir mesajın birçok tüketici tarafından işlenmesi gerektiği durumlarda kullanılır.
///Mesaj exchange bind edilmiş bütün kuyruklara yönlendirilir ve bircok tüketici tarafından işlenmesi gerektiği anlaıılır

/// bu tasarımlarda fanout exchange kullanılabilir

#endregion
