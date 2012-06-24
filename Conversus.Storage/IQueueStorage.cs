using System;
using Conversus.Core.DomainModel;

namespace Conversus.Storage
{
    public interface IQueueStorage : IStorage<IQueue>
    {
        IQueue GetByClient(Guid clientId);
    }
}
