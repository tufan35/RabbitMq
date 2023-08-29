using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new();

//Bağlantı Oluştuma
factory.Uri = new("amqps://nlehriei:6N6mjtRm7LXKuXMGFgfxGQ-GvZewo-fE@moose.rmq.cloudamqp.com/nlehriei");

//Bağlantı aktiflestirme ve kanal acma
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//Que Que oluiturma 
//QueueDeclare : parameter : durable => mesajların kalıcığı ile ilgili parametredir.
//QueueDeclare : parameter : exclusive=> birden fazla bağlantıyla işlem yapılıp yapılmayacağı gösteren paramtere default = true

channel.QueueDeclare(queue: "example-queque", exclusive: false);

///Kuyruğa mesaj iletme
// rabbitmq kuyruğa atacağı mesajları byte türünden atar ve byte dönüştürmek gerekiyor
//channel.BasicPublish parameter : exchange default Direct Exhangtir
//var message = Encoding.UTF8.GetBytes("Merhaba");
//channel.BasicPublish(exchange: "", routingKey: "example-queque", body: message);

for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);
    var message = Encoding.UTF8.GetBytes("Merhaba" + i);
    channel.BasicPublish(exchange: "", routingKey: "example-queque", body: message);
}

Console.Read();
