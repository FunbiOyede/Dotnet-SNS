using System;
namespace SNS.Publisher.Models
{
	public record OrderCreated(Guid Id = default, string Name = default!, string category = default!, double price = default!, int amount = default!,  string size = default!);
}

