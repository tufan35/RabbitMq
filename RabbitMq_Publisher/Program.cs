using MassTransit;
using Shared.Messages;

string RabbitMqUri = new("amqps://nlehriei:6N6mjtRm7LXKuXMGFgfxGQ-GvZewo-fE@moose.rmq.cloudamqp.com/nlehriei");

string queueName = "example-queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(RabbitMqUri);
});

//Tek bir kuyruga mesaj gönderilecek ise sendMesagge kullanıyoruz
ISendEndpoint sendEndpoint = await bus.GetSendEndpoint(new($"{RabbitMqUri}/{queueName}"));

Console.WriteLine("Göndeirlecek Mesaj : ");
string message = Console.ReadLine();
await sendEndpoint.Send<IMessage>(new ExampleMessage()
{
    Text = message
});

Console.Read();