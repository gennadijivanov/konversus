using System.Collections.Generic;
using Conversus.Core.DTO;
using Conversus.Core.DomainModel;

namespace Conversus.Service.Contract
{
    public interface IQueueService
    {
        QueueData GetOrCreateQueue(QueueType queueType);

        ICollection<QueueData> GetQueues();
    }
}