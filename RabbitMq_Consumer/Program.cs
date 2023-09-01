
using MassTransit;
using RabbitMq_Consumer.Consumers;

string RabbitMqUri = new("amqps://nlehriei:6N6mjtRm7LXKuXMGFgfxGQ-GvZewo-fE@moose.rmq.cloudamqp.com/nlehriei");

string queueName = "example-queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    //hangi hosttan bilgi acalığımızı belirliyoruz
    factory.Host(RabbitMqUri);

    //bu sekılde de consumerı tanımlamıs oluyoruz

    factory.ReceiveEndpoint(queueName, endpoint =>
    {
        endpoint.Consumer<ExampleMessageConsumer>();
    });
});


await bus.StartAsync();

Console.Read();

////Burda en önemli olan nokta burdaki gelen mesajların tuketimi belirlenen Imesaj türlerine göre tüketilmektedir. 