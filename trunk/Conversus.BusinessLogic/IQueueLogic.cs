using System.Collections.Generic;
using Conversus.Core.DTO;
using Conversus.Core.DomainModel;

namespace Conversus.BusinessLogic
{
    public interface IQueueLogic
    {
        QueueData GetQueue(int clientId);

        QueueData GetOrCreateQueue(QueueType queueType);

        ICollection<QueueData> GetQueues();
    }
}