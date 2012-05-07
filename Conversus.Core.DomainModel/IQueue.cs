using System.Collections.Generic;

namespace Conversus.Core.DomainModel
{
    public enum QueueType
    {
        Type1,
        Type2
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
