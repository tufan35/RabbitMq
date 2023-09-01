using Microsoft.Extensions.Hosting;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddMassTransit(config =>
        {
            config.AddConsumer<ExampleMessageConsumer>();

            config.UsingRabbitMq(context, _config =>
            {
                _config.Host("amqps://nlehriei:6N6mjtRm7LXKuXMGFgfxGQ-GvZewo-fE@moose.rmq.cloudamqp.com/nlehriei");

                _config.ReceiveEndpoint("example-message-queue", e => e.ConfigureCpnsumer<ExampleMessageConsumer>(context));
            });
        });
    })
    .Build();

await host.RunAsync();