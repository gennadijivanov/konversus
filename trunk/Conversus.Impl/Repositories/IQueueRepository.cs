using System;
using System.Collections.Generic;
using Conversus.Core.DomainModel;
using Conversus.Core.Infrastructure.Repository;

namespace Conversus.Impl.Repositories
{
    public interface IQueueRepository : IRepository
    {
        IQueue Get(Guid id);
        ICollection<IQueue> Get(IFilterParameters filter);
    }
}
