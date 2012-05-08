using System;
using Conversus.Core.DomainModel;
using Conversus.Core.Infrastructure.Repository;

namespace Conversus.Impl.Repositories
{
    public interface IQueueRepository : IRepository
    {
        IQueue Get(Guid id);
        IQueue GetByClient(Guid clientId);
    }
}
