using System.Collections.Generic;
using Conversus.Core.DTO;

namespace Conversus.Storage.Impl
{
    public class FakeClientStorage : IClientStorage
    {
        #region Implementation of IStorage<ClientData>

        private static readonly Dictionary<int, ClientData> _dict = new Dictionary<int, ClientData>();

        public void Create(ClientData data)
        {
            _dict.Add(data.Id, data);
        }

        public void Update(ClientData data)
        {
            _dict[data.Id] = data;
        }

        public ClientData Get(int id)
        {
            return _dict[id];
        }

        public void Delete(int id)
        {
            _dict.Remove(id);
        }

        public ICollection<ClientData> GetClients(int queueId)
        {
            return _dict.Values;
        }

        #endregion
    }
}
