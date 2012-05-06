using System.Collections.Generic;
using System.Linq;
using Conversus.Core.DTO;

namespace Conversus.Storage.Impl
{
    public class FakeQueueStorage : IQueueStorage
    {
        private static readonly Dictionary<int, QueueData> _dict = new Dictionary<int, QueueData>();

        public void Create(QueueData data)
        {
            _dict.Add(data.Id, data);
        }

        public void Update(QueueData data)
        {
            _dict[data.Id] = data;
        }

        public QueueData Get(int id)
        {
            return _dict[id];
        }

        public void Delete(int id)
        {
            _dict.Remove(id);
        }

        public QueueData GetByClient(int clientId)
        {
            return _dict.Values.FirstOrDefault();
        }
    }
}
