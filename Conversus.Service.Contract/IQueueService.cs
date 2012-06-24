using System.Collections.Generic;
using Conversus.Core.DomainModel;

namespace Conversus.Service.Contract
{
    public interface IQueueService
    {
        IQueue GetOrCreateQueue(QueueType queueType);

        ICollection<IQueue> GetQueues();
    }
}