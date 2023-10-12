using Amazon.SimpleNotificationService;
using SNS.Publisher;
using SNS.Publisher.Contracts;
using SNS.Publisher.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<ISNSPublisherService, SNSPublisherService>();
        services.AddSingleton<IAmazonSimpleNotificationService, AmazonSimpleNotificationServiceClient>();
        services.AddHostedService<Worker>();
       
    })
    .Build();

host.Run();
