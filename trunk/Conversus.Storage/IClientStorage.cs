using System;
using System.Collections.Generic;
using Conversus.Core.DTO;

namespace Conversus.Storage
{
    public interface IClientStorage : IStorage<ClientData>
    {
        ICollection<ClientData> GetClients(Guid queueId);
    }
}
