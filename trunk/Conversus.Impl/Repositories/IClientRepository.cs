using System.Collections.Generic;
using Conversus.Core.DomainModel;
using Conversus.Core.Infrastructure.Repository;

namespace Conversus.Impl.Repositories
{
    public interface IClientRepository : IRepository
    {
        IClient Get(int id);

        ICollection<IClient> GetClients(int queueId);
    }
}