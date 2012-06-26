using System.Collections.Generic;
using System.ServiceModel;
using Conversus.Core.DomainModel;

namespace Conversus.Service.Contract
{
    [ServiceContract]
    public interface IQueueService
    {
        [OperationContract]
        IQueue GetOrCreateQueue(QueueType queueType);

        [OperationContract]
        ICollection<IQueue> GetQueues();
    }
}