using System;
using Conversus.Core.DomainModel;

namespace Conversus.Storage
{
    public interface IClientStorage : IStorage<IClient>
    {
        IClient ChangeQueue(Guid clientId, Guid targetOperatorId, SortPriority sortPriority);
    }
}
