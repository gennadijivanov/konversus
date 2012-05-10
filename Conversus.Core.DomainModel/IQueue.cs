using System.Collections.Generic;

namespace Conversus.Core.DomainModel
{
    public enum QueueType
    {
        Approvement,
        Taking
    }

    public interface IQueue : IEntity
    {
        QueueType Type { get; set; }

        ICollection<IClient> GetClients();
    }

    public interface IQueueFactory
    {
        IQueue CreateNewQueue(QueueType queueType);
    }
}
