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

        public async Task<PublishResponse> PublishSNSMessageAsync()
        {
            var orderCreatedMessage = CreateOrderMessage();
            var topicArn = await GetArnTopicAsync();

            if(string.IsNullOrEmpty(topicArn))
            {
                var publishRequest = GetPublishRequest(orderCreatedMessage, topicArn);
                var response = await _amazonSimpleNotificationService.PublishAsync(publishRequest);
                return response;
            }

            throw new Exception("Topic ARN not found!!!");
        }

        private PublishRequest GetPublishRequest(OrderCreated orderCreated, string topicArn)
        {
            var publishRequest = new PublishRequest
            {
                Message = JsonSerializer.Serialize(orderCreated),
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


        private async Task<string> GetArnTopicAsync()
        {
            if (string.IsNullOrEmpty(topicArnCache))
            {
                var topic = await _amazonSimpleNotificationService.FindTopicAsync("Orders");
                topicArnCache = topic.TopicArn;
                return topicArnCache;
            }

            return topicArnCache;
        }


        private OrderCreated CreateOrderMessage()
        {
            var message = new OrderCreated
            {
                Id = Guid.NewGuid(),
                Name = "Jordan 3s",
                category = "Shoes",
                price = 243.98,
                amount = 1,
                size = "Large"
            };
            return message;
        }
    }
}

