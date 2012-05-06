using Conversus.Core.DTO;

namespace Conversus.Storage
{
    public interface IQueueStorage : IStorage<QueueData>
    {
        QueueData GetByClient(int clientId);
    }
}
