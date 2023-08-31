using RabbitMQ.Client;

ConnectionFactory factory = new();

factory.Uri = new("amqps://nlehriei:6N6mjtRm7LXKuXMGFgfxGQ-GvZewo-fE@moose.rmq.cloudamqp.com/nlehriei");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();


Console.Read();

#region P2P Tasarımı

///Bu tasarımda bir publisher ilgili mesajı direkt bir kuyruğa göndeirir ve bu mesaj kuyruğu işleyen consumer tarafından tüketilir Eğerki senaryo gereği bir mesajın bir tüketici taradınfan işlenmesi gerekiyorsa bu yaklaşım kullanılır.

#endregion

#region Publish/Subscribe Tasarımı
///Bu tasarımda mesajı bir exchnage gönderir ve böylece mesaj bu exchange e bind edilmiş olan tüm kuyruklara yönlendirilir. Butasarım bir mesajın birçok tüketici tarafından işlenmesi gerektiği durumlarda kullanılır.
#endregion

#region Work Queue Tasarımı 

///Bu tasarımda publisher tarafından yayımlanmış bir mesajın birden fazla consumer arasından yalnızca birisi tarafıdnan tüketilmesi amaçlanmaktadır. Böylece mesajların işlenmesi sürecinde tüm consumerlar aynı iş yüküne ve görev dağılımına sahip olacaktır.
#endregion

#region Request/Response Tasarımı
///Bu tasarımda publisher bir request yapar gibi kuyruğa mesaj gönderir ve bu mesajı tüketen consumerdan sonuca dair başka bir kuyruktan yanıt bekle bu tarz senaryoalar için oldukca yaygındır
#endregion