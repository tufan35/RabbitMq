using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
//Bağlantı Oluşturma
ConnectionFactory factory = new();
factory.Uri = new("amqps://nlehriei:6N6mjtRm7LXKuXMGFgfxGQ-GvZewo-fE@moose.rmq.cloudamqp.com/nlehriei");
//Bağlantı aktifleştirme ve Kanal açma 
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();


// Queque oluşturma
channel.QueueDeclare(queue: "example-queque", exclusive: false); //consumerdaki kuyruk puslisherdaki ile birebir aynı yapılandırma da tanımlanlaıdır.

//queque den mesaj okuma 

EventingBasicConsumer consumer = new(channel);

var consumerTag = channel.BasicConsume(queue: "example-queque", autoAck: false, consumer);

///basicCncel methodu gelen bütün queque deki mesajları işlemez reddeder
///channel.BasicCancel(consumerTag);

///basicreject ile gelen tek bir mesajı reddeder
///channel.BasicReject(deliveryTag:3,requeue:true);

consumer.Received += (sender, e) =>
{
    //Kuyruga gelen mesajın işlendiği yerdir 
    //e.body = kuyruktaki mesajın bütünsel olarak getirecetir
    //e.body.Span vyea e.body.ToArray() kuyruktaki mesajı getirecekrie.


    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));

    //multiple : false => sadece delete edilmesini istediğim channeldaki mesajı silinmesini istiyorum
    channel.BasicAck(deliveryTag: e.DeliveryTag, multiple: false);

    ///BasicNack ile işlenmeyen mesajları geri gönderirr
    ///requeque parametresi işlenmeyen parametrelerin kuyrukta silinip silinmeyeceğine karar vermektedir.
    //channel.BasicNack(deliveryTag:e.DeliveryTag,multiple:false,requeue:true);



};

Console.Read();