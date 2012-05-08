using System;
using Conversus.Core.DTO;

namespace Conversus.Storage
{
    public interface IQueueStorage : IStorage<QueueData>
    {
        QueueData GetByClient(Guid clientId);
    }
}
