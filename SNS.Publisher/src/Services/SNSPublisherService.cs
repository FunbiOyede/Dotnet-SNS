using System;
using Amazon.SimpleNotificationService.Model;
using SNS.Publisher.Models;
using System.Text.Json;
using Amazon.SimpleNotificationService;
using SNS.Publisher.Contracts;

namespace SNS.Publisher.Services
{
	public class SNSPublisherService : ISNSPublisherService
    {
        private readonly IAmazonSimpleNotificationService _amazonSimpleNotificationService;

        private string topicArnCache = string.Empty;

        public SNSPublisherService(IAmazonSimpleNotificationService amazonSimpleNotificationService)
		{
            _amazonSimpleNotificationService = amazonSimpleNotificationService;
        }

        public async Task<PublishResponse> PublishSNSMessageAsync<T>(T message)
        {
            
            var topicArn = await GetArnTopicAsync();

            if(!string.IsNullOrEmpty(topicArn))
            {
                var publishRequest = GetPublishRequest(message, topicArn);
                var _ = await _amazonSimpleNotificationService.PublishAsync(publishRequest);
                
            }

            throw new Exception("Topic ARN not found!!!");
        }

        private PublishRequest GetPublishRequest<T>(T message, string topicArn)
        {
            var publishRequest = new PublishRequest
            {
                Message = JsonSerializer.Serialize(message),
                TopicArn = topicArn,
                MessageAttributes = new Dictionary<string, MessageAttributeValue>
            {
                {
                    "MessageType", new MessageAttributeValue
                    {
                        DataType = "String",
                        StringValue = nameof(OrderCreated)
                    }
                }
            }
            };

            return publishRequest;
        }


        private async ValueTask<string> GetArnTopicAsync()
        {
            if (string.IsNullOrEmpty(topicArnCache))
            {
                var topic = await _amazonSimpleNotificationService.FindTopicAsync("Orders");
                topicArnCache = topic.TopicArn;
                return topicArnCache;
            }

            return topicArnCache;
        }

    }
}

