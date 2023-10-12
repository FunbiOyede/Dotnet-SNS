using SNS.Publisher;
using SNS.Publisher.Contracts;
using SNS.Publisher.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<ISNSPublisherService, SNSPublisherService>();
        services.AddHostedService<Worker>();
       
    })
    .Build();

host.Run();
