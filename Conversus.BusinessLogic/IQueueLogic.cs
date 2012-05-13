using System;
using System.Collections.Generic;
using Conversus.Core.DTO;
using Conversus.Core.DomainModel;

namespace Conversus.BusinessLogic
{
    public interface IQueueLogic
    {
        IQueue GetOrCreateQueue(QueueType queueType);

        ICollection<IQueue> GetQueues();
    }
}