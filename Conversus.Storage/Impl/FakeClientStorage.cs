using System;
using System.Collections.Generic;
using System.Linq;
using Conversus.Core.DTO;
using Conversus.Core.Infrastructure.Repository;

namespace Conversus.Storage.Impl
{
    public class FakeClientStorage : IClientStorage
    {
        #region Implementation of IStorage<ClientData>

        private static readonly Dictionary<Guid, ClientData> _dict = new Dictionary<Guid, ClientData>();

        public void Create(ClientData data)
        {
            _dict.Add(data.Id, data);
        }

        public void Update(ClientData data)
        {
            _dict[data.Id] = data;
        }

        public ClientData Get(Guid id)
        {
            return _dict[id];
        }

        public ICollection<ClientData> Get(IFilterParameters filter)
        {
            var f = filter as ClientFilterParameters;
            var queue = _dict.Values.AsQueryable();
            if (f.QueueId.HasValue)
                queue = queue.Where(q => q.Id == f.QueueId.Value);
            return queue.ToList();
        }

        public void Delete(Guid id)
        {
            _dict.Remove(id);
        }

        public ICollection<ClientData> GetClients(Guid queueId)
        {
            return Get(new ClientFilterParameters(){ QueueId = queueId });
        }

        #endregion
    }
}
