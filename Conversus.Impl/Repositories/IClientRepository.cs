using System;
using System.Collections.Generic;
using Conversus.Core.DomainModel;
using Conversus.Core.Infrastructure.Repository;

namespace Conversus.Impl.Repositories
{
    public interface IClientRepository : IRepository
    {
        IClient Get(Guid id);

        ICollection<IClient> GetClients(Guid queueId);
    }
}