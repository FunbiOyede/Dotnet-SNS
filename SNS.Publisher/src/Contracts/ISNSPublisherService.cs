using System;
using Amazon.SimpleNotificationService.Model;

namespace SNS.Publisher.Contracts
{
	public interface ISNSPublisherService
	{
		Task<PublishResponse> PublishSNSMessageAsync();

    }
}

