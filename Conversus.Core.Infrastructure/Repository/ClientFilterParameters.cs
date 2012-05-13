using System;

namespace Conversus.Core.Infrastructure.Repository
{
    public class ClientFilterParameters : IFilterParameters
    {
        public Guid? QueueId { get; set; }
    }
}