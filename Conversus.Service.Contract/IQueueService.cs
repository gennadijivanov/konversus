using System.Collections.Generic;
using System.ServiceModel;
using Conversus.Core.DomainModel;

namespace Conversus.Service.Contract
{
    [ServiceContract]
    public interface IQueueService
    {
        [OperationContract]
        QueueInfo GetOrCreateQueue(QueueType queueType);

        [OperationContract]
        ICollection<QueueInfo> GetQueues();
    }
}