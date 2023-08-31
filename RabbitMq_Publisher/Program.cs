using RabbitMQ.Client;
using System.Text;

///Routing key yerine headerları kullanarak mesajları kuyruklara yönlendirmek için kullanılan exchangedir 
//kuyruklarlar exchange arasındaki iletişim mesajların iletilme kararını headerdaki key value belirlemektedir. 
//x-match : ilgili queque nun mesajı hangi davranısla alacağının kararını veren keydir
//any ve all değerini alır 
//any : iliglil quenın  sadece tek bir key value değerinin eşleşmesi durumunda mesajı alacağını ifade eder 
//ilgilig quenin tüm key value değerindeki verileirn eşleşmesi neticesinde mesajı alacağını ifade eder

ConnectionFactory factory = new();

factory.Uri = new("amqps://nlehriei:6N6mjtRm7LXKuXMGFgfxGQ-GvZewo-fE@moose.rmq.cloudamqp.com/nlehriei");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(
    exchange: "header-exchange-example",
    type: ExchangeType.Headers
    );

for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);
    byte[] message = Encoding.UTF8.GetBytes($"Merhaba {i}");
    Console.Write("Lütden header valuesini giriniz. : ");
    string value = Console.ReadLine();

    IBasicProperties basicProperties = channel.CreateBasicProperties();

    basicProperties.Headers = new Dictionary<string, object>
    {
        ["no"] = value
    };

    channel.BasicPublish(exchange: "header-exchange-example", routingKey: string.Empty, body: message, basicProperties: basicProperties);

}


Console.Read();
