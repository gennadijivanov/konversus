using System;
using Conversus.Core.DomainModel;

namespace Conversus.Core.Infrastructure.Repository
{
    public class ClientFilterParameters : IFilterParameters
    {
        public Guid? QueueId { get; set; }

        public int? PIN { get; set; }

        public string Ticket { get; set; }

        public ClientStatus? Status { get; set; }

        public Guid? OperatorId { get; set; }
    }
}