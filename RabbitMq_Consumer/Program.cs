
using MassTransit;
using RabbitMq_Consumer.Consumers;

string RabbitMqUri = new("amqps://iuiqgafd:pWhZoYdKBr8fMH20Ff2Yp4sRKKidm6T6@woodpecker.rmq.cloudamqp.com/iuiqgafd");

string requestQueue = "request-queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{

    factory.Host(RabbitMqUri);
    factory.ReceiveEndpoint(requestQueue, endpoint =>
    {
        endpoint.Consumer<RequestMessageConsumer>();
    });

});

await bus.StartAsync();
Console.Read();