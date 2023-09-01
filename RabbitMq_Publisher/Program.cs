using MassTransit;
using Shared.RequestResponseMessage;

string RabbitMqUri = new("amqps://iuiqgafd:pWhZoYdKBr8fMH20Ff2Yp4sRKKidm6T6@woodpecker.rmq.cloudamqp.com/iuiqgafd");

string requestQueue = "request-queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(RabbitMqUri);
});

await bus.StartAsync();

var request = bus.CreateRequestClient<RequestMessage>(new Uri($"{RabbitMqUri}/{requestQueue}"));

int i = 1;

while (true)
{
    await Task.Delay(200);

    var response = await request.GetResponse<ResponseMessage>(new()
    {
        MessageNo = i,
        Text = $"{i++}. request"
    });

    Console.WriteLine($"received : {response.Message.Text}");
}

Console.Read();
