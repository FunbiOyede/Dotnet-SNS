using SNS.Publisher.Models;
using Amazon.SimpleNotificationService.Model;
using Amazon.SimpleNotificationService;
using System.Text.Json;
using SNS.Publisher.Contracts;

namespace SNS.Publisher;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    private readonly ISNSPublisherService SNSPublisherService;

    public Worker(ILogger<Worker> logger, ISNSPublisherService _SNSPublisher)
    {
        _logger = logger;
        SNSPublisherService = _SNSPublisher;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var response = SNSPublisherService.PublishSNSMessageAsync();
            Console.WriteLine(response.Status.ToString());

            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(10000, stoppingToken);
            
        }
    }
    
}
