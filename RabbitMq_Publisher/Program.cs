using MassTransit;
using Microsoft.Extensions.Hosting;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddMassTransit(config =>
        {
            config.UsingRabbitMq(context, _config =>
            {
                _config.Host("amqps://nlehriei:6N6mjtRm7LXKuXMGFgfxGQ-GvZewo-fE@moose.rmq.cloudamqp.com/nlehriei");
            });
        });

        services.AddHostedService<PıblishMessageService>(provider =>
        {
            usign IServiceScope  scope = provider.CreateScope();
            IPublishEndpoint publishEndpoint = scope.ServiceProvider.GetService<IPublishEndpoint>();
            return new(publishEndpoint);
        });

    })
    .Build();

host.Run();