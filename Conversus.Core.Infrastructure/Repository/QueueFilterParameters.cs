using System;
using Conversus.Core.DomainModel;

namespace Conversus.Core.Infrastructure.Repository
{
    public class QueueFilterParameters : IFilterParameters
    {
        public Guid? ClientId { get; set; }

        public QueueType? QueueType { get; set; }
    }
}